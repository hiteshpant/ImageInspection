using System.Threading.Tasks;

namespace ImageInspection.EventBus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
