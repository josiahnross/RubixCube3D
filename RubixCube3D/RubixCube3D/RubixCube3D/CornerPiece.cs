using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RubixCube3D
{
    public class CornerPiece : SidePiece
    {
        Panel panel3;
        
        public CornerPiece(Vector3 center, float size, Texture2D texture, Color color1, Color color2, Color color3, Camera camera, GraphicsDevice graficsDevice)
            :base(center, size, texture, color1, color2, camera, graficsDevice)
        {
            panel3 = new Panel(new Vector3(center.X, (center.Y) * -1, center.Z + size / 2), size, size, texture, color3, camera, graficsDevice);
            panel3.Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-90));
        }
        public void  Update()
        {
            panel1.Update();
            panel2.Update();
            panel3.Update();
        }
        public void Draw()
        {
            panel1.Draw();
            panel2.Draw();
            panel3.Draw();
        }
        public void Rotate(Vector3 rotation)
        {

            panel1.Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            panel1.Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            panel1.Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            panel2.Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            panel2.Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            panel2.Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            panel3.Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            panel3.Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            panel3.Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
        }
    }
}
