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
    class Enemy : GameObject
    {
        //Fields
        protected int health;
        protected int damage;
        protected Texture2D walk1;
        protected Texture2D walk2;

        int frame;
        double timeCounter;
        double fps;
        double timePerFrame;

        const int WalkFrameCount = 3;


        //Creating a constructor for the enemy that inherits from the gameobject constructor
        public Enemy(
            Rectangle rectangle, 
            Texture2D texture2D,
            Texture2D walk01,
            Texture2D walk02,
            int health, 
            int damage)
            : base(rectangle, texture2D)
        {
            this.health = health;
            this.damage = damage;
            walk1 = walk01;
            walk2 = walk02;

            fps = 10.0;
            timePerFrame = 1.0 / fps;
        }

       

        //Creating a method that checks for collision between the player and the enemy
        //Also implements knockback as well
        public void Collide(Player check)
        {
            if (Rectangle.Intersects(check.Rectangle) && alive && check.Immune == 0)
            {
                check.Health -= damage;

                if(RectangleX <= check.RectangleX)
                    check.RectangleX += 50;
                
                if (RectangleX >= check.RectangleX)
                    check.RectangleX -= 50;

                check.AddImmunity();

            }
        }

        public override void Draw(SpriteBatch sb)
        {
            switch (state)
            {
                case CharacterState.FaceLeft:
                    sb.Draw(
                        texture2D,
                        rectangle,
                        Color.White);
                    break;

                case CharacterState.WalkRight:
                    if (frame == 1)
                    {
                        sb.Draw(
                        walk1,
                        rectangle,
                        Color.White);
                    }
                    if (frame == 2)
                    {
                        sb.Draw(
                        walk2,
                        rectangle,
                        Color.White);
                    }
                    if (frame == 3)
                    {
                        sb.Draw(
                        texture2D,
                        rectangle,
                        Color.White);
                    }
                    if (frame > 3)
                    {
                        frame = 0;

                    }
                    break;

                case CharacterState.WalkLeft:
                    if (frame == 1)
                    {
                        sb.Draw(
                        walk2,
                        rectangle,
                        Color.White);
                    }
                    if (frame == 2)
                    {
                        sb.Draw(
                        walk1,
                        rectangle,
                        Color.White);
                    }
                    if (frame == 3)
                    {
                        sb.Draw(
                        texture2D,
                        rectangle,
                        Color.White);
                    }
                    if (frame > 3)
                    {
                        frame = 0;

                    }
                    break;

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

        //Creating a take damage method
        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
                alive = false;
        }
    }
}
