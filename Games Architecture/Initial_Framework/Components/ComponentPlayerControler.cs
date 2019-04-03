using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentPlayerControler : IComponent
    {
        

        public ComponentPlayerControler()
        {
            
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_PLAYER_CONTROLER; }
        }
        public void Close()
        {
        }
    }
}
