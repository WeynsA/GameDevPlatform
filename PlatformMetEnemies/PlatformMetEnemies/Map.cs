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
    abstract class Map
    {

        protected Player player;
        Rectangle healthRectangle;
        protected Camera camera;
        protected Tiles tile;
        int canvasWidth, canvasHeight;
        Rectangle pausedRectangle;
        protected List<Enemy> enemyList;
        protected Texture2D healthTexture;
        protected ContentManager Content;
        protected List<Tiles> collisionTiles = new List<Tiles>();
        private int width, height;

        public List<Tiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }

        protected Map(ContentManager Content, Camera camera, GraphicsDevice graphicsDevice)
        {

            player = new Player(100, Content);
            player.Load(Content);
            canvasWidth = graphicsDevice.Viewport.Bounds.Width;
            canvasHeight = graphicsDevice.Viewport.Bounds.Height;
            enemyList = new List<Enemy>();
            this.Content = Content;
            healthTexture = Content.Load<Texture2D>("Health");
            this.camera = camera;
        }

        public virtual void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            healthRectangle = new Rectangle((int)camera.centre.X - (canvasWidth / 2)+80, (int)camera.centre.Y - (canvasHeight/3)-120, player.health, 20);

            foreach (var tile in CollisionTiles)
            {
                player.Collision(tile.Rectangle, Width, Height);
                camera.Update(player.Position, Width, Height);
                foreach (Bullet bullets in player.bulletListUpdate)
                {
                    bullets.Collision(tile.Rectangle, Width, Height);
                }
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            player.Draw(spriteBatch);
            foreach (var enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            foreach (Tiles tile in collisionTiles)
                tile.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x<map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                        collisionTiles.Add(new Tiles(number, new Rectangle(x * size, y * size, size, size), Content));

                    width = (x+1) * size;
                    height = (y+1) * size;
                }
        } 
    }
}
