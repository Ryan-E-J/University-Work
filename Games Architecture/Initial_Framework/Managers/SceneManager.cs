﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenGL_Game.Scenes;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace OpenGL_Game.Managers
{
    class SceneManager : GameWindow
    {
        Scene scene;
        public static int width = 1600, height = 900;

        public delegate void SceneDelegate(FrameEventArgs e);
        public SceneDelegate renderer;
        public SceneDelegate updater;
        AudioContext audioContext;

        public SceneManager() : base(width, height, new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(8, 8, 8, 8), 16))
        {
            //Mouse.ButtonDown += Mouse_ButtonDown;
            audioContext = new AudioContext();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);

            //Load the GUI
            GUI.SetUpGUI(width, height);

            StartMenu();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            updater(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer(e);

            GL.Flush();
            SwapBuffers();
        }

        //private void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    switch (e.Button)
        //    {
        //        case MouseButton.Left:
        //            scene.Close();
        //            StartNewGame();
        //            break;
        //        case MouseButton.Right:
        //            scene.Close();
        //            StartMenu();
        //            break;
        //    }
        //}

        public void ChangeScene(SceneTypes sceneType)
        {
            scene.Close();
            switch(sceneType)
            {
                case SceneTypes.SCENE_MAIN_MENU:
                    StartNewGame();
                    break;
                case SceneTypes.SCENE_GAME:
                    StartGameOver();
                    break;
                case SceneTypes.SCENE_GAME_OVER:
                    StartMenu();
                    break;
            }
        }

        public void StartNewGame()
        {
            scene = new GameScene(this);
        }

        public void StartMenu()
        {
            scene = new MainMenuScene(this);
        }

        public void StartGameOver()
        {
            scene = new GameOverScene(this);
        }

        public static int WindowWidth
        {
            get { return width; }
        }

        public static int WindowHeight
        {
            get { return height; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            SceneManager.width = Width;
            SceneManager.height = Height;

            //Load the GUI
            GUI.SetUpGUI(Width, Height);
        }
    }

}

