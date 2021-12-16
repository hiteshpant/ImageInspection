using Reader.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Reader.Infrastructure
{
    public class CADRepository : ICadRepostiory
    {
        private readonly CadParsingContext _Context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _Context;
            }
        }

        public CADRepository(CadParsingContext context)
        {
            _Context = context;
        }

        public CADModel Add(CADModel entity)
        {
            return _Context.CadModel.Add(entity).Entity;
        }

        public async Task<CADModel> Get(int id)
        {
           return await _Context.CadModel.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CADModel>> GetAsync()
        {
            return await _Context.CadModel.ToListAsync();
        }

        public CADModel Update(CADModel entity)
        {
            return _Context.CadModel.Update(entity).Entity;
        }
    }
}
