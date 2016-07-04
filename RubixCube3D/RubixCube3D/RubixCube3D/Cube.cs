using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RubixCube3D
{
    public class Cube
    {
        protected List<Panel> panels = new List<Panel>();
        GraphicsDevice _graficsDevice;
        Camera _camera;
        public List<Panel> Panels
        {
            get
            {
                return panels;
            }
            set
            {
                panels = value;
            }
        }
        public Cube(Vector3 center, Color front, Color back, Color left, Color right, Color top, Color bottom, Texture2D texture,float width, float height, float depth, Camera camera, GraphicsDevice graficsDevice)
        {
            _camera = camera;
            _graficsDevice = graficsDevice;
            panels.Add(new Panel(new Vector3(center.X, center.Y, center.Z + depth / 2), width, height, texture, front, camera, graficsDevice));
            panels.Add(new Panel(new Vector3(center.X, center.Y, center.Z + depth / 2), width, height, texture, back, camera, graficsDevice));
            panels[panels.Count() - 1].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(180));

            panels.Add(new Panel(new Vector3(center.X, center.Y, center.Z + depth / 2), width, height,  texture, right,camera, graficsDevice));
            panels[panels.Count() - 1].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-90));
            panels.Add(new Panel(new Vector3(center.X, center.Y, center.Z + depth / 2), width, height, texture, left, camera, graficsDevice));
            panels[panels.Count() - 1].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(90));

            panels.Add(new Panel(new Vector3(center.X, center.Y, center.Z + depth / 2), width, depth, texture, top, camera, graficsDevice));
            panels[panels.Count() - 1].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            panels.Add(new Panel(new Vector3(center.X, center.Y, center.Z + depth / 2), width, height, texture, bottom, camera, graficsDevice));
            panels[panels.Count() - 1].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(90));
        }
        public void Update()
        {
            for (int count = 0; count < panels.Count(); count++)
            {
                panels[count].Update();

            }
            
        }
        public void Draw()
        {
            for (int count = 0; count < panels.Count(); count++)
            {
                panels[count].Draw();
            }
        }
        public void Rotate(Vector3 rotation)
        {
            for (int count = 0; count < panels.Count(); count++)
            {
                panels[count].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
                panels[count].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
                panels[count].Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            }
        }
    }
}
