using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public abstract class SoundManager : SingletonBase
    {
        [SerializeField]
        GameObject audioSourcePrefab;

        List<AudioSource> audioSources = new();

        protected abstract int InitSourceCount { get; }


        protected override void Init()
        {
            for (int i = 0; i < InitSourceCount; i++)
            {
                CreateSource();
            }
        }
        protected virtual AudioSource CreateSource()
        {
            var obj = Instantiate(audioSourcePrefab);
            obj.transform.SetParent(transform);
            var source = obj.GetComponent<AudioSource>();
            audioSources.Add(source);
            return source;
        }

        protected AudioSource GetSource(bool isCreate)
        {
            foreach (var source in audioSources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }

            return isCreate ? CreateSource() : null;
        }

        public void StopAll()
        {
            foreach (var source in audioSources)
            {
                source.Stop();
            }
        }
    }
}
