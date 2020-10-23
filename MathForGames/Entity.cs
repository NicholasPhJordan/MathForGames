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

        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Entity(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.Green)
            : base(x, y, icon, color)
        { }

        public Entity(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.Green)
            : base(x, y, rayColor, icon, color)
        {
            _alertColor = Color.RED;
        }

        public bool CheckTargetInSight(float maxAngle, float maxDistance)
        {
            if (Target == null)
                return false;

            //find vector reresenting distance between actor and target
            Vector2 direction = Target.Position - Position;
            //Get magnitude of the distance vector
            float distance = direction.Magnitude;
            //Use inverse cosine to find angle of dot product in radians 
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            // Draw partial circle
            Raylib.DrawCircleSector(
                new System.Numerics.Vector2(_position.X * 32, _position.Y * 32),
                maxDistance * 32,
                (int)((180 / Math.PI) * -maxAngle) + 90,
                (int)((180 / Math.PI) * maxAngle) + 90,
                10,
                Color.GREEN);

            if (angle <= maxAngle && distance <= maxDistance)
            {
                return true;
            }

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
        }

        public override void Draw()
        {
            

            base.Draw();
        }

    }
}
