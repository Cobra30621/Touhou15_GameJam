using Core;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace MapObject
{
    public class MarisaGoal : MonoBehaviour
    {
        
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.EnterGoodEnd();
            }
        }


       
    }
}