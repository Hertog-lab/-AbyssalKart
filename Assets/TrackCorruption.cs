using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCorruption : MonoBehaviour
{
    public float corruption;
    public float corruptionRate;
    
    [Header("WATER")]
    public GameObject o_water;
    [Range(0,1)]
    [SerializeField] private float waterCorruption;
    [SerializeField] private Gradient waterGradient;
    
    // Start is called before the first frame update
    void Start()
    {
    }   

    // Update is called once per frame
    void Update()
    {
        corruption += (Time.deltaTime*corruptionRate);
        
        o_water.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", waterGradient.Evaluate(waterCorruption));
    }
}
