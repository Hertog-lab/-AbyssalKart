using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackCorruption : MonoBehaviour
{
    public float corruption;
    public float corruptionRate;
    
    [Header("WATER")]
    public GameObject o_water;
    [Range(0,1)]
    [SerializeField] private float waterCorruption;
    [SerializeField] private Gradient waterGradient;
    
    [Header("TERRAIN")]
    [SerializeField] private Image staticOverlay;
    [SerializeField] private bool staticActive;
    public bool terrainIsCorrupted = false;
    private bool wasTerrainCorrupted = false;
    [Space(5)]
    [SerializeField] private GameObject[] corruptedTerrain;
    
    [Header("STATIC")]
    public bool forceStatic = false;
    [SerializeField] private float staticDuration = 0.1f;
    private float staticTime = 0;
    [Range(0,1)]
    [SerializeField] private float staticPassive = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        bool terrainIsCorrupted = false;
        bool wasTerrainCorrupted = false;
        foreach (GameObject cor in corruptedTerrain)
        {
            cor.SetActive(terrainIsCorrupted);
        }
        staticTime = -1;
    }   

    // Update is called once per frame
    void Update()
    {
        corruption += (Time.deltaTime*corruptionRate);
        
        //Static overlay when corrupting the environment
        
        
        Color staticCol = Color.white;
        staticCol.a = (staticActive) ? 1f : ((terrainIsCorrupted) ? staticPassive : 0);
        staticOverlay.color = staticCol;
        
        staticOverlay.transform.localScale = new Vector2(((Random.value < 0.5f) ? -1 : 1), ((Random.value < 0.5f) ? -1 : 1));
        
        staticActive = (((staticTime < staticDuration) && (staticTime > -1f)) || (forceStatic));
        
        if (staticTime > -1f)
        {
            if (staticTime < staticDuration)
            {
                staticTime += Time.deltaTime;
            }
            else
            {
                staticTime = -1f;
                staticActive = false;
            }
        }
        else
        {
            //staticActive = false;
        }
        
        o_water.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", waterGradient.Evaluate(waterCorruption));
        
        if (terrainIsCorrupted != wasTerrainCorrupted)
        {
            //static engaged
            staticTime = 0f;
            foreach (GameObject cor in corruptedTerrain)
            {
                cor.SetActive(terrainIsCorrupted);
            }
        }
        wasTerrainCorrupted = terrainIsCorrupted;
    }
}
