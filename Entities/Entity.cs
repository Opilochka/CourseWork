using CourseWork.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Entities
{
    public class Entity
    {
        public static Image spriteSheetBullet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\Bullet.png"));
        public int posX;
        public int posY;

        public int dirX;
        public int dirY;
        public bool IsMoving;
        public int speedW;
        public int speedS;
        public int speedA;
        public int speedD;

        public int dirXstatic;
        public int dirYstatic;

        public int runFramesVertical;
        public int runFramesHorizontal;

        public int currentAnimation;
        public int currentFrame;
        public int currentLimit;
        public int flipY;
        public int flipX;

        public int size;
        public string team;
        public int type;

        public bool IsAlive;

        public Image spriteSheet;
        public readonly Dictionary<int, int> currentAnimation_currentLimit;

        public static readonly Dictionary<Keys, Action<Entity>> Keys_AnimationConfiguration = new Dictionary<Keys, Action<Entity>>()
            {
                {Keys.W, (Entity player) =>
                {
                    player.dirY = -player.speedW; 
                    player.flipY = 1;
                    player.dirXstatic = 0;
                    player.dirYstatic = -1;
                    player.SetAnimationConfiguration(0);
                } },
                {Keys.S, (Entity player) =>
                {
                    player.dirY = player.speedS;
                    player.flipY = -1;
                    player.dirXstatic = 0;
                    player.dirYstatic = 1;
                    player.SetAnimationConfiguration(0);
                } },
                {Keys.A, (Entity player) =>
                {
                    player.dirX = -player.speedA;
                    player.flipX = -1;
                    player.dirXstatic = -1;
                    player.dirYstatic = 0;
                    player.SetAnimationConfiguration(1);
                } },
                {Keys.D, (Entity player) =>
                {
                    player.dirX = player.speedD;
                    player.flipX = 1;
                    player.dirXstatic = 1;
                    player.dirYstatic = 0;
                    player.SetAnimationConfiguration(1);
                } }
            };
            public static readonly Dictionary<Keys, Action<Entity>> Entity_Actions = new Dictionary<Keys, Action<Entity>>()
            {
                {Keys.Space, (Entity player) =>
                {
                    if(Account.AccountBulets > 0)
                    {
                        Account.AccountBulets--;
                        player.Shoot();
                    }
                } }
            };


        public Entity(int posX, int posY, int runFramesVertical, int runFramesHorizontal, Image spriteSheet, string team, int type)
        {
            this.posX = posX;
            this.posY = posY;
            this.runFramesVertical = runFramesVertical;
            this.runFramesHorizontal = runFramesHorizontal;
            this.spriteSheet = spriteSheet;
            this.team = team;
            this.type = type;
            size = 48;
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = 1;
            dirXstatic = 0;
            dirYstatic = -1;
            flipY = 1;
            flipX = 1;
            speedW = 1;
            speedA = 1;
            speedD = 1;
            speedS = 1;
            IsAlive = true;
            currentAnimation_currentLimit = new Dictionary<int, int>()
            {
                {0, runFramesVertical},
                {1, runFramesHorizontal}
            };
            this.team = team;
        }

        public void move()
        {
            posX += dirX;
            posY += dirY;
        }

        public void PlayAnimation(Graphics g)
        {
            if (currentFrame < currentLimit - 1 && IsMoving)
            {
                currentFrame++;
            }
            else currentFrame = 0;
            g.DrawImage(spriteSheet, new Rectangle(new Point(posX - flipX * size / 2, posY - flipY * size / 2), new Size(flipX * size, flipY * size)), size * currentFrame, size * currentAnimation, size, size, GraphicsUnit.Pixel);
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;
            if (currentAnimation_currentLimit.ContainsKey(this.currentAnimation))
            {
                currentLimit = currentAnimation_currentLimit[this.currentAnimation];
            }
        }

        // Далее идут различные способы выстрела
        // (простое добавление пуль в список с дальнейшей итерацией по ним, потоки, таймеры, таски)
        // Чтобы изменить способ выстрела, нужно изменить используемый метод
        // в словаре Entity_Actions данного класса при нажатии на Space
        public void Shoot()
        {
            Bullet bullet = new Bullet(posX, posY, 0, 0, spriteSheetBullet, team, 2, new Point(dirXstatic, dirYstatic));
            BulletController.Bullets.Add(bullet);
            System.Timers.Timer timer = new System.Timers.Timer();
            Action action = () => {
                if (!PhysicsController.BulletIsCollide(bullet))
                {
                    bullet.move();
                }
                else
                {
                    try
                    {
                        BulletController.Bullets.Remove(bullet);
                        bullet = null;
                        timer.Stop();
                        timer.Dispose();
                    }
                    catch
                    {

                    }
                }
            };
            timer.Elapsed += (sender, e) => action.Invoke();
            timer.Interval = Form1.timerInterval;
            timer.Start();
        }

        
    }
}
