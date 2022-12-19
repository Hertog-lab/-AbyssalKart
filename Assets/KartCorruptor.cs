using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartCorruptor : MonoBehaviour
{
    [SerializeField] private TrackCorruption corrupt;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<CorruptorTrigger>() != null)
        {
            CorruptorTrigger cortrig = col.gameObject.GetComponent<CorruptorTrigger>();
            
            corrupt.UpdateCorruption((Random.value < (cortrig.restoreChance*corrupt.corruptionPhase)) ? -1f : Random.Range(cortrig.corruptAmt.x, cortrig.corruptAmt.y));
            
            if (Random.value < (cortrig.randomStaticChance*corrupt.corruptionPhase))
            {
                corrupt.DoStatic(0.05f, false, false);
            }
        }
    }
}
