using System;
using System.Collections.Generic;

namespace Reader.API.Application.Queries
{
    public class InspectionViewModel
    {
        public int GeometryId { get; set; }
        public int ModelId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsInspected { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class CADViewModel
    {        
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsInspected { get; set; }
        public int Id { get; set; }      
    }

}