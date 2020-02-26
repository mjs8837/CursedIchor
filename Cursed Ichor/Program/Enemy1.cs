using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Program
{
    class Enemy1 : Enemy
    {
        //Fields
        int increment = 1;
        int windowWidth;
        protected CharacterState state;

        int frame;
        double timeCounter;
        double fps;
        double timePerFrame;

        const int WalkFrameCount = 3;

        //Creating a constructor for our first enemy type inheriting from the enemy class
        public Enemy1(Rectangle rectangle, Texture2D texture2D, Texture2D walk01, Texture2D walk02, int health, int damage, int windowWidth)
            : base(rectangle, texture2D, walk01, walk02, health, damage)
        {
            this.windowWidth = windowWidth;

            walk1 = walk01;
            walk2 = walk02;

            fps = 10.0;
            timePerFrame = 1.0 / fps;
        }

        //Properties
        public CharacterState State
        {
            get { return state; }
            set { state = value; }
        }

        //Creating an overriden update method for the new enemy to implement movement toward the player
        public override void Update(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            Player tmpCheck = null;

            if (check is Player)
            {
                tmpCheck = (Player)check;
                Chase(check.RectangleX, check.RectangleY);
                Collide(tmpCheck);
            }
        }

        

        public void UpdateAnimation(GameTime gameTime)
        {
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // If enough time has passed:
            if (timeCounter >= timePerFrame)
            {
                frame += 1;

                if (frame > WalkFrameCount)
                    frame = 1;

                timeCounter -= timePerFrame;

            }
        }

        //Creating a method to "chase" the player
        public void Chase(int playerPosX, int playerPosY)
        {
            if (playerPosX >= this.RectangleX && Math.Abs(playerPosX - RectangleX) <= windowWidth)
            {
                this.RectangleX += increment;
                this.State = CharacterState.WalkLeft;
            }

            if (playerPosX <= this.RectangleX && Math.Abs(playerPosX - RectangleX) <= windowWidth)
            {
                this.RectangleX -= increment;
                this.State = CharacterState.WalkRight;
            }

            if (playerPosY >= this.RectangleY && Math.Abs(playerPosY - RectangleY) <= 500)
            {
                this.RectangleY += increment;
                this.State = CharacterState.WalkLeft;
            }

            if (playerPosY <= this.RectangleY && Math.Abs(playerPosY - RectangleY) <= 500)
            {
                this.RectangleY -= increment;
                this.State = CharacterState.WalkRight;
            }

            // Implementing bounds for enemy movement

            if (RectangleY >= 255)
                RectangleY = 255;

            if (RectangleY < 80) 
                RectangleY = 80;
        }
    }
}
