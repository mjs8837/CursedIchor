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
    class BossProjectile : GameObject
    {
        private bool active = false;        //sets active when it gets fired, turns false when it reaches offscreen or hits player
        int countdown = 600;                //cooldown of the projectile being fired
        int projectileCooldown = 600;       //resets the cooldown
        int damage = 100;           

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public BossProjectile(Rectangle rectangle, Texture2D texture2D)
            : base(rectangle, texture2D)
        {
        }

        //Update method that takes care of calling the methods that are constantly being checked
        public override void Update(GameTime gameTime, Player check, List<Enemy> checkEnemy)
        {
            base.Update(gameTime, check, checkEnemy);
            Action(gameTime, check, checkEnemy);
            Collide(check);
        }

        //Checking if the projectile is ready to fire, and checks its position too
        public override void Action(GameTime gametime, Player check, List<Enemy> checkEnemy)
        {
            countdown--;
            if (countdown <= 0)
            {
                ProjectileReset(checkEnemy);
                this.Active = true;
                countdown += projectileCooldown;
            }
            else
            {
                if (this.Active == true)
                    MoveProjectile();
            }

            if (this.RectangleX <= -100)
                this.Active = false;
        }

        //Moves the projectile in the negative x direction
        public void MoveProjectile()
        {
            this.RectangleX--;
        }

        //Resets the position of the projectile
        public void ProjectileReset(List<Enemy> checkEnemy)
        {
            this.RectangleX = checkEnemy[0].RectangleX;
            this.RectangleY = checkEnemy[0].RectangleY + 125;
        }

        //Checks if a collision occurs with a passed in player object and has knockback
        public void Collide(Player check)
        {
            if (Rectangle.Intersects(check.Rectangle) && alive && check.Immune == 0)
            {
                check.Health -= damage;

                if (RectangleX <= check.RectangleX)
                    check.RectangleX += 50;

                if (RectangleX >= check.RectangleX)
                    check.RectangleX -= 50;

                check.AddImmunity();
                this.active = false;
            }
        }
    }
}
