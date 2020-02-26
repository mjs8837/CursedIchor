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
    class BackgroundElement : GameObject
    {
        int objectSize;
        public BackgroundElement(Rectangle rectangle, Texture2D texture2D)
            :base (rectangle, texture2D)
        {
            objectSize = rectangle.Width-10;
        }

        public override void Update(GameTime gameTime, Player check, List<Enemy> checkEnemy)
        {
            ResetLocation();
        }
        
        public void ResetLocation()
        {
            //if statement to see when the whole object is off the left side of the screen
            if (RectangleX < -objectSize)
                //sets the position to the end of the screen
                RectangleX = objectSize;

            //if statement to see when the whole object is off the right side of the screen
            if (RectangleX > objectSize)
                //sets the position to the end of the screen
                RectangleX = -objectSize;
        }
    }
}
