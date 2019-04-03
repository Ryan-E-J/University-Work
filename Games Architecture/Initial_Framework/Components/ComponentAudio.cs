using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenGL_Game.Managers;

namespace OpenGL_Game.Components
{
    class ComponentAudio : IComponent
    {
        int mySource;
        public ComponentAudio(string audioName)
        {
            int myBuffer = ResourceManager.LoadAudio(audioName);

            mySource = AL.GenSource();
            AL.Source(mySource, ALSourcei.Buffer, myBuffer);
            AL.Source(mySource, ALSourceb.Looping, false);
        }
        public void SetPosition(Vector3 emitterPosition)
        {
            AL.Source(mySource, ALSource3f.Position, ref emitterPosition);
        }

        public void Start()
        {
            AL.SourcePlay(mySource);
        }

        public void Stop()
        {
            AL.SourceStop(mySource);
        }

        public int Audio
        {
            get { return mySource; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AUDIO; }
        }

        public void Close()
        {
            Stop();
            AL.DeleteSource(mySource);
        }
    }
}
