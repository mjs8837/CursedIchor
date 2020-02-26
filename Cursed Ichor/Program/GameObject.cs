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
    enum CharacterState
    {
        FaceLeft,
        WalkLeft,
        FaceRight,
        WalkRight,
    }
    class GameObject //parent class of Player, Enemy, and Cards
    {
        //Fields
        protected bool alive;
        protected Rectangle rectangle;
        protected Texture2D texture2D;
        int rectangleX;
        int rectangleY;
        protected CharacterState state;

        //Properties
        public CharacterState State
        {
            get { return state; }
            set { state = value; }
        }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
        }
        public int RectangleX
        {
            get
            {
                rectangleX = rectangle.X;
                return rectangle.X;
            }
            set
            {
                rectangle.X = value;
            }
        }
        public int RectangleY
        {
            get
            {
                rectangleY = rectangle.Y;
                return rectangle.Y;
            }
            set
            {
                rectangle.Y = value;
            }
        }

        //Creating a protected constructor for the base gameobject
        protected GameObject(Rectangle rectangle, Texture2D texture2D) //recieves hitbox and texture for all players
        {
            this.rectangle = rectangle;
            this.texture2D = texture2D;
            alive = true;
        }

        //Creating an abstract update method for the child classes to use
        public virtual void Update(GameTime gameTime, Player check, List<Enemy> checkEnemy)
        {

        }

        //Creating a draw method available for the child classes to use
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture2D, rectangle, Color.White);
        }

        //Creating an abstract action method for the child classes to use
        public virtual void Action(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {

        }

        public virtual void Scrolling(int positionChangeAmount)
        {
            RectangleX += positionChangeAmount;
        }


    }
}
