using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Helpers
{
    public record struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
