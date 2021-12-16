using ImageInspection.EventBus;
using System;
using System.Collections.Generic;


namespace Inspector.API.Application
{
    /// <summary>
    /// Integration Event to notify to other running services(in diffrent container or process)
    /// this will be published to RabbitMQ or N service Bus etc, so that other 
    /// service can act accordingly
    /// </summary>
    public class PartAddedForInspectionIntegrationEvent : IntegrationEvent
    {
        public string Name { get; }

        public int CadId {get; }

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

    public class Geometry 
    {
        private IReadOnlyCollection<Position> Positions { get; }

        public string Name { get; }

        public Geometry(string name, List<Position> pos)
        {
            Positions = pos;
            Name = name;
        }
    }

    public class Position
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public int Id { get; }


        public Position(double x, double y, double z)
        {
            X = x;
            Z = y;
            Y = z;
        }

    }
}
