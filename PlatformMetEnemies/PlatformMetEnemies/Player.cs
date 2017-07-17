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
    class Player
    {
        List<Bullet> bulletListUpdate;
        double lastBulletShot;
        double timer;
        private float shootInterval = 0.33f;

        private Texture2D textureRight, textureLeft;
        private Vector2 position;
        public Vector2 velocity;
        public Rectangle rectangle;

        private Animation walkLeft, walkRight, currentAnimation;

        public int invulnerability;
        public int health;

        private bool hasJumped = false;
        private bool walkRightBl, walkLeftBl;

        ContentManager Content;
        public Vector2 Position
        {
            get { return position; }
        }
        public Player(int newHealth, ContentManager Content)
        {
            this.Content = Content;

            health = newHealth;
            position = new Vector2(100, 50);

            rectangle = new Rectangle((int)position.X, (int)position.Y, 20, 36);

            walkLeft = new Animation();
            walkLeft.AddFrame(new Rectangle(0, 0, 20, 29), TimeSpan.FromSeconds(.08));
            walkLeft.AddFrame(new Rectangle(20, 0, 20, 29), TimeSpan.FromSeconds(.08));
            walkLeft.AddFrame(new Rectangle(40, 0, 20, 29), TimeSpan.FromSeconds(.08));
            walkRight = new Animation();
            walkRight.AddFrame(new Rectangle(0, 0, 20, 29), TimeSpan.FromSeconds(.08));
            walkRight.AddFrame(new Rectangle(20, 0, 20, 29), TimeSpan.FromSeconds(.08));
            walkRight.AddFrame(new Rectangle(40, 0, 20, 29), TimeSpan.FromSeconds(.08));

            currentAnimation = walkRight;

            //Bulets
            bulletListUpdate = new List<Bullet>();

        }

        public void Load(ContentManager Content)
        {
            textureRight = Content.Load<Texture2D>("walkRight");
            textureLeft = Content.Load<Texture2D>("walkLeft");
        }

        public void Update(GameTime gameTime)
        {
            timer = gameTime.TotalGameTime.TotalSeconds;

            position += velocity;

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;

            Input(gameTime);
            if (invulnerability > 0) invulnerability -= 1;

            if (velocity.Y < 10)
                velocity.Y += 0.4f;

            if (walkLeftBl)
                currentAnimation = walkLeft;

            if (walkRightBl)
                currentAnimation = walkRight;

            currentAnimation.Update(gameTime);

            foreach (var bullets in bulletListUpdate)
            {
                bullets.Update(gameTime);
            }
        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (shootInterval < (timer - lastBulletShot) || lastBulletShot == 0)
                 {
                    bulletListUpdate.Add(new Bullet(Content));
                    bulletListUpdate.Last().Shoot(position, walkLeftBl);
                    lastBulletShot = gameTime.TotalGameTime.TotalSeconds;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                walkLeftBl = true;
                walkRightBl = false;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                walkRightBl = true;
                walkLeftBl = false;
            }
            else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -10f;
                hasJumped = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
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
            var sourceRectangle = currentAnimation.CurrentRectangle;
            if (health > 0)
                 if (invulnerability % 25 <= 20)
                {
                    if (walkRightBl)
                        spriteBatch.Draw(textureRight, position, sourceRectangle, Color.White);
                    else spriteBatch.Draw(textureLeft, position, sourceRectangle, Color.White);
                }

            foreach (var bullets in bulletListUpdate)
            {
                bullets.Draw(spriteBatch);
            }


        }

        internal void Hit()
        {
            if (invulnerability == 0)
            {
                health -= 30;
                invulnerability = 150;
            }
        }
    }
}
