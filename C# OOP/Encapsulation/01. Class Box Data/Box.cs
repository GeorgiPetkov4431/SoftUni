using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length { 
            get { return this.length; } 
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }

                this.length = value;
            }
        }

        public double Width
        {
            get { return this.width; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }

                this.width = value;
            }
        }

        public double Height
        {
            get { return this.height; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height cannot be zero or negative.");
                }

                this.height = value;
            }
        }

        public double SurfaceArea()
        {
            return (this.Width * this.Height + this.Width * this.Length + this.Height * this.Length) * 2;
        }

        public double LateralSurfaceArea()
        {
            return (this.Length * this.Height + this.Width * this.Height) * 2;
        }

        public double Volume()
        {
            return this.Length * this.Height * this.Width;
        }
    }
}
