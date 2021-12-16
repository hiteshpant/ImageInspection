using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageInspection.EventBus;
using Reader.Infrastructure;

namespace Reader.API.Application
{
    public class PartInspectionCompletedIntegrationEventHandler :
                                IIntegrationEventHandler<PartInspectionCompletedIntegrationEvent>
    {
        private readonly ICadRepostiory _Repository;


        public PartInspectionCompletedIntegrationEventHandler(ICadRepostiory repository)
        {
            _Repository = repository;
        }

        public async Task Handle(PartInspectionCompletedIntegrationEvent @event)
        {
            var selectedCad =  await _Repository.Get(@event.ModelId);
            selectedCad.CompleteInspection();
            _Repository.Update(selectedCad);
            await _Repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
