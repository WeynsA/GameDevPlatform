using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlatformMetEnemies
{
    static class CollisionHelper
    {
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Intersects(new Rectangle(r2.Left+8, r2.Top, r2.Width-16, 16)));
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Intersects(new Rectangle(r2.Left+8, r2.Bottom - 16, r2.Width-16, 16)));
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Intersects(new Rectangle(r2.Left, r2.Top + 16, 16, r2.Height - 32)));
        }
        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Intersects(new Rectangle(r2.Right - 16, r2.Top + 16, 16, r2.Height - 32)));
        }
    }
}
