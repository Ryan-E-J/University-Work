using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentPosition : IComponent
    {
        Vector3 position;
        Vector3 scale;

        public ComponentPosition(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
            scale = new Vector3(1, 1, 1);
        }

        public ComponentPosition(Vector3 pos, Vector3 giveScale)
        {
            position = pos;
            scale = giveScale;
        }


        public ComponentPosition(Vector3 pos)
        {
            position = pos;
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_POSITION; }
        }
        public void Close()
        {
        }
    }
}
