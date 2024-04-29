using CourseWork.Controllers;
using CourseWork.Entities;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Form1 : Form
    {
        public Image tankSheet;
        public Entity player;
        public Keys current_key;
        public Keys current_action_key;
        public static int timerInterval = 10;
        public Timer timer1 = new Timer();
        public Form1()
        {
            
            InitializeComponent();
            
            timer1.Interval = timerInterval;
            timer1.Tick += new EventHandler(Update);

            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);

            Init();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == current_key && player.IsMoving)
            {
                player.dirX = 0;
                player.dirY = 0;
                player.IsMoving = false;
                current_key = new Keys();
                player.SetAnimationConfiguration(player.currentAnimation);
            }
            if (e.KeyCode == current_action_key)
            {
                current_action_key = new Keys();
            }
        }
        public void OnPress(object sender, KeyEventArgs e)
        {
            if (Entity.Keys_AnimationConfiguration.ContainsKey(e.KeyCode) && !player.IsMoving)
            {
                Entity.Keys_AnimationConfiguration[e.KeyCode].Invoke(player);
                current_key = e.KeyCode;
                player.IsMoving = true;
            }
            if (Entity.Entity_Actions.ContainsKey(e.KeyCode) && current_action_key != e.KeyCode)
            {
                current_action_key = e.KeyCode;
                Entity.Entity_Actions[e.KeyCode].Invoke(player);
            }
            
        }
        
        public void Init()
        {
            MapController.Init();
            BotController.Init();
            this.Width = MapController.GetWidth();
            this.Height = MapController.GetHeight();
            tankSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\MainTank.png"));
            player = new Entity(320, 580, Hero.runFramesVertical, Hero.runFramesHorizontal, tankSheet, "Ally", 1);
            MapController.PlayersAndBots.Add(player);
            timer1.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            //BulletController.MoveBullets();
            if (!PhysicsController.IsCollide(player))
            {

                if (player.IsMoving)
                {
                    player.move();
                    
                }
            }
            Invalidate();
        }


        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(Account.AccountLife > 0)
            {
                if (Account.AccountTank > 0)
                {
                    MapController.DrawMap(g);
                    BulletController.DrawBullets(g);
                    player.PlayAnimation(g);
                    BotController.DrawBots(g);
                    Account.DrawAccountBullets(g);
                    Account.DrawAccountTank(g);
                    Account.DrawAccountLife(g);
                }
                else
                {
                    Account.Victory(g);
                }
            }
            else
            {
                Account.Lose(g);
            }
            

        }

        //Закрытие формы
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
} 
