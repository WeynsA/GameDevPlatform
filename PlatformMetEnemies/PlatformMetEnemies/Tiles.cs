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
    class Tiles
    {
        protected Texture2D texture;

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }


        public Tiles(int i, Rectangle newRectangle, ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }

        public void Draw(SpriteBatch spriteBatch/*, Texture2D collisionBoxTexture*/)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

            /*spriteBatch.Draw(collisionBoxTexture, new Rectangle(rectangle.Left+8, rectangle.Top, rectangle.Width-16, 16), Color.White); // TouchTopOf

            spriteBatch.Draw(collisionBoxTexture, new Rectangle(rectangle.Left+8, rectangle.Bottom-16, rectangle.Width-16, 16), Color.White); //Bottom

            spriteBatch.Draw(collisionBoxTexture, new Rectangle(rectangle.Left, rectangle.Top + 16, 16, rectangle.Height - 32), Color.White); //Left

            spriteBatch.Draw(collisionBoxTexture, new Rectangle(rectangle.Right - 16, rectangle.Top + 16, 16, rectangle.Height - 32), Color.White); //Right*/
        }
    }
 }
