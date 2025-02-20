using System;
using UnityEngine.Serialization;

namespace Map.Data
{
    [Serializable]
    public class Stage
    {
        public StageName StageName;

        public int RequiredRoomCount = -1;
    }
}