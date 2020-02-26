using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Weapon : GameObject
    {
        // Fields
        private string name;
        private int slashTime = 15;
        private int activeTime = 0;
        private int damage;
        KeyboardState kbState;
        KeyboardState previouskbState;

        // Constructor
        public Weapon(Rectangle rectangle, Texture2D texture2D, string name, int damage)
            : base(rectangle, texture2D)
        {
            this.alive = false;
            this.name = name;
            this.damage = damage;
        }

        // Methods
        public override void Update(GameTime gametime, Player check, List<Enemy> checkEnemy)
        {
            Action(gametime, check, checkEnemy);

            if (activeTime > 0)
                activeTime--;

            else
                activeTime = 0;

            if (activeTime == 0)
                this.alive = false;

            CollisionCheck(checkEnemy);
        }

        public override void Action(GameTime gametime, Player check, List<Enemy> checkEnemy)
        {
            kbState = Keyboard.GetState();

            if (SingleKeyPress(Keys.Space))
            {
                if (activeTime == 0)
                {
                    activeTime += slashTime;
                    this.alive = true;
                }
            }

            previouskbState = kbState;
        }

        public void CollisionCheck(List<Enemy> check)
        {
            for (int i = 0; i < check.Count; i++)
            {
                if (Rectangle.Intersects(check[i].Rectangle) && alive)
                {
                    check[i].TakeDamage(damage);

                    if (RectangleX <= check[i].RectangleX)
                        check[i].RectangleX += 50;

                    if (RectangleX >= check[i].RectangleX)
                        check[i].RectangleX -= 50;
                }
            }
        }

        private bool SingleKeyPress(Keys key)
        {
            if (kbState.IsKeyUp(key) && previouskbState.IsKeyDown(key))
                return true;

            else
                return false;
        }
    }
}
