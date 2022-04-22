using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders.Models
{
    internal class Effect
    {
        internal Point Position { get; set; }
        internal Size Size = new Size(12, 12);

        private GraphicsPath groupPath = new GraphicsPath();
        private Matrix rotateMatrix = new Matrix();

        internal Effect(Point Position)
        {
            this.Position = Position;

            groupPath.AddRectangle(new Rectangle(this.Position, this.Size));
            rotateMatrix.RotateAt(12, this.Position);
        }

        internal void Draw(Graphics g, Pen pen)
        {
            groupPath.Transform(rotateMatrix);
            g.DrawPath(pen, this.groupPath);
        }
    }
}
