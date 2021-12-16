using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Reader.Domain;

namespace Reader.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(CadParsingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.CadModel.Any())
            {
                return;   // DB has been seeded
            }
            for (int i = 1; i < 5; i++)
            {
                var cadModel = new CADModel("CadCube"+i, DateTime.Now, false);
                var positions = new List<Position>()
                {
                    new Position(0,0,0) ,
                    new Position(10,8,0),
                    new Position(8,20,0)

                };               
                var circleGeometry = new Geometry("Circle");
                circleGeometry.AddPosition(positions);
                context.Position.AddRange(positions);

                 positions = new List<Position>()
                {
                    new Position(0,0,0) ,
                    new Position(30,20,0)
                };
                var rectangleGeometry = new Geometry("Rectangle");
                rectangleGeometry.AddPosition(positions);
                context.Position.AddRange(positions);
                positions = new List<Position>()
                {
                    new Position(0,0,0),
                    new Position(0,20,0),
                    new Position(10,20,0)
                };
                var tirangleGeometry = new Geometry("Triangle");
                tirangleGeometry.AddPosition(positions);
                context.Position.AddRange(positions);

                cadModel.AddGeometry(new List<Geometry>()
                { 
                  circleGeometry, tirangleGeometry,rectangleGeometry
                });
                context.CadModel.Add(cadModel);
                context.Geometry.AddRange(circleGeometry, tirangleGeometry, rectangleGeometry);
                                
            }
            context.SaveChanges(); 

        }
    }
}
