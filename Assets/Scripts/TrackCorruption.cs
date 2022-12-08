using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TrackCorruption : MonoBehaviour
{
    public float corruption;
    public float corruptionRate;
    
    public int corruptionPhase;
    private int prev_corruptionPhase;
    
    [Header("WATER")]
    public GameObject o_water;
    [Range(0,1)]
    [SerializeField] private float waterCorruption;
    [SerializeField] private Gradient waterGradient;
    
    [Header("TERRAIN")]
    [SerializeField] private Image staticOverlay;
    [SerializeField] private bool staticActive;
    [Space(5)]
    [SerializeField] private GameObject[] corruptedTerrain1, corruptedTerrain2, corruptedTerrain3;
    
    [Header("STATIC")]
    public bool forceStatic = false;
    [SerializeField] private float staticDuration = 0.1f;
    private float staticTime = 0;
    [Range(0,1)]
    [SerializeField] private float staticPassive = 0f;
    [Space(5)]
    [SerializeField] private Volume ppvol;
    [SerializeField] private AudioSource staticSnd;
    
    // Start is called before the first frame update
    void Start()
    {
        ToggleCorruptionObjects();
        staticTime = -1;
    }   

    // Update is called once per frame
    void Update()
    {
        corruption += (Time.deltaTime*corruptionRate);
        
        //Static overlay when corrupting the environment
        
        
        Color staticCol = Color.white;
        staticCol.a = (staticActive) ? 1f : ((corruptionPhase > 0) ? staticPassive : 0);
        staticSnd.volume = (staticActive) ? 1f : ((corruptionPhase > 0) ? 0.25f : 0);
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
        
        if (corruptionPhase != prev_corruptionPhase)
        {
            //static engaged
            staticTime = 0f;
            ToggleCorruptionObjects();
        }
        prev_corruptionPhase = corruptionPhase;
    }
    
    public void ToggleCorruptionObjects()
    {
        waterCorruption = ((corruptionPhase > 0) ? ((corruptionPhase > 1) ? 1 : 0.5f) : 0);
        ppvol.weight = ((corruptionPhase > 0) ? ((corruptionPhase > 1) ? ((corruptionPhase > 0) ? 1 : 0.5f) : 0.25f) : 0);
        
        foreach (GameObject cor1 in corruptedTerrain1)
        {
            cor1.SetActive(corruptionPhase > 0);
        }
        foreach (GameObject cor2 in corruptedTerrain2)
        {
            cor2.SetActive(corruptionPhase > 1);
        }
        foreach (GameObject cor3 in corruptedTerrain3)
        {
            cor3.SetActive(corruptionPhase > 2);
        }
    }
    
    public void PlaySoundRelativeTo(Transform par, Vector3 pos, AudioClip clip, float pitch, float volume)
    {
        if (clip != null)
        {
            AudioSource go = new GameObject().AddComponent<AudioSource>();
            go.transform.parent = par;
            go.transform.localPosition = pos;
            go.name = clip.name;

            go.clip = clip;
            go.pitch = pitch;
            go.volume = volume;

            go.Play();
            Destroy(go.gameObject, clip.length*pitch);
        }
        else
        {
            Debug.LogWarning("Tried to call PlaySoundRelativeTo with a null Audio clip!");
        }
    }
}
