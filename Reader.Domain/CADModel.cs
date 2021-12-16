using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reader.Domain
{
    public class CADModel : Entity
    {
        private List<Geometry> _Geometry;
        public IReadOnlyCollection<Geometry> Geometry
        {
            get
            {
                return _Geometry;
            }
            private set
            {
                _Geometry = value.ToList();
            }
        }           

        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public bool IsInspected { get; private set; } = false;

        public CADModel(string description, DateTime creationDate, bool isInspected)
        {
            _Geometry = new List<Geometry>();
            CreationDate = creationDate;
            Description = description;
            IsInspected = isInspected;
        }       

        public void AddGeometry(IEnumerable<Geometry> geometry)
        {
            Geometry = geometry.ToList();
        }

        public void CompleteInspection()
        {
            IsInspected = true;
        }
    }

    public class Geometry : Entity
    {
        private List<Position> _Positions;
        public IReadOnlyCollection<Position> Positions
        {
            get
            {
                return _Positions;
            }
            private set
            {
                _Positions = value.ToList();
            }
        }

        public string Name { get; private set; }

        public Geometry(string name)
        {
            _Positions = new List<Position>();
            Name = name;
        }

        public void AddPosition(IEnumerable<Position> position)
        {
            _Positions = position.ToList();
        }
    }

    public class Position : ValueObject
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public int Id { get; private set; }


        public Position(double x, double y, double z)
        {
            X = x;
            Z = y;
            Y = z;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

    }
}
