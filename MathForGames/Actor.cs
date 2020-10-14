using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Actor
    {
        private char _icon = '#';
        private int _x = 10;
        private int _y = 5;

        public void Start()
        {

        }

        public void Update()
        {
            ConsoleKey keyPressed = Game.GetNextKey();
            switch (keyPressed)
            {
                case ConsoleKey.A:
                    _x--;
                    break;
                case ConsoleKey.D:
                    _x++;
                    break;
                case ConsoleKey.W:
                    _y--;
                    break;
                case ConsoleKey.S:
                    _y++;
                    break;
            }
            _x = Math.Clamp(_x, 0, Console.WindowWidth-1);
            _y = Math.Clamp(_y, 0, Console.WindowHeight-1);
        }

        public void Draw()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_icon);
        }

        public void End()
        {

        }
    }
}
