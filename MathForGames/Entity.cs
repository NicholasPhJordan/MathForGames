using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Entity : Actor
    {

        private Actor _target;
        private Color _alertColor;
        private Sprite _sprite;

        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Entity(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.Green)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/enemy.png");
        }

        public Entity(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.Green)
            : base(x, y, rayColor, icon, color)
        {
            _alertColor = Color.RED;
            _sprite = new Sprite("Images/enemy.png");
        }

        public bool CheckTargetInSight(float maxAngle, float maxDistance)
        {
            if (Target == null)
                return false;

            //find vector reresenting distance between actor and target
            Vector2 direction = Target.LocalPosition - LocalPosition;
            //Get magnitude of the distance vector
            float distance = direction.Magnitude;
            //Use inverse cosine to find angle of dot product in radians 
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            if (angle <= maxAngle && distance <= maxDistance)
                return true;

            return false;
        }

        public override void Update(float deltaTime)
        {
            if (CheckTargetInSight(0.75f, 5))
            {
                _rayColor = Color.RED;
            }
            else
            {
                _rayColor = Color.BLUE;
            }

            base.Update(deltaTime);


            _sprite.Draw(_globalTransform);
        }

    }
}
