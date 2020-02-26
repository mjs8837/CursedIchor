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
    class Door : GameObject
    {
        public Door(Rectangle rectangle, Texture2D texture2D)
            : base(rectangle, texture2D)
        {
        }     
    }
}
