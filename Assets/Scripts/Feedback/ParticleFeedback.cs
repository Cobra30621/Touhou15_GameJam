using Sirenix.OdinInspector;
using UnityEngine;

namespace Feedback
{
    public class ParticleFeedback : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject particlePrefab;
        
        public void Play(Transform spawnPos, bool spawnUnderTransform = false)
        {
            if (!particlePrefab) return;

            Debug.Log($"{particlePrefab.name}, {spawnUnderTransform}");
            
            if (spawnUnderTransform)
            {
                Instantiate(particlePrefab, spawnPos);
            }
            else
            {
                Instantiate(particlePrefab, spawnPos.position, spawnPos.rotation);
            }
        
        }
    }
}