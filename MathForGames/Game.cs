﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private static Scene[] _scenes;
        private static int _currentSceneIndex;

        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        public static Scene GetScene(int index)
        {
            return _scenes[index];
        }

        public static int AddScene(Scene scene)
        {
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            tempArray[_scenes.Length] = scene;
            _scenes = tempArray;

            return _scenes.Length;
        }

        public static bool RemoveScene(Scene scene)
        {
            if (scene == null)
            {
                return false;
            }

            bool sceneRemoved = false;

            Scene[] tempArray = new Scene[_scenes.Length - 1];

            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        public static void SetCurrentScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return;

            _currentSceneIndex = index;
        }

        public static bool GetKeyDown(int key)
        {
            bool testBool = true;
            int test = Convert.ToInt32(testBool);
            return Raylib.IsKeyDown((KeyboardKey)key);
        }

        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }

        public Game()
        {
            _scenes = new Scene[0];
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            //creates new raylib window
            Raylib.InitWindow(1024, 760, "Math For Games");
            Raylib.SetTargetFPS(60);

            //Set up console window
            Console.CursorVisible = false;
            Console.Title = "Math for Games";

            //create new scene for our actors to exist in
            Scene scene1 = new Scene();

            //creates new actors
            Actor actor = new Actor(0, 0, Color.GREEN, '■', ConsoleColor.Green);
            actor.Velocity.X = 1;
            scene1.AddActor(actor);
            Player player = new Player(2, 1, Color.RED, '@', ConsoleColor.Red);
            scene1.AddActor(player);

            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(scene1);

            SetCurrentScene(startingSceneIndex);
        }


        //Called every frame.
        public void Update()
        {
            if (!_scenes[_currentSceneIndex].Startred)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update();
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }


        //Called when the game ends.
        public void End()
        {
           if (_scenes[_currentSceneIndex].Startred) 
                _scenes[_currentSceneIndex].End();
        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver && !Raylib.WindowShouldClose())
            {
                Update();
                Draw();
                while (Console.KeyAvailable) 
                    Console.ReadKey(true);
                Thread.Sleep(150);
            }

            End();
        }
    }
}
