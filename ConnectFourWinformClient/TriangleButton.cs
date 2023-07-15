using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourWinformClient
{
    public class TriangleButton : Button
    {
        public TriangleButton()
        {

        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Point[] trianglePoints = new Point[]
            {
                new Point(Width, 0),
                new Point(Width / 2, Height),
                new Point(0, 0)
            };

            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddPolygon(trianglePoints);
            this.Region = new Region(graphicsPath);

            StyleAppander.SetDefaultButtonStyle(this);
            base.OnPaint(pevent);
        }


    }
}
