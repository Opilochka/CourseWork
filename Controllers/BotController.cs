using CourseWork.Entities;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Controllers
{
    public class BotController
    {

        public static Image tankBot;
        public static List<Bot> bots;
        public static int direction;
        public static List<Point> spawns = new List<Point>()
        {
            new Point(320, 60),
            new Point(520, 60),
            new Point(120, 60)
        };
        public static int numberOfBots = 1;
        public static void Init()
        {
            tankBot = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\BotTank.png"));
            bots = new List<Bot>();
            SpawnBot();
        }

        public static void SpawnBot()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, spawns.Count);
            Point randomSpawn = spawns[randomIndex];
            Bot bot = new Bot(randomSpawn.X, randomSpawn.Y, Hero.runFramesVertical, Hero.runFramesHorizontal, tankBot, "Enemy", 1);
            bots.Add(bot);
            MapController.PlayersAndBots.Add(bot);

            direction = random.Next(0, 5);
            bot.SetDirection(direction);

            System.Timers.Timer timer = new System.Timers.Timer();
            Action action = () => {
                if (bot.IsAlive)
                {
                    if (!PhysicsController.IsCollide(bot))
                    {
                        bot.move();
                    }
                    bot.tick--;
                    if (bot.tick <= 0)
                    {
                        bot.tick = random.Next(bot.tickMaximum, bot.tickMaximum);
                        direction = random.Next(0, 5);
                        bot.SetDirection(direction);
                    }
                    int randShoot = random.Next(0, 130);
                    int randShoot2 = random.Next(0, 130);
                    if (randShoot == randShoot2)
                    {
                        bot.Shoot();
                    }
                }
                else
                {
                    bots.Remove(bot);
                    bot = null;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Elapsed += (sender, e) => action.Invoke();
            timer.Interval = Form1.timerInterval;
            timer.Start();
        }

        public static void DrawBots(Graphics g)
        {
            if (bots.Count < numberOfBots && (Account.AccountTank) > 0)
            {
                SpawnBot();
            }
            foreach (Bot bot  in bots)
            {
                bot.PlayAnimation(g);
            }
        }
    }
}
