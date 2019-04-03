using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics;
using OpenGL_Game.Managers;

namespace OpenGL_Game.Components
{
    class ComponentTexture : IComponent
    {
        int texture;

        public ComponentTexture(string textureName)
        {
            texture = ResourceManager.LoadTexture(textureName);
        }

        public ComponentTexture(List<string> Textures)
        {
            texture = ResourceManager.LoadCubeMap(Textures);
        }

        public int Texture
        {
            get { return texture; }
        }

        public void remove()
        {
            texture = 0;
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_TEXTURE; }
        }
        public void Close()
        {
        }
    }
}
