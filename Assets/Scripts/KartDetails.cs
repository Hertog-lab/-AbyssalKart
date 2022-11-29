using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartDetails : MonoBehaviour
{
    public Kart kart;
    public TrailRenderer[] driftlines;
    
    [SerializeField]
    private bool dodrifting_internal;
    public bool dodrifting
    {
        get {return dodrifting_internal;}
        set
        {
            if(value == dodrifting_internal) {return;}
            dodrifting_internal = value;
            
            foreach (TrailRenderer tr in driftlines)
            {
                tr.emitting = value;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dodrifting != kart.p_drifting)
        {
            dodrifting = kart.p_drifting;
        }
    }
}
