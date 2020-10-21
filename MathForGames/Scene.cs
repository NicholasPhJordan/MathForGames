using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MathForGames
{
    class Scene
    {
        private Actor[] _actors;

        public bool Startred { get; private set; }

        public Scene()
        {
            _actors = new Actor[0];
        }

        public void AddActor(Actor actor)
        {
            //creating a new array with a size one greater than our old array
            Actor[] appendedArray = new Actor[_actors.Length + 1];
            //copies values from old array to new array
            for (int i = 0; i < _actors.Length; i++)
            {
                appendedArray[i] = _actors[i];
            }
            //set last value in new array to be the actor we want to add
            appendedArray[_actors.Length] = actor;
            //set old array to the values of the new array
            _actors = appendedArray;
        }

        public bool RemoveActor(int index)
        {
            if (index < 0 || index >= _actors.Length)
            {
                return false;
            }
            bool actorRemoved = false;
            //creates new array with a size one less than our old array
            Actor[] tempArray = new Actor[_actors.Length - 1];
            //creates varriable toa ccess tempArray index
            int j = 0;
            //copy values from old array to new
            for (int i = 0; i < _actors.Length; i ++)
            {
                //if current index is not the index that needs to be removed, 
                //add the value into the old array and increment j
                if(i != index)
                {
                    tempArray[i] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (_actors[i].Started)
                        _actors[i].End();
                }
            }
            //set the old array to be the tempArray
            _actors = tempArray;
            //return whether or not the removal was sucessful
            return actorRemoved;
        }

        public bool RemoveActor(Actor actor)
        {
            //check to see if the actor was null
            if (actor == null)
            {
                return false;
            }
            bool actorRemoved = false;
            //creates new array with a size one less than our old array
            Actor[] newArray = new Actor[_actors.Length - 1];
            //creates varriable toa ccess tempArray index
            int j = 0;
            //copy values from old array to new
            for (int i = 0; i < _actors.Length; i ++)
            {
                //if current index is not the index that needs to be removed, 
                //add the value into the old array and increment j
                if (actor != _actors[i])
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (actor.Started)
                        actor.End();
                }
            }
            //set the old array to be the tempArray
            _actors = newArray;
            //return whether or not the removal was sucessful
            return actorRemoved;
        }

        public virtual void Start()
        {
            Startred = true;
        }

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }

        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].End();
            }

            Startred = false;
        }
    }
}
