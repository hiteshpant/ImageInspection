using Microsoft.Extensions.Options;
using Reader.Domain;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Reader.API.Application.Queries
{
    ///Please Note, if you’re using the CQS/CQRS architectural pattern,
    //the initial queries are performed by side queries
    //out of the domain model, performed by simple SQL statements 
    //using Dapper. This approach is much more flexible than 
    //repositories because you can query and join any tables you need,
    //and these queries aren’t restricted by rules from the aggregates.That data goes to the presentation layer or client app.
    public class Reader : IReader
    {
        private readonly string _ConnectionString;

        public Reader(IConfiguration config)
        {
            _ConnectionString = config.GetSection("ConnectionString").Value;
        }

        public async Task<IEnumerable<InspectionViewModel>> GetAllCad()
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<InspectionViewModel>(
                            @"select g.Id as GeometryId, g.CADModelId as ModelId,g.Name, pos.X , pos.Y,pos.Z 
                            From dbo.Position pos                       
                            inner join dbo.Geometry g   on pos.GeometryId = g.Id
                            order By g.CadModelId
                           ");

                var cadInfo = await connection.QueryAsync<CADViewModel>(
                         @"select g.Id, g.Description,g.CreationDate,g.IsInspected
                            From dbo.CadModel g");
               
                CADViewModel selectedCAD = null;
                foreach (var item in result)
                {
                    if (selectedCAD?.Id != item.ModelId)
                    {
                        selectedCAD = cadInfo.ToList().FirstOrDefault(x => x.Id == item.ModelId);                        
                    }
                    item.IsInspected = selectedCAD.IsInspected;
                    item.Description = selectedCAD?.Description;
                    item.CreationDate = selectedCAD.CreationDate;
                }

                if (result.AsList().Count == 0)
                    throw new NoPartAvailableException();

                return result;
            }
        }        

        public async Task<CADViewModel> GetSelectedCad(int id)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {

                connection.Open();

                var cadInfo = await connection.QueryFirstAsync<CADViewModel>(
                           @"select model.Id as Id,model.Description as Description,model.IsInspected as IsInspected
                            from
                            dbo.CadModel model       
                            WHERE model.Id=@id
                           ", new { id });



                if (cadInfo == null)
                    throw new NoPartAvailableException();
                return cadInfo;
            }
        }
    }
}