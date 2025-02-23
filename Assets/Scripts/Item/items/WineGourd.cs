using Player;
using System.Collections;
using System.Collections.Generic;
using Feedback;
using UnityEngine;

public class WineGourd : BaseItem
{
    [SerializeField]private float size = 0.1f;
    
    [SerializeField] private ParticleFeedback particleFeedback;
    
    public override bool use()
    {
        PlayerController.Instance.sizeHandler.Resize(size);

        var position = PlayerController.Instance.transform;
        
        if(particleFeedback != null)
            particleFeedback.Play(position, true);
        
        return true;
    }
}
