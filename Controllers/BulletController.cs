using CourseWork.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Controllers
{
    public static class BulletController
    {
        public static List<Bullet> Bullets = new List<Bullet>();
        public static List<Bullet> BulletsToDelete = new List<Bullet>();

        public static void MoveBullets()
        {
            
            Parallel.For(0, Bullets.Count, i =>
            {
                Bullet bullet = Bullets[i];
                if (!PhysicsController.BulletIsCollide(bullet))
                {
                    bullet.move();
                }
                else
                {
                    BulletsToDelete.Add(bullet);
                }
            });
            if (BulletsToDelete.Count > 0)
            {
                DeleteBullets();   
            }
            
            
        }

        public static void DeleteBullets()
        {
            if (BulletsToDelete.Count > 0)
            {
                for (int i = 0; i < BulletsToDelete.Count; i++)
                {
                    Bullet bullet = BulletsToDelete[i];
                    Bullets.Remove(bullet);
                    bullet = null;
                }
                BulletsToDelete.Clear();
            }
        }
        public static void DrawBullets(Graphics g)
        {
            for (int i=0; i < Bullets.Count; i++)
            {
                try
                {
                    Bullet bullet = Bullets[i];
                    bullet.DrawBullet(g);
                }
                catch(Exception e)
                {
                    continue;
                }
            }
        }
    }
}
