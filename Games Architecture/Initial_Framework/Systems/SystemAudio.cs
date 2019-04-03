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
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace OpenGL_Game.Systems
{
    class SystemAudio : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_AUDIO);

        public SystemAudio()
        {
        }

        public string Name
        {
            get { return "SystemAudio"; }
        }

        public void OnAction(Entity entity, List<Entity> entityList)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent audioComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AUDIO;
                });

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });

                Position((ComponentPosition)positionComponent, (ComponentAudio)audioComponent);
                Vector3 listenerPosition = new Vector3(0, 0, 0);
                Vector3 listenerDirection = new Vector3(0, 0, 0);
                Vector3 listenerUp = Vector3.UnitY;
                AL.Listener(ALListener3f.Position, ref listenerPosition);
                AL.Listener(ALListenerfv.Orientation, ref listenerDirection, ref listenerUp);
            }
        }

        public void Position(ComponentPosition positionComponent, ComponentAudio audioComponent)
        {
            audioComponent.SetPosition(positionComponent.Position);
        }
    }
}
