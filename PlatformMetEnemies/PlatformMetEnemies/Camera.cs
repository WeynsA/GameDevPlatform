using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformMetEnemies
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        public Vector2 centre;
        private Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width / 2)
                centre.X = viewport.Width / 2 + 70;
           /* else if (position.X > xOffset - (viewport.Width / 2))
                centre.X = xOffset - (viewport.Width / 2);*/
            else centre.X = position.X +70;

            if (position.Y < viewport.Height / 2)
                centre.Y = viewport.Height / 2;
            else if (position.Y > yOffset - (viewport.Height / 2)+25)
                centre.Y = yOffset - (viewport.Height / 2);
            else centre.Y = viewport.Height/2;

            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0));
        }
    }
}
