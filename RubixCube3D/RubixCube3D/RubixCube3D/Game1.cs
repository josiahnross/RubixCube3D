using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RubixCube3D
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        List<Panel> centers = new List<Panel>();
        List<SidePiece> sides = new List<SidePiece>();
        List<CornerPiece> corners = new List<CornerPiece>();
        bool noImput = false;
        float rotationTemp = 0;
        Color otherSides = Color.Gray;
        float turnSpeed = 9;
        List<Sprite> arrows = new List<Sprite>();
        Random random = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            camera = new Camera(new Vector3(-7, 10, 25), Vector3.Zero, Vector3.Up, 5, 300f, MathHelper.ToRadians(45), GraphicsDevice.Viewport.AspectRatio);

            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region 3D
            ////centers
            //front 
            centers.Add(new Panel(new Vector3(0, 0, 3), 2, 2, Content.Load<Texture2D>("Outline"), Color.Blue, camera, GraphicsDevice));
            //back 
            centers.Add(new Panel(new Vector3(0, 0, 3), 2, 2, Content.Load<Texture2D>("Outline"), Color.Green, camera, GraphicsDevice));
            centers[centers.Count() - 1].Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(180));
            //right 
            centers.Add(new Panel(new Vector3(0, 0, 3), 2, 2, Content.Load<Texture2D>("Outline"), Color.Red, camera, GraphicsDevice));
            centers[centers.Count() - 1].Rotation = Matrix.CreateRotationY(MathHelper.ToRadians(90));
            //left 
            centers.Add(new Panel(new Vector3(0, 0, 3), 2, 2, Content.Load<Texture2D>("Outline"), Color.OrangeRed, camera, GraphicsDevice));
            centers[centers.Count() - 1].Rotation = Matrix.CreateRotationY(MathHelper.ToRadians(-90));
            //top 
            centers.Add(new Panel(new Vector3(0, 0, 3), 2, 2, Content.Load<Texture2D>("Outline"), Color.Yellow, camera, GraphicsDevice));
            centers[centers.Count() - 1].Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            //bottom 
            centers.Add(new Panel(new Vector3(0, 0, 3), 2, 2, Content.Load<Texture2D>("Outline"), Color.White, camera, GraphicsDevice));
            centers[centers.Count() - 1].Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(90));

            ////sides middle
            //front right 0
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Blue, Color.Red, camera, GraphicsDevice));

            //front left 1
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.OrangeRed, Color.Blue, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(0, -90, 0));

            //back left 2
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Green, Color.OrangeRed, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(0, 180, 0));

            //back right 3
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Red, Color.Green, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(0, 90, 0));

            ////sides top

            //front top 4
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Yellow, Color.Blue, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(-90, -90, 0));

            //right top 5
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Yellow, Color.Red, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(-90, 0, 0));

            //back top 6
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Yellow, Color.Green, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(-90, 90, 0));

            //left top 7
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.Yellow, Color.OrangeRed, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(-90, 180, 0));

            ////sides bottom

            //front bottom 8
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.White, Color.Blue, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(90, -90, 0));

            //right bottom 9
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.White, Color.Red, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(90, 0, 0));

            //back bottom 10
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.White, Color.Green, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(90, 90, 0));

            //left bottom 11
            sides.Add(new SidePiece(new Vector3(2, 0, 2), 2, Content.Load<Texture2D>("Outline"), Color.White, Color.OrangeRed, camera, GraphicsDevice));
            sides[sides.Count() - 1].Rotate(new Vector3(90, 180, 0));

            ////corners Top

            //frontRightUp 0
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.Blue, Color.Red, Color.Yellow, camera, GraphicsDevice));

            //frontLeftUp 1
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.OrangeRed, Color.Blue, Color.Yellow, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(0, -90, 0));

            //BackLeftUp 2
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.Green, Color.OrangeRed, Color.Yellow, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(0, 180, 0));

            //BackRightUp 3
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.Red, Color.Green, Color.Yellow, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(0, 90, 0));

            ////corners Bottom

            //frontRightBottom 4
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.Red, Color.Blue, Color.White, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(180, -90, 0));


            //BackRightBottom 5
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.Green, Color.Red, Color.White, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(180, 0, 0));

            //BackLeftBottom 6
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.OrangeRed, Color.Green, Color.White, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(180, 90, 0));

            //frontLeftBottom 7
            corners.Add(new CornerPiece(new Vector3(2, 2, 2), 2, Content.Load<Texture2D>("Outline"), Color.Blue, Color.OrangeRed, Color.White, camera, GraphicsDevice));
            corners[corners.Count() - 1].Rotate(new Vector3(180, 180, 0));
            #endregion
            //#region 2D
            //arrows.Add(new Sprite(Content.Load<Texture2D>("arrow"), new Vector2(GraphicsDevice.Viewport.Width - 100, 50), Color.White));
            //arrows[arrows.Count() - 1].Orgin = new Vector2(arrows[arrows.Count() - 1].HitBox.Width / 2, arrows[arrows.Count() - 1].HitBox.Height / 2);
            //arrows.Add(new Sprite(Content.Load<Texture2D>("arrow"), new Vector2(GraphicsDevice.Viewport.Width - 97, 135), Color.White));
            //arrows[arrows.Count() - 1].Orgin = new Vector2(arrows[arrows.Count() - 1].HitBox.Width / 2, arrows[arrows.Count() - 1].HitBox.Height / 2);
            //arrows[arrows.Count() - 1].Rotation = MathHelper.ToRadians(180);
            //arrows.Add(new Sprite(Content.Load<Texture2D>("arrow"), new Vector2(GraphicsDevice.Viewport.Width - 40, 90), Color.White));
            //arrows[arrows.Count() - 1].Orgin = new Vector2(arrows[arrows.Count() - 1].HitBox.Width / 2, arrows[arrows.Count() - 1].HitBox.Height / 2);
            //arrows[arrows.Count() - 1].Rotation = MathHelper.ToRadians(90);

            //arrows.Add(new Sprite(Content.Load<Texture2D>("arrow"), new Vector2(GraphicsDevice.Viewport.Width - 154, 93), Color.White));
            //arrows[arrows.Count() - 1].Orgin = new Vector2(arrows[arrows.Count() - 1].HitBox.Width / 2, arrows[arrows.Count() - 1].HitBox.Height / 2);
            //arrows[arrows.Count() - 1].Rotation = MathHelper.ToRadians(-90);
            //#endregion
        }
        KeyboardState ks;
        MouseState ms;
        protected override void Update(GameTime gameTime)
        {
            if (!noImput)
            {
                ks = Keyboard.GetState();
                ms = Mouse.GetState();
            }
            for (int count = 0; count < centers.Count(); count++)
            {
                centers[count].Update();
            }
            for (int count = 0; count < sides.Count(); count++)
            {
                sides[count].Update();
            }
            for (int count = 0; count < corners.Count(); count++)
            {
                corners[count].Update();
            }
            if (ks.IsKeyDown(Keys.Right) && lastKs.IsKeyUp(Keys.Right))
            {
                Right(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.Left) && lastKs.IsKeyUp(Keys.Left))
            {
                Left(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.Up) && lastKs.IsKeyUp(Keys.Up))
            {
                Up(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.Down) && lastKs.IsKeyUp(Keys.Down))
            {
                Down(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.D) && lastKs.IsKeyUp(Keys.D))
            {
                D(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.A) && lastKs.IsKeyUp(Keys.A))
            {
                A(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.W) && lastKs.IsKeyUp(Keys.W))
            {
                W(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.S) && lastKs.IsKeyUp(Keys.S))
            {
                S(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.E) && lastKs.IsKeyUp(Keys.E))
            {
                E(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.Q) && lastKs.IsKeyUp(Keys.Q))
            {
                Q(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.T) && lastKs.IsKeyUp(Keys.T))
            {
                RotateUp(ks, lastKs);
                //Up(ks, lastKs);
                //W(ks, lastKs);
                
            }
            else if (ks.IsKeyDown(Keys.G) && lastKs.IsKeyUp(Keys.G))
            {
                RotateDown(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.F) && lastKs.IsKeyUp(Keys.F))
            {
                RotateRight(ks, lastKs);
            }
            else if (ks.IsKeyDown(Keys.H) && lastKs.IsKeyUp(Keys.H))
            {
                RotateLeft(ks, lastKs);
            }
            

            if (!noImput)
            {
                lastKs = Keyboard.GetState();
                lastMs = Mouse.GetState();
            }
            base.Update(gameTime);
        }
        KeyboardState lastKs;
        MouseState lastMs;
        public void Right(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    tempSide = sides[8];
                    sides[8] = sides[11];
                    sides[11] = sides[10];
                    sides[10] = sides[9];
                    sides[9] = tempSide;

                    tempCorner = corners[4];
                    corners[4] = corners[7];
                    corners[7] = corners[6];
                    corners[6] = corners[5];
                    corners[5] = tempCorner;

                }
                else
                {
                    sides[8].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[9].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[10].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[11].Rotate(new Vector3(0, turnSpeed, 0));
                    //order: 4, 5, 6, 7
                    corners[4].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[5].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[6].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[7].Rotate(new Vector3(0, turnSpeed, 0));

                    centers[5].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));
                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;
            }

        }
        public void Left(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    tempSide = sides[11];
                    sides[11] = sides[8];
                    sides[8] = sides[9];
                    sides[9] = sides[10];
                    sides[10] = tempSide;

                    tempCorner = corners[7];
                    corners[7] = corners[4];
                    corners[4] = corners[5];
                    corners[5] = corners[6];
                    corners[6] = tempCorner;
                }
                else
                {
                    sides[8].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[9].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[10].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[11].Rotate(new Vector3(0, -turnSpeed, 0));
                    //order: 4, 5, 6, 7
                    corners[4].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[5].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[6].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[7].Rotate(new Vector3(0, -turnSpeed, 0));

                    centers[5].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));
                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void Up(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    tempSide = sides[0];
                    sides[0] = sides[9];
                    sides[9] = sides[3];
                    sides[3] = sides[5];
                    sides[5] = tempSide;

                    tempCorner = corners[0];
                    corners[0] = corners[4];
                    corners[4] = corners[5];
                    corners[5] = corners[3];
                    corners[3] = tempCorner;
                }
                else
                {
                    //order: 0, 5, 3, 9
                    sides[0].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[5].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[3].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[9].Rotate(new Vector3(-turnSpeed, 0, 0));
                    //order: 0, 3, 5, 4
                    corners[0].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[3].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[5].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[4].Rotate(new Vector3(-turnSpeed, 0, 0));
                    centers[2].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));
                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void Down(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    tempSide = sides[9];
                    sides[9] = sides[0];
                    sides[0] = sides[5];
                    sides[5] = sides[3];
                    sides[3] = tempSide;

                    tempCorner = corners[0];
                    corners[0] = corners[3];
                    corners[3] = corners[5];
                    corners[5] = corners[4];
                    corners[4] = tempCorner;
                }
                else
                {
                    //order: 0, 5, 3, 9
                    sides[0].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[5].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[3].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[9].Rotate(new Vector3(turnSpeed, 0, 0));
                    //order: 0, 3, 5, 4
                    corners[0].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[3].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[5].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[4].Rotate(new Vector3(turnSpeed, 0, 0));
                    centers[2].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));
                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;
            }
        }
        public void D(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    //order: 4, 5, 6, 7
                    tempSide = sides[4];
                    sides[4] = sides[7];
                    sides[7] = sides[6];
                    sides[6] = sides[5];
                    sides[5] = tempSide;
                    //order: 0, 3, 2, 1
                    tempCorner = corners[0];
                    corners[0] = corners[1];
                    corners[1] = corners[2];
                    corners[2] = corners[3];
                    corners[3] = tempCorner;


                }
                else
                {
                    sides[4].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[5].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[6].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[7].Rotate(new Vector3(0, turnSpeed, 0));

                    corners[0].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[3].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[2].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[1].Rotate(new Vector3(0, turnSpeed, 0));

                    centers[4].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));
                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void A(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    //order: 7, 6, 5, 4
                    tempSide = sides[7];
                    sides[7] = sides[4];
                    sides[4] = sides[5];
                    sides[5] = sides[6];
                    sides[6] = tempSide;
                    //order: 1, 2, 3, 0
                    tempCorner = corners[1];
                    corners[1] = corners[0];
                    corners[0] = corners[3];
                    corners[3] = corners[2];
                    corners[2] = tempCorner;
                }
                else
                {
                    sides[7].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[6].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[5].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[4].Rotate(new Vector3(0, -turnSpeed, 0));

                    corners[1].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[2].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[3].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[0].Rotate(new Vector3(0, -turnSpeed, 0));

                    centers[4].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));
                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void W(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    //order: 1, 7, 2, 11
                    tempSide = sides[1];
                    sides[1] = sides[11];
                    sides[11] = sides[2];
                    sides[2] = sides[7];
                    sides[7] = tempSide;
                    //order: 1, 2, 6, 7
                    tempCorner = corners[1];
                    corners[1] = corners[7];
                    corners[7] = corners[6];
                    corners[6] = corners[2];
                    corners[2] = tempCorner;
                }
                else
                {

                    sides[1].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[7].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[2].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[11].Rotate(new Vector3(-turnSpeed, 0, 0));

                    corners[1].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[2].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[6].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[7].Rotate(new Vector3(-turnSpeed, 0, 0));
                    centers[3].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));
                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }

        }
        public void S(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    //order: 11, 2, 7, 1
                    tempSide = sides[11];
                    sides[11] = sides[1];
                    sides[1] = sides[7];
                    sides[7] = sides[2];
                    sides[2] = tempSide;
                    //order: 7, 6, 2, 1
                    tempCorner = corners[7];
                    corners[7] = corners[1];
                    corners[1] = corners[2];
                    corners[2] = corners[6];
                    corners[6] = tempCorner;
                }
                else
                {
                    sides[11].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[2].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[7].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[1].Rotate(new Vector3(turnSpeed, 0, 0));

                    corners[7].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[6].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[2].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[1].Rotate(new Vector3(turnSpeed, 0, 0));
                    centers[3].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));
                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;
            }
        }
        public void E(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    //order: 0, 8, 1, 4
                    tempSide = sides[0];
                    sides[0] = sides[4];
                    sides[4] = sides[1];
                    sides[1] = sides[8];
                    sides[8] = tempSide;
                    //order: 0, 4, 7, 1
                    tempCorner = corners[0];
                    corners[0] = corners[1];
                    corners[1] = corners[7];
                    corners[7] = corners[4];
                    corners[4] = tempCorner;
                }
                else
                {
                    sides[0].Rotate(new Vector3(0, 0, -turnSpeed));
                    sides[8].Rotate(new Vector3(0, 0, -turnSpeed));
                    sides[1].Rotate(new Vector3(0, 0, -turnSpeed));
                    sides[4].Rotate(new Vector3(0, 0, -turnSpeed));

                    corners[0].Rotate(new Vector3(0, 0, -turnSpeed));
                    corners[4].Rotate(new Vector3(0, 0, -turnSpeed));
                    corners[7].Rotate(new Vector3(0, 0, -turnSpeed));
                    corners[1].Rotate(new Vector3(0, 0, -turnSpeed));
                    centers[0].Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(-turnSpeed));
                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;
            }
        }
        public void Q(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    CornerPiece tempCorner;
                    //order: 4, 1, 8, 0
                    tempSide = sides[4];
                    sides[4] = sides[0];
                    sides[0] = sides[8];
                    sides[8] = sides[1];
                    sides[1] = tempSide;
                    //order: 1, 7, 4, 0
                    tempCorner = corners[1];
                    corners[1] = corners[0];
                    corners[0] = corners[4];
                    corners[4] = corners[7];
                    corners[7] = tempCorner;
                }
                else
                {
                    sides[0].Rotate(new Vector3(0, 0, turnSpeed));
                    sides[8].Rotate(new Vector3(0, 0, turnSpeed));
                    sides[1].Rotate(new Vector3(0, 0, turnSpeed));
                    sides[4].Rotate(new Vector3(0, 0, turnSpeed));

                    corners[0].Rotate(new Vector3(0, 0, turnSpeed));
                    corners[4].Rotate(new Vector3(0, 0, turnSpeed));
                    corners[7].Rotate(new Vector3(0, 0, turnSpeed));
                    corners[1].Rotate(new Vector3(0, 0, turnSpeed));
                    centers[0].Rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(turnSpeed));
                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;
            }
        }
        public void RotateUp(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    Panel tempCenter;
                    //order: 4, 6, 10, 8
                    tempSide = sides[4];
                    sides[4] = sides[8];
                    sides[8] = sides[10];
                    sides[10] = sides[6];
                    sides[6] = tempSide;
                    //order: 0, 4, 1, 5
                    tempCenter = centers[0];
                    centers[0] = centers[5];
                    centers[5] = centers[1];
                    centers[1] = centers[4];
                    centers[4] = tempCenter;
                    //up
                    CornerPiece tempCorner;
                    tempSide = sides[0];
                    sides[0] = sides[9];
                    sides[9] = sides[3];
                    sides[3] = sides[5];
                    sides[5] = tempSide;

                    tempCorner = corners[0];
                    corners[0] = corners[4];
                    corners[4] = corners[5];
                    corners[5] = corners[3];
                    corners[3] = tempCorner;
                    //W
                    tempSide = sides[1];
                    sides[1] = sides[11];
                    sides[11] = sides[2];
                    sides[2] = sides[7];
                    sides[7] = tempSide;

                    tempCorner = corners[1];
                    corners[1] = corners[7];
                    corners[7] = corners[6];
                    corners[6] = corners[2];
                    corners[2] = tempCorner;
                }
                else
                {
                    sides[4].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[6].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[10].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[8].Rotate(new Vector3(-turnSpeed, 0, 0));

                    centers[0].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));
                    centers[4].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));
                    centers[1].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));
                    centers[5].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));

                    //Up
                    sides[0].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[5].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[3].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[9].Rotate(new Vector3(-turnSpeed, 0, 0));
                    
                    corners[0].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[3].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[5].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[4].Rotate(new Vector3(-turnSpeed, 0, 0));
                    centers[2].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));

                    //W
                    sides[1].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[7].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[2].Rotate(new Vector3(-turnSpeed, 0, 0));
                    sides[11].Rotate(new Vector3(-turnSpeed, 0, 0));

                    corners[1].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[2].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[6].Rotate(new Vector3(-turnSpeed, 0, 0));
                    corners[7].Rotate(new Vector3(-turnSpeed, 0, 0));
                    centers[3].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-turnSpeed));

                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void RotateDown(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    Panel tempCenter;
                    CornerPiece tempCorner;
                    //order: 8, 10, 6, 4
                    tempSide = sides[8];
                    sides[8] = sides[4];
                    sides[4] = sides[6];
                    sides[6] = sides[10];
                    sides[10] = tempSide;
                    //order: 5, 1, 4, 0
                    tempCenter = centers[5];
                    centers[5] = centers[0];
                    centers[0] = centers[4];
                    centers[4] = centers[1];
                    centers[1] = tempCenter;

                    //down
                    tempSide = sides[9];
                    sides[9] = sides[0];
                    sides[0] = sides[5];
                    sides[5] = sides[3];
                    sides[3] = tempSide;

                    tempCorner = corners[0];
                    corners[0] = corners[3];
                    corners[3] = corners[5];
                    corners[5] = corners[4];
                    corners[4] = tempCorner;

                    //S
                    tempSide = sides[11];
                    sides[11] = sides[1];
                    sides[1] = sides[7];
                    sides[7] = sides[2];
                    sides[2] = tempSide;
                    
                    tempCorner = corners[7];
                    corners[7] = corners[1];
                    corners[1] = corners[2];
                    corners[2] = corners[6];
                    corners[6] = tempCorner;
                }
                else
                {
                    sides[4].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[6].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[10].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[8].Rotate(new Vector3(turnSpeed, 0, 0));

                    centers[0].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));
                    centers[4].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));
                    centers[1].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));
                    centers[5].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));

                    //down
                    sides[0].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[5].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[3].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[9].Rotate(new Vector3(turnSpeed, 0, 0));
                    
                    corners[0].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[3].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[5].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[4].Rotate(new Vector3(turnSpeed, 0, 0));
                    centers[2].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));

                    //S
                    sides[11].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[2].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[7].Rotate(new Vector3(turnSpeed, 0, 0));
                    sides[1].Rotate(new Vector3(turnSpeed, 0, 0));

                    corners[7].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[6].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[2].Rotate(new Vector3(turnSpeed, 0, 0));
                    corners[1].Rotate(new Vector3(turnSpeed, 0, 0));
                    centers[3].Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(turnSpeed));

                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void RotateRight(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == 90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    Panel tempCenter;
                    //order: 0, 3, 2, 1
                    tempSide = sides[0];
                    sides[0] = sides[1];
                    sides[1] = sides[2];
                    sides[2] = sides[3];
                    sides[3] = tempSide;
                    //order: 0, 2, 1, 3
                    tempCenter = centers[0];
                    centers[0] = centers[3];
                    centers[3] = centers[1];
                    centers[1] = centers[2];
                    centers[2] = tempCenter;

                    //Right
                    CornerPiece tempCorner;
                    tempSide = sides[8];
                    sides[8] = sides[11];
                    sides[11] = sides[10];
                    sides[10] = sides[9];
                    sides[9] = tempSide;

                    tempCorner = corners[4];
                    corners[4] = corners[7];
                    corners[7] = corners[6];
                    corners[6] = corners[5];
                    corners[5] = tempCorner;

                    //D
                    tempSide = sides[4];
                    sides[4] = sides[7];
                    sides[7] = sides[6];
                    sides[6] = sides[5];
                    sides[5] = tempSide;
                    
                    tempCorner = corners[0];
                    corners[0] = corners[1];
                    corners[1] = corners[2];
                    corners[2] = corners[3];
                    corners[3] = tempCorner;
                }
                else
                {
                    sides[0].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[3].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[2].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[1].Rotate(new Vector3(0, turnSpeed, 0));

                    centers[0].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));
                    centers[2].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));
                    centers[1].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));
                    centers[3].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));

                    //Right
                    sides[8].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[9].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[10].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[11].Rotate(new Vector3(0, turnSpeed, 0));
                    
                    corners[4].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[5].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[6].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[7].Rotate(new Vector3(0, turnSpeed, 0));

                    centers[5].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));

                    //D
                    sides[4].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[5].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[6].Rotate(new Vector3(0, turnSpeed, 0));
                    sides[7].Rotate(new Vector3(0, turnSpeed, 0));

                    corners[0].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[3].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[2].Rotate(new Vector3(0, turnSpeed, 0));
                    corners[1].Rotate(new Vector3(0, turnSpeed, 0));

                    centers[4].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(turnSpeed));

                    rotationTemp += turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        public void RotateLeft(KeyboardState ks, KeyboardState lastKs)
        {
            if (noImput)
            {
                if (rotationTemp == -90)
                {
                    noImput = false;
                    SidePiece tempSide;
                    Panel tempCenter;
                    //order: 1, 2, 3, 0
                    tempSide = sides[1];
                    sides[1] = sides[0];
                    sides[0] = sides[3];
                    sides[3] = sides[2];
                    sides[2] = tempSide;
                    //order: 3, 1, 2, 0
                    tempCenter = centers[3];
                    centers[3] = centers[0];
                    centers[0] = centers[2];
                    centers[2] = centers[1];
                    centers[1] = tempCenter;


                    //Left
                    CornerPiece tempCorner;
                    tempSide = sides[11];
                    sides[11] = sides[8];
                    sides[8] = sides[9];
                    sides[9] = sides[10];
                    sides[10] = tempSide;

                    tempCorner = corners[7];
                    corners[7] = corners[4];
                    corners[4] = corners[5];
                    corners[5] = corners[6];
                    corners[6] = tempCorner;

                    //A
                    tempSide = sides[7];
                    sides[7] = sides[4];
                    sides[4] = sides[5];
                    sides[5] = sides[6];
                    sides[6] = tempSide;
                    
                    tempCorner = corners[1];
                    corners[1] = corners[0];
                    corners[0] = corners[3];
                    corners[3] = corners[2];
                    corners[2] = tempCorner;
                }
                else
                {
                    sides[0].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[3].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[2].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[1].Rotate(new Vector3(0, -turnSpeed, 0));

                    centers[0].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));
                    centers[2].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));
                    centers[1].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));
                    centers[3].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));


                    //Left
                    sides[8].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[9].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[10].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[11].Rotate(new Vector3(0, -turnSpeed, 0));
                    
                    corners[4].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[5].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[6].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[7].Rotate(new Vector3(0, -turnSpeed, 0));

                    centers[5].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));

                    //A
                    sides[7].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[6].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[5].Rotate(new Vector3(0, -turnSpeed, 0));
                    sides[4].Rotate(new Vector3(0, -turnSpeed, 0));

                    corners[1].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[2].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[3].Rotate(new Vector3(0, -turnSpeed, 0));
                    corners[0].Rotate(new Vector3(0, -turnSpeed, 0));

                    centers[4].Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-turnSpeed));

                    rotationTemp -= turnSpeed;
                }
            }
            else
            {
                rotationTemp = 0;
                noImput = true;

            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.None };
            //GraphicsDevice.RasterizerState = new RasterizerState() { FillMode = FillMode.WireFrame };
            for (int count = 0; count < centers.Count(); count++)
            {
                centers[count].Draw();
            }
            for (int count = 0; count < sides.Count(); count++)
            {
                sides[count].Draw();
            }
            for (int count = 0; count < corners.Count(); count++)
            {
                corners[count].Draw();
            }
            //#region 2D
            //spriteBatch.Begin();
            //for (int count = 0; count < arrows.Count(); count++)
            //{
            //    //arrows[count].Draw(spriteBatch);
            //}
            //spriteBatch.End();
            //#endregion
            base.Draw(gameTime);
        }


    }
}
