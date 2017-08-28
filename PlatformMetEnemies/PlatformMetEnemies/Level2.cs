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
    class Level2 : Map
    {
        public Level2(ContentManager Content, Camera camera, GraphicsDevice graphicsDevice) : base(Content, camera, graphicsDevice)
        {
            enemyList.Add(new Enemy(new Vector2(850, 20), 150, Content));
            enemyList.Add(new Enemy(new Vector2(650, 120), 150, Content));
            enemyList.Add(new Enemy(new Vector2(0, 350), 150, Content));
            GenerateWorld();
            isFinished = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = 0; i < enemyList.Count; i++)
            {
                foreach (var bullets in player.bulletListUpdate)
                {
                    if (enemyList[i].rectangle.Intersects(bullets.rectangle)) enemyList.RemoveAt(i);
                }
                enemyList[i].Update(player);

                foreach (var tile in CollisionTiles)
                {
                    enemyList[i].Collision(tile.Rectangle, Width, Height);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void GenerateWorld()
        {
            Generate(new int[,]{
            { 0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1},
            { 0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2},
            { 0,2,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2},
            { 0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,2,2,2,2,2,2,2,2},
            { 0,2,2,1,1,1,0,0,0,0,1,1,1,2,2,2,2,2,2,2,2,0,0,0,0,0,2,2,2,2,2,2,2,2},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            }, 64);
        }
    }
}
