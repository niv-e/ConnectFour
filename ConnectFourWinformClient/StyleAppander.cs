using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourWinformClient
{
    public static class StyleAppander
    {
        public static void SetDefaultButtonStyle(ButtonBase button)
        {

            button.BackColor = Color.FromArgb(69, 94, 181);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 63, 205);
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 63, 205);
            button.TabStop = false;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Padding = new Padding(16, 0, 16, 0);

        }


        public static void SetHoverButtonStyle(ButtonBase button)
        {
            button.Scale(new SizeF(1.1f, 1.1f));
            button.BackColor = Color.FromArgb(69, 94, 181);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 63, 205);
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 63, 205);
            button.TabStop = false;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Padding = new Padding(20, 0, 20, 0);
        }
        private static Image GetRadialGradientImage(Color color1, Color color2)
        {
            int width = 100; // Adjust as needed
            int height = 100; // Adjust as needed

            Bitmap image = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                Rectangle rect = new Rectangle(0, 0, width, height);
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                using (Brush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.Vertical))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(rect);
                        graphics.FillPath(brush, path);
                    }
                }
            }

            return image;
        }

    }
}
