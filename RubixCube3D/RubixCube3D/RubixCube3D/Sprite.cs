using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RubixCube3D
{
    public class Sprite
    {
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Color _tint;
        protected float _rotation;
        protected Vector2 _orgin;
        protected Vector2 _scale;
        protected Rectangle? _sourceRectangle;

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public Color Tint
        {
            get
            {
                return _tint;
            }
            set
            {
                _tint = value;
            }
        }
        public float Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }
        public Rectangle HitBox
        {
            get
            {
                if (_sourceRectangle.HasValue)
                {
                    return new Rectangle((int)(_position.X - _orgin.X), (int)(_position.Y - _orgin.Y), _sourceRectangle.Value.Width * (int)_scale.X, _sourceRectangle.Value.Height * (int)_scale.Y);
                }
                return new Rectangle((int)(_position.X - _orgin.X), (int)(_position.Y - _orgin.Y), _texture.Width * (int)_scale.X, _texture.Height * (int)_scale.Y);
            }
        }
        public Vector2 Orgin
        {
            get
            {
                return _orgin;
            }
            set
            {
                _orgin = value;
            }
        }
        public Vector2 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }

        public Rectangle? SourceRectangle
        {
            get
            {
                return _sourceRectangle;
            }
            set
            {
                 _sourceRectangle = value;
            }
        }


        public Sprite(Texture2D texture, Vector2 position, Color tint)
        {
            _texture = texture;
            _position = position;
            _tint = tint;
            _rotation = 0;
            _orgin = Vector2.Zero;
            _scale = Vector2.One;
            _sourceRectangle = null;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _sourceRectangle, _tint, _rotation, _orgin, _scale, SpriteEffects.None, 0);
        }
    }
}
