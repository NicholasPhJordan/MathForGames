using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames3D
{
    class Actor
    {
        protected Matrix3 _globalTransform = new Matrix3();
        protected Matrix3 _localTransform = new Matrix3();
        private Matrix4 _translation = new Matrix4();
        private Matrix4 _rotation = new Matrix4();
        private Matrix4 _scale = new Matrix4();

        public bool Started { get; private set; }

        public Vector3 WorldPosition
        {
            get { return new Vector3(_globalTransform.m13, _globalTransform.m23, _globalTransform.m33); }
        }

        public Vector3 LocalPosition
        {
            get { return new Vector3(_localTransform.m13, _localTransform.m23, _localTransform.m33); }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
                _translation.m33 = value.Z;
            }
        }
    }
}
