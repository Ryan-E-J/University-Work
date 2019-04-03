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
using OpenTK.Input;

namespace OpenGL_Game.Systems
{
    class SystemPlayerControler : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_VELOCITY | ComponentTypes.COMPONENT_PLAYER_CONTROLER);
        Vector3 up;
        Vector3 still;
        Vector3 down;
        Vector3 right;
        Vector3 left;

        public SystemPlayerControler()
        {
            up = new Vector3(0.0f, 1.0f, 0.0f);
            still = new Vector3(0.0f, 0.0f, 0.0f);
            down = new Vector3(0.0f, -1.0f, 0.0f);
            right = new Vector3(1.0f, 0.0f, 0.0f);
            left = new Vector3(-1.0f, 0.0f, 0.0f);
        }

        public string Name
        {
            get { return "SystemPlayerControler"; }
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

                IComponent controlerComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_PLAYER_CONTROLER;
                });

                Motion((ComponentVelocity)velocityComponent, (ComponentPlayerControler)controlerComponent);
            }
        }

        public void Motion(ComponentVelocity velocityComponent, ComponentPlayerControler contolerComponent)
        {
            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                velocityComponent.Velocity = up;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                velocityComponent.Velocity = down;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Right))
            {
                velocityComponent.Velocity = right;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Left))
            {
                velocityComponent.Velocity = left;
            }
            if (!Keyboard.GetState().IsAnyKeyDown)
            {
                velocityComponent.Velocity = still;
            }
        }
    }
}
