using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        protected char _icon = ' ';
        protected Vector2 _velocity;
        protected Matrix3 _transform;
        private Matrix3 _translation;
        private Matrix3 _rotation;
        private Matrix3 _scale;
        protected ConsoleColor _color;
        protected Color _rayColor;

        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get { return new Vector2(_transform.m11, _transform.m21); }
        }

        public Vector2 Position
        {
            get { return new Vector2(_transform.m13, _transform.m23); }
            set
            {
                _transform.m13 = value.X;
                _transform.m23 = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }

        public void SetTranslation(Vector2 position)
        {
            _translation.m13 = position.X;
            _translation.m23 = position.Y;
        }

        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m21 = -(float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale.m11 = _transform.m11 * x;
            _scale.m21 = _transform.m21 * y;
        }

        private void UpdateTransform()
        {
            _transform = (_translation * _rotation * _scale);
        }

        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _transform = new Matrix3();
            Position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
        }

        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, icon, color)
        {
                _transform = new Matrix3();
                _rayColor = rayColor;
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            Position += _velocity * deltaTime;
            Position.X = Math.Clamp(Position.X, 0, Console.WindowWidth - 1);
            Position.Y = Math.Clamp(Position.Y, 0, Console.WindowHeight - 1);
        }

        public virtual void Draw()
        {
            Raylib.DrawText(_icon.ToString(), (int)(Position.X * 32), (int)(Position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X + Forward.X) * 32),
                (int)((Position.Y + Forward.Y) * 32),
                Color.WHITE
            );

            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)Position.X, (int)Position.Y);
            Console.Write(_icon);
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }
    }
}
