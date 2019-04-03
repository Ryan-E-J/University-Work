using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentLife : IComponent
    {
        int life = 0;

        public ComponentLife(int health)
        {
            life = health;
        }

        public int health()
        {
            return life;
        }

        public void down()
        {
            life--;
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_LIFE; }
        }
        public void Close()
        {
        }
    }
}
