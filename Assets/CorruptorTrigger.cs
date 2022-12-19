using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptorTrigger : MonoBehaviour
{
    [Range(0,1)] public float randomStaticChance = 0.1f;
    [Range(0,1)] public float restoreChance = 0.05f;
    public Vector2 corruptAmt = new Vector2(0,0.2f);
}
