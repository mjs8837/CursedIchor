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
    class Player : GameObject
    {
        //Fields
        //private double cooldown; 
        private int mana; //may change to double
        private int health;
        private int maxHealth;
        private int maxMana;
        int windowWidth;
        int windowHeight;
        int immunityTime = 30;
        int immune = 0;
        Weapon sword;
        Texture2D swordTexture;
        Rectangle swordRec;
        Texture2D walk1;
        Texture2D walk2;

        int frame;              
        double timeCounter;     
        double fps;             
        double timePerFrame;    
            
        const int WalkFrameCount = 3;      

        //Properties
        public int Mana
        {
            get
            {
                return mana;
            }

            set
            {
                mana = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public int Immune
        {
            get
            {
                return immune;
            }
        }

        //Creating a constructor for the player that inherits from the gameobject constructor
        public Player(
            Rectangle rectangle, 
            Texture2D texture2D, 
            Texture2D walk01, 
            Texture2D walk02,
            Texture2D swordTexture,
            int mana, int health, int damage, 
            int windowWidth, int windowHeight)
            : base(rectangle, texture2D)
        {
            this.mana = mana;
            this.maxMana = mana;
            this.health = health;
            this.maxHealth = health;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.swordTexture = swordTexture;
            walk1 = walk01;
            walk2 = walk02;
            swordRec = new Rectangle(this.RectangleX, this.RectangleY, rectangle.Width* 2, rectangle.Height /4);
            sword = new Weapon(swordRec, swordTexture, "sword", damage);
            fps = 10.0;
            timePerFrame = 1.0 / fps;
        }

        //Creating an overriden update method that calls movement, immunity, and 
         public override void Update(GameTime gameTime, Player check, List<Enemy> checkEnemy)
        {
            Movement();
            ImmuneUpdate();
            SometimesUnlucky();
            sword.Update(gameTime, check, checkEnemy);
            sword.RectangleX = this.RectangleX + 40;
            sword.RectangleY = this.RectangleY + 55;

            if(mana > maxMana)
                mana = maxMana;
            if (mana < 0)
                mana = 0;

            if (health > maxHealth)
                health = maxHealth;
        }
        
        // Methods to handle immunity from colliding with enemies
        public void AddImmunity()
        {
            immune = immunityTime;
        }

        public void ImmuneUpdate()
        {
            if(immune > 0)
                immune--;
        }

        //determine if alive or dead
        public void SometimesUnlucky()
        {
            if (health <= 0)
                alive = false;
        }

        //animate
        public override void Draw(SpriteBatch sb)
        {
            if (sword.Alive)
                sword.Draw(sb);

            switch (state)
            {
                case CharacterState.FaceRight:
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

        public void Movement()
        {
            KeyboardState kbState;
            kbState = Keyboard.GetState();

            
            //Creating basic movement for the player
            int speed = 5;

            if (kbState.IsKeyDown(Keys.Up)) // up
            {
                RectangleY -= speed;
                this.State = CharacterState.WalkRight;
            }

            if (kbState.IsKeyDown(Keys.Down)) // down
            {
                RectangleY += speed;
                this.State = CharacterState.WalkLeft;
            }

            if (kbState.IsKeyUp(Keys.Left))
                this.State = CharacterState.FaceRight;


            //Implementing screenwrapping for the player
            if (RectangleX >= windowWidth-rectangle.Width)
                RectangleX = windowWidth-rectangle.Width;

            if (RectangleX < 0)
                RectangleX = 0;

            if (RectangleY >= 255) // Implementing bounds for player movement
                RectangleY = 255;

            if (RectangleY < 80) // Implementing bounds for player movement
                RectangleY = 80;
        }
    }
}
