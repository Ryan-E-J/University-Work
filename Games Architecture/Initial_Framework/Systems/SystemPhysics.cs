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
    class SystemPhysics : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_VELOCITY);

        public SystemPhysics()
        {
        }

        public string Name
        {
            get { return "SystemPhysics"; }
        }

        public void OnAction(Entity entity, List<Entity> entityList)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent velocityComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                });

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });

                Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent);
            }
        }

        public void Motion(ComponentPosition positionComponent, ComponentVelocity velocityComponent)
        {
            positionComponent.Position = positionComponent.Position + velocityComponent.Velocity * GameScene.dt;
        }
    }
}
