using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourWinformClient
{
    internal class Pawn : Label 
    {
        public bool IsPlaced { get; set; }

        public Pawn()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Transparent;
            this.Update();

        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new Region(graphicsPath);

            base.OnPaint(pevent);
        }

    }
}
