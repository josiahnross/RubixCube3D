using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RubixCube3D
{
    public class SidePiece
    {
        protected Panel panel1;
        protected Panel panel2;

        protected Vector3 _center;
        protected float _size;
        public SidePiece(Vector3 center, float size, Texture2D texture, Color color1, Color color2, Camera camera, GraphicsDevice graficsDevice)
        {
            _center = center;
            _size = size;
            panel1 = new Panel(new Vector3(center.X, center.Y, center.Z + size / 2), size, size, texture, color1,  camera, graficsDevice);
            panel2 = new Panel(new Vector3(center.X * -1, center.Y, center.Z + size / 2), size, size, texture, color2, camera, graficsDevice);
            panel2.Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(90));
        }
        public virtual void Update()
        {
            panel1.Update();
            panel2.Update();
        }
        public virtual void Draw()
        {
            panel1.Draw();
            panel2.Draw();
        }
        public void Rotate(Vector3 rotation)
        {

            panel1.Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            panel1.Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            panel1.Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            panel2.Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            panel2.Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            panel2.Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));

        }
        
    }
}
