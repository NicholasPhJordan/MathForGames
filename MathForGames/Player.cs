using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) 
            : base(x, y, icon, color)
        { }

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White) 
            : base(x,y,icon,color)
        { }

        public override void Update(float deltaTime)
        {
            int Hori = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));

            int Vert = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            Velocity = new Vector2(Hori, Vert);
            Velocity = Velocity.Normalized * Speed;

            base.Update(deltaTime);
        }

    }
}
