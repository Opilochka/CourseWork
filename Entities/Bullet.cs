using CourseWork.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Entities
{
    public class Bullet : Entity
    {
        public static readonly Dictionary<int, int> DirX_CurrentFrame = new Dictionary<int, int>()
        {
            {1, 0},
            {-1, 0 },
            {0, 1 }
        };
        public static readonly Dictionary<Point, Point> DirX_Flip = new Dictionary<Point, Point>()
        {
            {new Point(1, 0), new Point(1, 1)},
            {new Point(-1, 0), new Point(-1, 1)},
            {new Point(0, 1), new Point(1, -1)},
            {new Point(0, -1), new Point(1, 1)},
        };

        public Bullet(int posX, int posY, int runFramesVertical, int runFramesHorizontal, Image spriteSheet, string team, int type, Point dir) : base(posX, posY, runFramesVertical, runFramesHorizontal, spriteSheet, team, type)
        {
            size = 6;
            speedA = 5;
            currentAnimation = 0;
            dirX = dir.X * speedA;
            dirY = dir.Y * speedA;
            currentFrame = DirX_CurrentFrame[dir.X];
            IsMoving = true;
            Point flips = DirX_Flip[dir];
            flipX = flips.X;
            flipY = flips.Y;
        }
        public void DrawBullet(Graphics g)
        {
            g.DrawImage(spriteSheet, new Rectangle(new Point(posX - flipX * size / 2, posY - flipY * size / 2), new Size(flipX * size, flipY * size)), size * currentFrame, size * currentAnimation, size, size, GraphicsUnit.Pixel);
        }
    }
}
