using ImageInspection.EventBus;


namespace Reader.API.Application
{
    /// <summary>
    /// Integration Event to notify to other running services(in diffrent container or process)
    /// this will be published to RabbitMQ or N service Bus etc, so that other 
    /// service can act accordingly
    /// </summary>
    public class PartInspectionCompletedIntegrationEvent : IntegrationEvent
    {
        public string Name { get; }

        public int ModelId { get; }

        public int IsInspected { get; }

        public PartInspectionCompletedIntegrationEvent(string name, int modelId, bool isInspected)
        {
            Name = name;
            ModelId = modelId;
            IsInspected = IsInspected;
        }
    }
}
