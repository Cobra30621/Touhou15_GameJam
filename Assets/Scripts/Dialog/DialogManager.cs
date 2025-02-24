using Fungus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [Required]
        [SerializeField] private Flowchart flowchart;


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