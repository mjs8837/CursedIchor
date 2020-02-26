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
    //Creating a delegate for card effects
    delegate void CardEffect(); // MP cost varies for each effect

    //Enum for card types
    public enum CardType
    {
        Heal,
        Attack,
        MeteorSwarm
    }

    class Cards : GameObject
    {
        //Fields
        //not all fields will be used with every card, some cards don't use some of the variables, but it just ignores it
        //private CardType name;
        private string name;
        private int cardNum;
        private bool active = false;
        private bool collidable = true;         //makes the object collidable, gets changed to false for some card types like Meteor
        private bool offensive = false;           //defaults it to false and gets turned on if ability is an offensive ability
        private bool usable = true;         
        private bool onUseTriggers = true;          //useful for allowing on use triggers to happen 
        private int damage;
        private int heal;
        private int healthCost;
        private int manaReturn;
        private int manaCost;
        private int startX;
        private int startY;
        private int endX;   //only used if applicable
        private int endY;   //only used if applicable


        //Properties
        public string Name
        {
            get { return name; }
        }
        public int CardNum
        {
            get { return cardNum; }
        }
        public int ManaCost
        {
            get { return manaCost; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public bool Usable
        {
            get { return usable; }
            set { usable = value; }
        }
        public bool OnUseTriggers
        {
            get { return onUseTriggers; }
            set { onUseTriggers = value; }
        }

        //Creating a constructor for the cards that inherit from the gameobject constructor
        public Cards(Rectangle rectangle, Texture2D texture2D, string name, int cardNum/*CardEffect someMethod*/)
            :base (rectangle, texture2D)
        {
            this.name = name;
            this.cardNum = cardNum;
            SetCardNumbers();
        }

        //setting damage based on name
        public void SetCardNumbers()
        {
            switch (name)
            {
                case "Fireball":
                    {
                        damage = 100;
                        manaCost = 1;
                        offensive = true;
                        break;
                    }

                case "Meteor":
                    {
                        damage = 100;
                        manaCost = 2;
                        offensive = true;
                        break;
                    }

                case "Heal":
                    {
                        heal = 200;
                        manaCost = 1;
                        break;
                    }

                case "Sacrifice":
                    {
                        healthCost = 100;
                        manaReturn = 5;
                        break;
                    }
            }     
        }

        //Creating an overriden update method for the cards
        public override void Update(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            Action(gameTime, check, enemyCheck);

            if (active && offensive)
            {
                Collide(enemyCheck);
                DeactivateIfNoCollision();
            }
        }

        public override void Action(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            //cardEffect();
            if(name == "Fireball" && active && usable)
            {
                if (onUseTriggers)
                {
                    check.Mana -= manaCost;
                    onUseTriggers = false;
                }

                FireballAction(gameTime, check, enemyCheck);
            }

            if(name == "Meteor" && active && usable)
            {
                if (onUseTriggers)
                {
                    check.Mana -= manaCost;
                    onUseTriggers = false;
                }
                
                collidable = false; //makes the meteor not collidable 
                MeteorAction(gameTime, check, enemyCheck);
            }

            if(name == "Heal" && active && usable)
            {
                HealAction(gameTime, check, enemyCheck);

                if (onUseTriggers)
                {
                    check.Mana -= manaCost;
                    onUseTriggers = false;
                }
            }

            if (name == "Sacrifice" && active && usable)
            {
                SacrificeAction(gameTime, check, enemyCheck);

                if (onUseTriggers)
                {
                    onUseTriggers = false;
                }
            }
        }

        public void Collide(List<Enemy> enemyCheck)
        {
            for (int i = 0; i < enemyCheck.Count; i++)
            {
                if (Rectangle.Intersects(enemyCheck[i].Rectangle) && enemyCheck[i].Alive && collidable)
                {
                    enemyCheck[i].TakeDamage(damage);

                    if (RectangleX <= enemyCheck[i].RectangleX)
                        enemyCheck[i].RectangleX += 50;

                    if (RectangleX >= enemyCheck[i].RectangleX)
                        enemyCheck[i].RectangleX -= 50;

                    active = false;
                    usable = false;
                }
            }
            
        }

        public void StartValues(GameObject check)
        {
            //makes the card object to be on top of the player
            this.startX = check.RectangleX;
            this.startY = check.RectangleY;
            this.RectangleX = startX;
            this.RectangleY = startY;

            //adjust the values dependant on the name of the card

            //Fireball card object adjustment
            if(name == "Fireball")
            {
                endX = startX + 700; //ends when fireball reaches 700 pixels away from starting point
                endY = startY;
            }

            //Meteor card object adjustment (makes the meteor start 300 pixels above the player's position
            if (name == "Meteor")
            {
                startY -= 300;
                this.RectangleY = startY;
                endX = startX + 220;        //ends when Meteor reaches 300 pixels away from player
                endY = startY + 220;
            }
        }

        private void FireballAction(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            this.RectangleX += 10; //moves the position of the fireball 10 pixels per frame

            //stops the fireball from going too far of a distance
            /*
            if(this.RectangleX - startX > 700)
                active = false;
            */
        }

        private void MeteorAction(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            this.RectangleX += 10; //moves the position of the meteor 10 pixels per frame
            this.RectangleY += 10; 

            //contact
            if(this.RectangleX == endX && this.RectangleY == endY)
                collidable = true;
        }

        private void HealAction(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            if (onUseTriggers)
                check.Health += heal;

            active = false;
        }

        private void SacrificeAction(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            if(onUseTriggers)
            {
                if (check.Health <= healthCost)
                    check.Health = 1;

                else
                    check.Health -= healthCost;
                check.Mana += manaReturn;
            }

            active = false;
        }

        private void DeactivateIfNoCollision()
        {
            if (this.RectangleX >= endX && this.RectangleY >= endY)
            {
                active = false;
                usable = false;
            }
        }

        public override void Scrolling(int positionChangeAmount)
        {
            RectangleX += positionChangeAmount;
            startX += positionChangeAmount;
            endX += positionChangeAmount;
        }
    }
}
