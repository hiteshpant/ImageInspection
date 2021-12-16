using ImageInspection.EventBus;
using System.Threading.Tasks;

namespace Inspector.API.Application
{
    public class PartAddedForInspectionIntegrationEventHandler :
        IIntegrationEventHandler<PartAddedForInspectionIntegrationEvent>
    {
        private readonly IEventBus _eventbus;
        public PartAddedForInspectionIntegrationEventHandler(IEventBus eventbus)
        {
            _eventbus = eventbus;
        }

        public Task Handle(PartAddedForInspectionIntegrationEvent @event)
        {
            // This is just the simulation of Shape Inspector,x 
            //Ideally way to inspect is encapsulated in a domain layer
            foreach (var item in @event?.Position)
            {
                Task.Delay(20000);
            }

            // again Publishes message once inspection is completed 
            // reader service listens to it and update the Part to be inspected
            _eventbus.Publish(new PartInspectionCompletedIntegrationEvent(@event.Name, @event.CadId, true));
            return Task.CompletedTask;

        }
    }
}
