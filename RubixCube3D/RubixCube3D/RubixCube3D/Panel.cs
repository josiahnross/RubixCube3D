using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RubixCube3D
{
    public class Panel
    {
        VertexPositionColor[] points = new VertexPositionColor[6];
        VertexPositionTexture[] texturePoints = new VertexPositionTexture[6];
        VertexPositionColorTexture[] colorTexturePoints = new VertexPositionColorTexture[6];
        VertexBuffer triangleBuffer;
        BasicEffect triangleEffect;
        GraphicsDevice _graphicsDevice;
        Camera _camera;
        Matrix scale;
        Vector3 tempScale;
        Quaternion tempRotation;
        Vector3 tempTranslation;
        Texture2D _texture;
        public Vector3 RotationAmount
        {

            get
            {
                return new Vector3(tempRotation.X, tempRotation.Y, tempRotation.Z);
            }
        }
        public Matrix Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }
        Matrix rotation;
        public Matrix Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }
        Matrix translation;
        public Matrix Translation
        {
            get
            {
                return translation;
            }
            set
            {
                translation = value;
            }
        }
        public Matrix World
        {
            get
            {
                return triangleEffect.World;
            }
            set
            {
                triangleEffect.World = value;
            }
        }
        public VertexPositionColor[] ColorPoints
        {
            get
            {
                return points;
            }
        }
        public VertexPositionTexture[] TexturePoints
        {
            get
            {
                return texturePoints;
            }
        }
        public VertexPositionColorTexture[] ColorTexturePoints
        {
            get
            {
                return colorTexturePoints;
            }
        }
        public Panel(VertexPositionColor center, float widith, float height,  Camera camera, GraphicsDevice graficsDevice)
        {
            //Vector3 temp = center.Position;
            //center.Position = Vector3.Zero;
            VertexPositionColor topRight = new VertexPositionColor(new Vector3(center.Position.X +widith/2, center.Position.Y +height/2, center.Position.Z), center.Color);
            VertexPositionColor bottomLeft = new VertexPositionColor(new Vector3(center.Position.X - widith / 2, center.Position.Y - height / 2, center.Position.Z), center.Color);
            points[0] = new VertexPositionColor(new Vector3(center.Position.X - widith / 2, center.Position.Y + height / 2, center.Position.Z), center.Color);
            points[1] = topRight;
            points[2] = bottomLeft;
            points[3] = bottomLeft;
            points[4] = topRight;
            points[5] = new VertexPositionColor(new Vector3(center.Position.X + widith / 2, center.Position.Y - height / 2, center.Position.Z), center.Color);
            SetupColor(points, camera, graficsDevice);


            //Translation = Matrix.CreateTranslation(temp);

        }
        public Panel(Vector3 center, float widith, float height, Texture2D texture, Camera camera, GraphicsDevice graficsDevice)
        {
            _texture = texture;
            //Vector3 temp = center;
            //center = Vector3.Zero;
            VertexPositionTexture topRight = new VertexPositionTexture(new Vector3(center.X + widith / 2, center.Y + height / 2, center.Z), new Vector2(1, 1));
            VertexPositionTexture bottomLeft = new VertexPositionTexture(new Vector3(center.X - widith / 2, center.Y - height / 2, center.Z), new Vector2(0, 0));



            texturePoints[0] = new VertexPositionTexture(new Vector3(center.X - widith / 2, center.Y + height / 2, center.Z), new Vector2(0, -1));
            texturePoints[1] = topRight;
            texturePoints[2] = bottomLeft;
            texturePoints[3] = bottomLeft;
            texturePoints[4] = topRight;
            texturePoints[5] = new VertexPositionTexture(new Vector3(center.X + widith / 2, center.Y - height / 2, center.Z), new Vector2(1, 0));
            SetupTexture(texturePoints, texture, camera, graficsDevice);
            //Translation = Matrix.CreateTranslation(temp);
        }
        public Panel(Vector3 center, float widith, float height, Texture2D texture, Color tint, Camera camera, GraphicsDevice graficsDevice)
        {
            _texture = texture;
            //Vector3 temp = center;
            //center = Vector3.Zero;
            VertexPositionColorTexture topRight = new VertexPositionColorTexture(new Vector3(center.X + widith / 2, center.Y + height / 2, center.Z), tint, new Vector2(1, 1));
            VertexPositionColorTexture bottomLeft = new VertexPositionColorTexture(new Vector3(center.X - widith / 2, center.Y - height / 2, center.Z), tint, new Vector2(0, 0));

            colorTexturePoints[0] = new VertexPositionColorTexture(new Vector3(center.X - widith / 2, center.Y + height / 2, center.Z), tint, new Vector2(0, 1));
            colorTexturePoints[1] = topRight;
            colorTexturePoints[2] = bottomLeft;
            colorTexturePoints[3] = bottomLeft;
            colorTexturePoints[4] = topRight;
            colorTexturePoints[5] = new VertexPositionColorTexture(new Vector3(center.X + widith / 2, center.Y - height / 2, center.Z), tint, new Vector2(1, 0));
            SetupColorTexture(colorTexturePoints, texture, camera, graficsDevice);
            //Translation = Matrix.CreateTranslation(temp);
        }
        public Panel(VertexPositionColorTexture topRight, VertexPositionColorTexture topLeft, VertexPositionColorTexture bottomRight, VertexPositionColorTexture bottomLeft, Texture2D texture, Camera camera, GraphicsDevice graficsDevice)
        {
            _texture = texture;
            //Vector3 temp = center;
            //center = Vector3.Zero;
            

            colorTexturePoints[0] = topLeft;
            colorTexturePoints[1] = topRight;
            colorTexturePoints[2] = bottomLeft;
            colorTexturePoints[3] = bottomLeft;
            colorTexturePoints[4] = topRight;
            colorTexturePoints[5] = bottomRight;
            SetupColorTexture(colorTexturePoints, texture, camera, graficsDevice);
            //Translation = Matrix.CreateTranslation(temp);
        }
        public void Update()
        {
            triangleEffect.View = _camera.View;
            rotation.Decompose(out tempScale, out tempRotation, out tempTranslation);
            triangleEffect.World = scale * rotation * translation;
        }
        public void Draw()
        {
            _graphicsDevice.SetVertexBuffer(triangleBuffer);

            foreach (EffectPass pass in triangleEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, triangleBuffer.VertexCount / 3);
            }
        }
        private void SetupColor(VertexPositionColor[] points, Camera camera, GraphicsDevice graficsDevice)
        {
            triangleBuffer = new VertexBuffer(graficsDevice, typeof(VertexPositionColor), points.Count(), BufferUsage.WriteOnly);
            triangleBuffer.SetData<VertexPositionColor>(points);

            triangleEffect = new BasicEffect(graficsDevice);
            
            triangleEffect.View = camera.View;
            triangleEffect.Projection = camera.Projection;
            triangleEffect.World = Matrix.Identity;

            translation = Matrix.Identity;
            scale = Matrix.Identity;
            rotation = Matrix.Identity;

            triangleEffect.VertexColorEnabled = true;
            _camera = camera;
            _graphicsDevice = graficsDevice;
        }
        private void SetupTexture(VertexPositionTexture[] texturePoints, Texture2D texture,  Camera camera, GraphicsDevice graficsDevice)
        {
            triangleBuffer = new VertexBuffer(graficsDevice, typeof(VertexPositionTexture), points.Count(), BufferUsage.WriteOnly);
            triangleBuffer.SetData<VertexPositionTexture>(texturePoints);
            
            triangleEffect = new BasicEffect(graficsDevice);
            triangleEffect.View = camera.View;
            triangleEffect.Projection = camera.Projection;
            triangleEffect.Texture = texture;
            triangleEffect.TextureEnabled = true;
            triangleEffect.VertexColorEnabled = false;
            _camera = camera;
            _graphicsDevice = graficsDevice;


            triangleEffect.World = Matrix.Identity;

            translation = Matrix.Identity;
            scale = Matrix.Identity;
            rotation = Matrix.Identity;
        }
        private void SetupColorTexture(VertexPositionColorTexture[] texturePoints, Texture2D texture, Camera camera, GraphicsDevice graficsDevice)
        {
            triangleBuffer = new VertexBuffer(graficsDevice, typeof(VertexPositionColorTexture), points.Count(), BufferUsage.WriteOnly);
            triangleBuffer.SetData<VertexPositionColorTexture>(texturePoints);

            triangleEffect = new BasicEffect(graficsDevice);
            triangleEffect.View = camera.View;
            triangleEffect.Projection = camera.Projection;
            triangleEffect.Texture = texture;
            triangleEffect.TextureEnabled = true;
            triangleEffect.VertexColorEnabled = true;
            _camera = camera;
            _graphicsDevice = graficsDevice;


            triangleEffect.World = Matrix.Identity;

            translation = Matrix.Identity;
            scale = Matrix.Identity;
            rotation = Matrix.Identity;
        }
    }
}
