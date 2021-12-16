using ImageInspection.EventBus;
using Reader.Domain;
using System;
using System.Collections.Generic;

namespace Reader.API.Application
{
    /// <summary>
    /// Integration Event to notify to other running services(in diffrent container or process)
    /// this will be published to RabbitMQ or N service Bus etc, so that other 
    /// service can act accordingly
    /// </summary>
    public class PartAddedForInspectionIntegrationEvent : IntegrationEvent
    {
        public string Name { get; }

        public int CadId { get; }

        public DateTime Date { get; }

        public bool IsInspected { get; }

        public IReadOnlyCollection<Geometry> Position { get; }

        public PartAddedForInspectionIntegrationEvent(int cadId, DateTime creationDate, bool isInspected, string name,
                               IReadOnlyCollection<Geometry> position)
        {
            CadId = cadId;
            Date = creationDate;
            IsInspected = isInspected;
            Name = name;
            Position = position;
        }
    } 

}
