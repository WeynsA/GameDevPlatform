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
    class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public Vector2 velocity;
        public Rectangle rectangle;

        float rotation = 0f;

        bool right, isDeath;
        float distance;
        float oldDistance;

        public Enemy(Vector2 newPosition, float newDistance, ContentManager Content)
        {
            position = newPosition;
            distance = newDistance;
            origin = new Vector2(0,0);
            oldDistance = distance;
            isDeath = false;
            texture = Content.Load<Texture2D>("Enemy");
        }

        float playerDistance;
        public void Update(Player player)
        {
            position += velocity;

            if (distance <= 0)
            {
                right = true;
                velocity.X = 1f;
            }
            else if (distance >= oldDistance)
            {
                right = false;
                velocity.X = -1f;
            }
            if (right) distance += 1; else distance -= 1;
            
            playerDistance = player.Position.X - position.X;

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (velocity.Y < 10)
                velocity.Y += 0.4f;

            if (playerDistance >= -200 && playerDistance <= 200)
            {
                if (playerDistance < -1)
                    velocity.X = -1f;
                else if (playerDistance > 1)
                    velocity.X = 1f;
                else if (playerDistance == 0)
                    velocity.X = 0f;
            }

            if (rectangle.Intersects(player.rectangle))
                Death();
  

        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.Y > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isDeath)
            {
                if (velocity.X > 0)
                    spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                else
                    spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public void Death()
        {
            isDeath = true;
            position.X = -50;
        }
    }
}
