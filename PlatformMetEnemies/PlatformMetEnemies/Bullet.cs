using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformMetEnemies
{
    public class Bullet
    {

        private Vector2 bulletlocation;
        private Texture2D texture;
        Rectangle rectangle;

        bool isshot, iscollide;

        int width, height;
        float bulletSpeed;
        public Bullet(ContentManager Content)
        {
            width = 4;
            height = 4;
            iscollide = false;
            bulletSpeed = 6f;
            texture = Content.Load<Texture2D>("bullet");
        }

        public void Shoot(Vector2 entityposition, bool shootLinks)
        {
            rectangle = new Rectangle((int)entityposition.X, (int)entityposition.Y, width, height);
            isshot = true;

            if (shootLinks)
            {
                bulletlocation.X = entityposition.X + 17;
                bulletlocation.Y = entityposition.Y + 16;
                bulletSpeed = 10f;
            }
            else
            {
                bulletlocation.X = entityposition.X - 17;
                bulletlocation.Y = entityposition.Y + 16;
                bulletSpeed = -10f;
            }
        }

       public void Update(GameTime gameTime)
        {
            if (!iscollide)
            {
                bulletlocation.X += bulletSpeed;
                rectangle.X = (int)bulletlocation.X;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bulletlocation, Color.White);
        }

        public Vector2 Position
        {
            get { return bulletlocation; }
            set { bulletlocation = value; }
        }
        public int PositieX
        {
            get { return (int)bulletlocation.X; }
            set { bulletlocation.X = value; }
        }
        public int PositieY
        {
            get { return (int)bulletlocation.Y; }
            set { bulletlocation.Y = value; }
        }

        public bool Isshot
        {
            get { return isshot; }
            set { isshot = value; }
        }
        public bool Iscollide
        {
            get { return iscollide; }
            set { iscollide = value; }
        }
    }
}