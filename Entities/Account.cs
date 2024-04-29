using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Entities
{
    public class Account
    {
        public static Image life = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\life.png"));
        public static Image tank = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\Tank.png"));
        public static Image bullet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\Bul.png"));
        public static int AccountLife;
        public static int AccountBulets = 15;
        public static int AccountTank;
        // Get the elapsed time as a TimeSpan value.
        
        public static void DrawAccountTank(Graphics g)
        {
            String drawString = $"{AccountTank}";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(760.0F, 107.0F);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            g.DrawImage(tank, new Rectangle(new Point(700, 100), new Size(32, 32)),0, 0, 32, 32, GraphicsUnit.Pixel );
            g.DrawString(drawString, drawFont, drawBrush, drawPoint, drawFormat);
        }

        public static void DrawAccountBullets(Graphics g)
        {
            String drawString = $"{AccountBulets}";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(770.0F, 50.0F);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            g.DrawImage(bullet, new Rectangle(new Point(700, 50), new Size(32, 32)), 0, 0, 32, 32, GraphicsUnit.Pixel);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint, drawFormat);
        }

        public static void DrawAccountLife(Graphics g)
        {
            String drawString = $"{AccountLife}";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(760.0F, 150.0F);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            g.DrawImage(life, new Rectangle(new Point(710, 153), new Size(32, 32)), 0, 0, 32, 32, GraphicsUnit.Pixel);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint, drawFormat);
        }

        public static void Victory(Graphics g)
        {
            String drawString = $"Вы победили";
            String Team = $"Команда";
            String NameTeam = $"goldobster X opilochka";
            String Developers = $"Разработчики";
            String Max = $"Голдобин Максим";
            String Olya = $"Валиева Ольга";
            Font drawFont = new Font("Arial", 40);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            // Draw string to screen.
            g.Clear(Color.Black);
            g.DrawString(drawString, drawFont, drawBrush, 650.0F, 100.0F, drawFormat);
            g.DrawString(Team, drawFont, drawBrush, 615.0F, 200.0F, drawFormat);
            g.DrawString(NameTeam, drawFont, drawBrush, 760.0F, 250.0F, drawFormat);
            g.DrawString(Developers, drawFont, drawBrush, 670.0F, 300.0F, drawFormat);
            g.DrawString(Max, drawFont, drawBrush, 710.0F, 350.0F, drawFormat);
            g.DrawString(Olya, drawFont, drawBrush, 680.0F, 400.0F, drawFormat);
        }
        public static void Lose(Graphics g)
        {
            String drawString = $"Вас убили";
            Font drawFont = new Font("Arial", 40);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(650.0F, 250.0F);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            // Draw string to screen.
            g.Clear(Color.Black);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint, drawFormat);
        }
    }
}
