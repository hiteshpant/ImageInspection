using Reader.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Infrastructure
{
    public interface ICadRepostiory:IRepository<CADModel>
    {
        CADModel Add(CADModel entity);
        CADModel Update(CADModel entity);
        Task<IEnumerable<CADModel>> GetAsync();
        Task<CADModel> Get(int id);
    }
}
