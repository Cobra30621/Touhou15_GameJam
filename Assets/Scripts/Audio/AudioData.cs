using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio Data", menuName = "Audio Data")]
    public class AudioData : ScriptableObject
    {
        public List<Audio> audioList = new List<Audio>();

        public Audio GetAudioData(string id)
        {
            return audioList.Find(audio => audio.id == id);
        }
    }



    [System.Serializable]
    public class Audio
    {
        public string id;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }
}