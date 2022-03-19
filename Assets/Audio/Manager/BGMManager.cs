using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class BGMManager : SoundManager
    {
        protected override int InitSourceCount => 1;

        public void Play(AudioClip clip, float volumeScale = 1.0f)
        {
            StopAll();
            GetSource(false).PlayOneShot(clip, volumeScale);
        }
    }
}
