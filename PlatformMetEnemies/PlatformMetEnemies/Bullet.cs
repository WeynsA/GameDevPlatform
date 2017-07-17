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

        private Vector2 position;
        private Texture2D texture;
        public Rectangle rectangle;

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
         public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                iscollide = true;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                iscollide = true;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                iscollide = true;
            }

            if (position.X < 0) position.X = 0;
            if (position.Y > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
        }

        public void Shoot(Vector2 entityposition, bool shootLinks)
        {
            rectangle = new Rectangle((int)entityposition.X, (int)entityposition.Y, width, height);
            isshot = true;

            if (shootLinks)
            {
                position.X = entityposition.X + 17;
                position.Y = entityposition.Y + 6;
                bulletSpeed = 10f;
            }
            else
            {
                position.X = entityposition.X - 17;
                position.Y = entityposition.Y + 6;
                bulletSpeed = -10f;
            }
        }


       public void Update(GameTime gameTime)
        {
            if (!iscollide)
            {
                position.X += bulletSpeed;
                rectangle.X = (int)position.X;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public int PositieX
        {
            get { return (int)position.X; }
            set { position.X = value; }
        }
        public int PositieY
        {
            get { return (int)position.Y; }
            set { position.Y = value; }
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