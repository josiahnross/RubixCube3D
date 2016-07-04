using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RubixCube3D
{
    public class Camera
    {
        private Vector3 _position;
        private Vector3 _lookAt;
        private Vector3 _up;

        public Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                _view = Matrix.CreateLookAt(_position, _lookAt, _up);
            }
        }
        public Matrix View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
            }
        }
        public Matrix Projection
        {
            get
            {
                return _projection;
            }
            set
            {
                _projection = value;
            }
        }
        protected Matrix _view;
        protected Matrix _projection;

        public Camera(Vector3 position, Vector3 lookAt, Vector3 up, float nearPlane, float farPlane, float feildOfView, float aspectRatio)
        {
            _position = position;
            _lookAt = lookAt;
            _up = up;

            _view = Matrix.CreateLookAt(_position, _lookAt, _up);
            _projection = Matrix.CreatePerspectiveFieldOfView(feildOfView, aspectRatio, nearPlane, farPlane);
        }

        public void Rotate(Vector3 rotate)
        {
            Matrix positionMatrix = Matrix.CreateTranslation(_position) * Matrix.CreateFromYawPitchRoll(rotate.Y, rotate.X, rotate.Z);

            Position = positionMatrix.Translation;

        }
    }
}
