using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentVelocity : IComponent
    {
        Vector3 position;

        public ComponentVelocity(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
        }

        public ComponentVelocity(Vector3 pos)
        {
            position = pos;
        }

        public Vector3 Velocity
        {
            get { return position; }
            set { position = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_VELOCITY; }
        }
        public void Close()
        {
        }
    }
}
