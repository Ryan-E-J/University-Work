using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using System.Collections.Generic;
using System;

namespace OpenGL_Game.Scenes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameScene : Scene
    {
        public Matrix4 view, projection;
        public static float dt = 0;
        EntityManager entityManager;
        SystemManager systemManager;

        public static GameScene gameInstance;

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();

            // Set the title of the window
            sceneManager.Title = "Game";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class
            sceneManager.Keyboard.KeyDown += Keyboard_KeyDown;

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            view = Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75), 800f / 480f, 0.01f, 100f);

            CreateEntities();
            CreateSystems();

            // TODO: Add your initialization logic here

        }

        private void CreateEntities()
        {
            Entity newEntity;

            newEntity = new Entity("Skybox");
            newEntity.AddComponent(new ComponentPosition(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(75f,75f,75f)));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));

            List<string> skyBoxTextures = new List<string>();
            skyBoxTextures.Add("Textures/cubemap/sea_rt.jpg");
            skyBoxTextures.Add("Textures/cubemap/sea_lf.jpg");
            skyBoxTextures.Add("Textures/cubemap/sea_up.jpg");
            skyBoxTextures.Add("Textures/cubemap/sea_dn.jpg");
            skyBoxTextures.Add("Textures/cubemap/sea_ft.jpg");
            skyBoxTextures.Add("Textures/cubemap/sea_bk.jpg");
            newEntity.AddComponent(new ComponentTexture(skyBoxTextures));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Player");
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.25f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/player.png"));
            newEntity.AddComponent(new ComponentVelocity(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            newEntity.AddComponent(new ComponentPlayerControler());
            newEntity.AddComponent(new ComponentLife(3));
            //newEntity.AddComponent(new ComponentAudio());
            entityManager.AddEntity(newEntity);
            float i = -7.0f;
            while (i < 7.0f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(2.0f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-2.0f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }
            i = -7.0f;

            while(i < 0.0f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(0.0f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }
            i = 3f;

            while(i < 7)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 6f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i - 1, 9f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(i - 1, 9f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(i, 9f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(i + 1, 9f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(i + 2, 9f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);


            i = 3f;

            while (i < 7)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(9f, i+3, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(6f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(10f, 6f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(9f, 3f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(10f, 3f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            i = 11f;

            while (i < 15)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 3f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 6f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }
            i = 2;


            while (i > -10f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(9f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(6f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i--;
            }

            while (i > -13)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(6f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i--;
            }
            i++;

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(7f, i, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(8f, i, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(9f, i, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            i = 10f;

            while (i < 15)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -12f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -9f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }

            i = -9f;

            while (i < 3)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(14f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(17f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }

            while(i < 7)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(17f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i++;
            }
            i--;
            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(16f, i, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(15f, i, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            i = -12f;

            while (i > -20f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(14f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i--;
            }

            i = -9f;

            while (i > -20f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(25f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i--;
            }

            i = 18f;

            while (i < 25f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -9f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 15f;

            while (i < 25f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -19f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }







            i = 7f;

            while (i < 15)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-2f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 10f;

            while (i < 12)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(2f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 3f;

            while (i < 12)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 11f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 8f;

            while (i < 11)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(11f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 12f;

            while (i < 20)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 8f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 7f;

            while (i > -6f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(19f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i--;
            }

            i = 20f;

            while (i < 26)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -5f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -4f;

            while (i < 22)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(25f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 15;

            while (i < 26)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 11f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 8;

            while (i > -3)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(22f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i--;
            }

            i = -2f;

            while (i < 22)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 15f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 16f;

            while(i < 18)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(21f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -6f;

            while (i < 25)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 21f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -2f;

            while (i < 21)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 17f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(-2f, 16f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            i = 17f;

            while (i < 21)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-6f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -10f;

            while (i < 14)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-6f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -6f;

            while (i < -1f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -11f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -11f;

            while (i < -7f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(2f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -20f;

            while (i < -11f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(2f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-2f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -24f;

            while (i < -20f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(2f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -2f;

            while (i < 2f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i,-24f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -9f;

            while (i < -2f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -24f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -20f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -11f;

            while (i < -9f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -24f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }


            i = -18f;

            while (i < -11f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -24f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -20f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -22f;

            while (i < -18f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -24f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -23f;

            while (i < -19f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-22f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -19f;

            while (i < -11f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-22f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-18f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }


            i = -8f;

            while (i < 0f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-22f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-18f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 0f;

            while (i < 4f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-18f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 4f;

            while (i < 14f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-22f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-18f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = 14f;

            while (i < 17f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-22f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -22f;

            while (i < -17f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 17f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -17f;

            while (i < -6f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 17f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 13f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -25f;

            while (i < -22f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -12f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, 4f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -11f;

            while (i < 4f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-25f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -19f;

            while (i < -11f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-12f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-9f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -11f;

            while (i < -7f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(-9f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -12f;

            while (i < -9f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -8f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }

            i = -17f;

            while (i < -12f)
            {
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -12f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                newEntity = new Entity("Wall");
                newEntity.AddComponent(new ComponentPosition(i, -8f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);
                i++;
            }








            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(25f, 25f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(-25f, -25f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(-25f, 25f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(25f, -25f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/iki.png"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            i = 1.5f;


            while (i < 7)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, 7.5f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = 7.5f;

            while (i > -10)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(7.5f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i -= 1.5f;
            }

            i = 3f;

            while (i > -10)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(15.5f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i -= 1.5f;
            }

            i = 9f;


            while (i < 16)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, 4.5f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = 9f;


            while (i < 14)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, -10.5f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -20.5f;


            while (i < 0)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(0f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -20f;


            while (i < -1.5f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, -22f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -20f;


            while (i < 14)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(-20f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -17.5f;


            while (i < -5f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, 15f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -9f;


            while (i < 20)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(-4f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -2f;


            while (i < 23f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, 19f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = 13f;


            while (i < 18.5)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(23f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = 0f;


            while (i < 22f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, 13f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = 9f;

            while (i < 13f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(0f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = 13f;


            while (i < 24f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, 9.5f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -3.5f;

            while (i < 8.5f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(23.5f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -3.5f;

            while (i < 8.5f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(20.5f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -10f;

            while (i < 3f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(-23.5f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -18f;


            while (i < -10f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(i, -10f, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            i = -20f;


            while (i < -11f)
            {
                newEntity = new Entity("Item");
                newEntity.AddComponent(new ComponentPosition(-10.5f, i, -29.5f));
                newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
                newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
                newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
                newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
                entityManager.AddEntity(newEntity);

                i += 1.5f;
            }

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(-22f, 2f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(-22f, -10f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(13f, 11f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(-2f, -9f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(0.0f, 1.5f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(0.0f, 3.0f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(0.0f, 4.5f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(0.0f, 6.0f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Item");
            newEntity.AddComponent(new ComponentPosition(0.0f, 7.5f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/smugsaiko.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerItem");
            newEntity.AddComponent(new ComponentPosition(22f, -3.5f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/power.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/powerup.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerItem");
            newEntity.AddComponent(new ComponentPosition(0f, -22f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/power.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/powerup.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerItem");
            newEntity.AddComponent(new ComponentPosition(-20f, 15f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/power.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/powerup.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerItem");
            newEntity.AddComponent(new ComponentPosition(7.5f, -10.5f, -29.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/power.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/powerup.wav"));
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            int test;

            Random rnd = new Random();


            newEntity = new Entity("Ghost");
            newEntity.AddComponent(new ComponentPosition(20f, -10.5f, -28.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/ghost.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/life_lost.wav"));
            test = rnd.Next(4);
            switch (test)
            {
                case 0:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, 1.0f, 0.0f));
                    break;
                case 1:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, -1.0f, 0.0f));
                    break;
                case 2:
                    newEntity.AddComponent(new ComponentVelocity(1.0f, 0.0f, 0.0f));
                    break;
                case 3:
                    newEntity.AddComponent(new ComponentVelocity(-1.0f, 0.0f, 0.0f));
                    break;
            }
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ghost");
            newEntity.AddComponent(new ComponentPosition(20f, -15f, -28.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/ghost.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/life_lost.wav"));
            test = rnd.Next(4);
            switch (test)
            {
                case 0:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, 1.0f, 0.0f));
                    break;
                case 1:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, -1.0f, 0.0f));
                    break;
                case 2:
                    newEntity.AddComponent(new ComponentVelocity(1.0f, 0.0f, 0.0f));
                    break;
                case 3:
                    newEntity.AddComponent(new ComponentVelocity(-1.0f, 0.0f, 0.0f));
                    break;
            }
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ghost");
            newEntity.AddComponent(new ComponentPosition(16f, -15f, -28.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/ghost.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/life_lost.wav"));
            test = rnd.Next(4);
            switch (test)
            {
                case 0:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, 1.0f, 0.0f));
                    break;
                case 1:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, -1.0f, 0.0f));
                    break;
                case 2:
                    newEntity.AddComponent(new ComponentVelocity(1.0f, 0.0f, 0.0f));
                    break;
                case 3:
                    newEntity.AddComponent(new ComponentVelocity(-1.0f, 0.0f, 0.0f));
                    break;
            }
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ghost");
            newEntity.AddComponent(new ComponentPosition(18f, -17f, -28.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/CubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/ghost.jpg"));
            newEntity.AddComponent(new ComponentAudio("Audio/life_lost.wav"));
            test = rnd.Next(4);
            switch (test)
            {
                case 0:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, 1.0f, 0.0f));
                    break;
                case 1:
                    newEntity.AddComponent(new ComponentVelocity(0.0f, -1.0f, 0.0f));
                    break;
                case 2:
                    newEntity.AddComponent(new ComponentVelocity(1.0f, 0.0f, 0.0f));
                    break;
                case 3:
                    newEntity.AddComponent(new ComponentVelocity(-1.0f, 0.0f, 0.0f));
                    break;
            }
            newEntity.AddComponent(new ComponentCollision(1.0f, 1.0f, 1.0f));
            entityManager.AddEntity(newEntity);
        }

        private void CreateSystems()
        {
            ISystem newSystem;
            ISystem physicsSystem;
            ISystem audioSystem;
            ISystem controlerSystem;
            ISystem collisionSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);

            physicsSystem = new SystemPhysics();
            systemManager.AddSystem(physicsSystem);

            audioSystem = new SystemAudio();
            systemManager.AddSystem(audioSystem);

            controlerSystem = new SystemPlayerControler();
            systemManager.AddSystem(controlerSystem);

            collisionSystem = new SystemCollision();
            systemManager.AddSystem(collisionSystem);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Update(FrameEventArgs e)
        {
            //if (GamePad.GetState(1).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Key.Escape))
              //  sceneManager.Exit();

            dt = (float)e.Time;

            // TODO: Add your update logic here

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            systemManager.ActionSystems(entityManager);
        }

        /// <summary>
        /// This is called when the game exits.
        /// </summary>
        public override void Close()
        {
            entityManager.close();
        }

        public void test()
        {
            sceneManager.ChangeScene(SceneTypes.SCENE_GAME);
        }

        public void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    sceneManager.ChangeScene(SceneTypes.SCENE_GAME);
                    break;
            }
        }
    }
}
