using CourseWork.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace CourseWork.Entities
{
    public class Bot : Entity
    {
        public int tick;
        public int tickMinimum = 30;
        public int tickMaximum = 50;
        public Bot(int posX, int posY, int runFramesVertical, int runFramesHorizontal, Image spriteSheet, string team, int type) : base(posX, posY, runFramesVertical, runFramesHorizontal, spriteSheet, team, type)
        {
            this.posX = posX;
            this.posY = posY;
            this.runFramesVertical = runFramesVertical;
            this.runFramesHorizontal = runFramesHorizontal;
            size = 48;
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = 1;
            flipY = 1;
            flipX = 1;
            Random random = new Random();
            tick = random.Next(tickMinimum, tickMaximum);
        }

        public void SetDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    dirX = 0;
                    dirY = 0;
                    IsMoving = false;
                    break;
                case 1:
                    IsMoving = true;
                    dirX = 0;
                    dirY = -speedW;
                    flipY = 1;
                    flipX = 1;
                    dirXstatic = 0;
                    dirYstatic = -1;
                    SetAnimationConfiguration(0);
                    break;

                case 2:
                    IsMoving = true;
                    dirX = 0;
                    dirY = speedS;
                    flipY = -1;
                    flipX = 1;
                    dirXstatic = 0;
                    dirYstatic = 1;
                    SetAnimationConfiguration(0);
                    break;

                case 3:
                    IsMoving = true;
                    dirX = -speedD;
                    dirY = 0;
                    flipX = -1;
                    flipY = 1;
                    dirXstatic = -1;
                    dirYstatic = 0;
                    SetAnimationConfiguration(1);
                    break;

                case 4:
                    IsMoving = true;
                    dirX = speedA;
                    dirY = 0;
                    flipX = 1;
                    flipY = 1;
                    dirXstatic = 1;
                    dirYstatic = 0;
                    SetAnimationConfiguration(1);
                    break;
            }
        }
    }
}
