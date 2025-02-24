using System;
using Fungus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance;
        
        
        [Required]
        [SerializeField] private Flowchart flowchart;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }


        [Button]
        public void PlayStory()
        {
            flowchart.ExecuteBlock("main Story");
        }

        [Button]
        public void CompleteStory()
        {
            // TODO : call enter battle
            Debug.Log("Call Enter Battle");
        }
    }
}