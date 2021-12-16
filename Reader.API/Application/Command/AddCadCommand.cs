using MediatR;
using Reader.Domain;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reader.API.Application.Command
{
    /// <summary>
    /// this is also a kind of Event based mechanism to issue  a command within a same service
    /// So that changes can be propogated to Aggregate or other intrested entities
    /// But this happens within a  same process to propogate changes within the same process
    /// and its more like a command rather then a event which has happened in the past
    /// </summary>
    public class AddCadCommand:IRequest<CADModel>
    {
        [DataMember]
        public IList<GeometryDTO> Geometry { get; set; }

        [DataMember]
        public string Name { get;  set; }

        [DataMember]
        public DateTime CreationDate { get;  set; }

        [DataMember]
        public bool IsInspected { get; set; } = false;

        public AddCadCommand()
        {
           
        }

    }

    public class GeometryDTO
    {
        [DataMember]
        public List<PositionDTO> Positions { get;  set; }

        [DataMember]
        public string Name { get;  set; }

        public GeometryDTO(string name, List<PositionDTO> position)
        {
            Positions = position;
            Name = name;
        }

        public GeometryDTO()
        {

        }
    }

    public class PositionDTO
    {
        [DataMember]
        public double X { get;  set; }
        [DataMember]
        public double Y { get;  set; }
        [DataMember]
        public double Z { get;  set; }

        public PositionDTO(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public PositionDTO()
        {

        }

    }
}
