using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlatformMetEnemies
{
    public class Ship : Sprite
    {
        public Bullet Bullet;

        public Ship(Texture2D texture)
            :base(texture)
        {

        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                AddBullet(sprites);
            }
        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 3;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }
    }
}
