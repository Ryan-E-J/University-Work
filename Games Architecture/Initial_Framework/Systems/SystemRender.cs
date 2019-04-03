using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;

namespace OpenGL_Game.Systems
{
    class SystemRender : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_GEOMETRY | ComponentTypes.COMPONENT_TEXTURE);

        protected int pgmID;
        protected int vsID;
        protected int fsID;
        protected int attribute_vtex;
        protected int attribute_vpos;
        protected int uniform_stex;
        protected int uniform_mview;

        protected int skypgmID;
        protected int skyvsID;
        protected int skyfsID;
        protected int skySamplerAttributevpos;
        protected int skySamplerUniformMview;

        public SystemRender()
        {
            pgmID = GL.CreateProgram();
            LoadShader("Shaders/vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            LoadShader("Shaders/fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            attribute_vpos = GL.GetAttribLocation(pgmID, "a_Position");
            attribute_vtex = GL.GetAttribLocation(pgmID, "a_TexCoord");
            uniform_mview = GL.GetUniformLocation(pgmID, "WorldViewProj");
            uniform_stex  = GL.GetUniformLocation(pgmID, "s_texture");

            if (attribute_vpos == -1 || attribute_vtex == -1 || uniform_stex == -1 || uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            skypgmID = GL.CreateProgram();
            LoadShader("Shaders/skybox/vs.glsl", ShaderType.VertexShader, skypgmID, out skyvsID);
            LoadShader("Shaders/skybox/fs.glsl", ShaderType.FragmentShader, skypgmID, out skyfsID);
            GL.LinkProgram(skypgmID);
            Console.WriteLine(GL.GetProgramInfoLog(skypgmID));

            skySamplerAttributevpos = GL.GetAttribLocation(skypgmID, "aPosition");
            skySamplerUniformMview = GL.GetUniformLocation(skypgmID, "WorldViewProj");

            if (skySamplerAttributevpos == -1 || skySamplerUniformMview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }
        }

        void LoadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public string Name
        {
            get { return "SystemRender"; }
        }

        public void OnAction(Entity entity, List<Entity> entityList)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent geometryComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_GEOMETRY;
                });
                Geometry geometry = ((ComponentGeometry)geometryComponent).Geometry();

                IComponent positionComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                Vector3 position = ((ComponentPosition)positionComponent).Position;
                Vector3 scale = ((ComponentPosition)positionComponent).Scale;


                Matrix4 world = Matrix4.Mult(Matrix4.CreateScale(scale), Matrix4.CreateTranslation(position));


                IComponent textureComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_TEXTURE;
                });
                int texture = ((ComponentTexture)textureComponent).Texture;



                if (entity.Name == "Skybox")
                {
                DrawSky(world, geometry, texture);
                }
                else
                    Draw(world, geometry, texture);

            }
        }

        public void DrawSky(Matrix4 world, Geometry geometry, int texture)
        {
            GL.DepthMask(false);
            GL.UseProgram(skypgmID);

            GL.BindTexture(TextureTarget.TextureCubeMap, texture);

            Matrix4 worldViewProjection = world * GameScene.gameInstance.view * GameScene.gameInstance.projection;
            GL.UniformMatrix4(skySamplerUniformMview, false, ref worldViewProjection);

            GL.DepthMask(true);

            geometry.Render();

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.TextureCubeMap, 0);
            GL.UseProgram(0);
        }

        public void Draw(Matrix4 world, Geometry geometry, int texture)
        {
            GL.UseProgram(pgmID);

            GL.Uniform1(uniform_stex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Enable(EnableCap.Texture2D);

            Matrix4 worldViewProjection = world * GameScene.gameInstance.view * GameScene.gameInstance.projection;
            GL.UniformMatrix4(uniform_mview, false, ref worldViewProjection);

            geometry.Render();

            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
