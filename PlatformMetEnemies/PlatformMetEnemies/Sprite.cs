using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformMetEnemies
{
    public class Sprite: ICloneable
    {
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        public Input Input;
        public Sprite Parent;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public float Speed = 1f;
        public float _rotation;
        public float LinearVelocity = 4f;
        public float RotationVelocity = 3f;
        public float LifeSpan = 0f;

        public bool isRemoved = false;

        public Vector2 Velocity;
        public Vector2 Origin;
        public Vector2 Direction;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right...");
        }

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Velocity.Y = -10f; ;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite (Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update(GameTime gametime, List<Sprite> sprites)
        {
            Move();

            SetAnimations();

            _animationManager.Update(gametime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X < 0)
                _animationManager.Play(_animations["WalkRight"]);
            else if (Velocity.X > 0)
                _animationManager.Play(_animations["WalkLeft"]);
            else if (Velocity.Y < 0)
                _animationManager.Play(_animations["WalkUp"]);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }    
}
