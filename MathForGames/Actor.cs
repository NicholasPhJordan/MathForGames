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
        protected Matrix3 _globalTransform;
        protected Matrix3 _localTransform;
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        private float _collisionRadius;

        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get { return new Vector2(_localTransform.m11, _localTransform.m21); }
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(_globalTransform.m13, _globalTransform.m23); }
        }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.m13, _localTransform.m23); }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
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

        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
        }

        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, icon, color)
        {
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            _rayColor = rayColor;
        }

        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];
            
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        }

        public bool RemoveChild(Actor child)
        {
            bool childRemoved = false;

            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }
            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }

        /// <summary>
        /// checks to see if this actor overlaps another
        /// </summary>
        /// <param name="other">actor that this actor is checking collision against</param>
        /// <returns></returns>
        public bool CheckCollision(Actor other)
        {
            return false;
        }

        /// <summary>
        /// called whenever collision occurs betweenthis actor and another
        /// use to define game logic for this actors collision
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(Actor other)
        {

        }

        public void SetTranslation(Vector2 position)
        {
            _translation.m13 = position.X;
            _translation.m23 = position.Y;
        }

        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m21 = -(float)Math.Sin(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }

        public void Rotate(float radians)
        {
            _rotationAngle += radians;
            SetRotation(_rotationAngle);
        }

        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 =  y;
        }

        private void UpdateTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            LocalPosition += _velocity * deltaTime;
            LocalPosition.X = Math.Clamp(LocalPosition.X, 0, Console.WindowWidth - 1);
            LocalPosition.Y = Math.Clamp(LocalPosition.Y, 0, Console.WindowHeight - 1);
        }

        public virtual void Draw()
        {

            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)LocalPosition.X, (int)LocalPosition.Y);
            Console.Write(_icon);
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }
    }
}
