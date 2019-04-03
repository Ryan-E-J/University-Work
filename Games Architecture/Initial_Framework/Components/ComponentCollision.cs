using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentCollision : IComponent
    {
        Vector3 size;

        public ComponentCollision(float x, float y, float z)
        {
            size = new Vector3(x, y, z);
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION; }
        }
        public void Close()
        {
        }
    }
}
