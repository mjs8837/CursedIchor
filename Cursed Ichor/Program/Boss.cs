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
    class Boss : Enemy
    {
        int increment = 10;
        BossProjectile bossProj;

        //Creating a contructor for the boss that inherits from the enemy class
        public Boss(Rectangle rectangle, Texture2D texture2D, Texture2D walk01, Texture2D walk02, Player user, int health, int damage, Texture2D projTexture, Rectangle projRect)
            : base(rectangle, texture2D, walk01, walk02, health, damage)
        {
            rectangle.Y -= 200;
            bossProj = new BossProjectile(projRect, projTexture);
            bossProj.RectangleX = this.RectangleX;
            bossProj.RectangleY = this.RectangleY + 125;
        }

        // Methods
        public override void Action(GameTime gametime, Player check, List<Enemy> checkEnemy)
        {
        }

        public void Movement(int playerPosY)
        {
            if (playerPosY >= this.RectangleY + 100)
                this.RectangleY += increment;

            if (playerPosY <= this.RectangleY + 100)
                this.RectangleY -= increment;
        }
        

        public override void Draw(SpriteBatch sb)
        {
            if(bossProj.Active == true)
                bossProj.Draw(sb);

            base.Draw(sb);
        }

        public override void Update(GameTime gameTime, Player check, List<Enemy> enemyCheck)
        {
            Player tmpCheck = null;
            this.RectangleX = 600;
            if (check is Player)
            {
                tmpCheck = (Player)check;
                Movement(check.RectangleY);
                Collide(tmpCheck);
                bossProj.Update(gameTime, check, enemyCheck);
            }
        }
    }
}
