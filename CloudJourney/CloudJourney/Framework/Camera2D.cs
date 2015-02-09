using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework
{
    class Camera2D
    {
        protected float _zoom; // Camera Zoom
        private Matrix _transform; // Matrix Transform
        private Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation
        public float xvelocity { get; private set; }
        public float rumble { get; set; }
        private float lastx;

        public static Camera2D cam = new Camera2D();

        public Camera2D()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            lastx = _pos.X;
            _pos += amount;
            xvelocity = _pos.X - lastx;
        }

        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set
            {
                lastx = _pos.X;
                _pos = value;
                xvelocity = _pos.X - lastx;
            }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            int ox = Cloud.randCloud.Next((int)Math.Ceiling(rumble));
            int oy = Cloud.randCloud.Next((int)Math.Ceiling(rumble));
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X + ox, -_pos.Y + oy, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(Cloud.screenWidth * 0.5f, Cloud.screenHeight * 0.5f, 0));
            return _transform;
        }
    }
}
