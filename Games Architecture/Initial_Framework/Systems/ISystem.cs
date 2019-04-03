using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Systems
{
    interface ISystem
    {
        void OnAction(Entity entity, List<Entity> entityList);

        // Property signatures: 
        string Name
        {
            get;
        }
    }
}
