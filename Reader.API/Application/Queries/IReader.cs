using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reader.API.Application.Queries
{
    /// <summary>
    /// This inteface is used for Querying
    /// Sepration is done so that CQRS can be demostrated
    /// If You have sepreate ReadOnly Db for reporting 
    /// This interface should be implemented to query data 
    /// from that DB right now in this sample its the same 
    /// DB for querying and update
    /// </summary>
    public interface IReader
    {
        Task<CADViewModel> GetSelectedCad(int id);

        Task<IEnumerable<InspectionViewModel>> GetAllCad();

    }
}
