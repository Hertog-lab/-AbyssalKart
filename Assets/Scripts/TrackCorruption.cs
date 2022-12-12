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
    
    [System.Serializable] public class PhaseEntry //entry [0] is default
    {
        public bool doIntermittentStatic, doIntermittentText;
        public Vector2 staticWindow;
        public Vector2 staticDuration;
        [Range(0,1)] public float passiveStaticVolume, passiveWhisperVolume;
    }
    
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
    public PhaseEntry[] phases;
    public bool forceStatic = false;
    public bool transition = false;
    [SerializeField] private float staticDuration = 0.1f;
    private float staticTime = 0;
    [Range(0,1)]
    [SerializeField] private float staticPassive = 0f;
    [Space(5)]
    [SerializeField] private Volume ppvol;
    [SerializeField] private AudioSource staticSnd, whisperSnd;
    private float imt_staticTimer, imt_staticTime;
    [Space(5)]
    [SerializeField] private TMPro.TextMeshProUGUI txt;
    [SerializeField] private string[] flash_entries;
    [SerializeField] private Vector2 randomPos, randomPossiblePos;
    [SerializeField] private float jitter;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ToggleCorruptionObjects();
        staticTime = -999;
        imt_staticTime = 0;
    }   

    // Update is called once per frame
    void Update()
    {
        corruption += (Time.deltaTime*corruptionRate);
        
        if ((corruptionPhase < phases.Length) && (phases[corruptionPhase].doIntermittentStatic))
        {
            if (imt_staticTimer > imt_staticTime)
            {
                DoStatic(Random.Range(phases[corruptionPhase].staticDuration.x, phases[corruptionPhase].staticDuration.y), phases[corruptionPhase].doIntermittentText, false);
                imt_staticTimer = 0 - Random.Range(phases[corruptionPhase].staticWindow.x, phases[corruptionPhase].staticWindow.y);
            }
            else
            {
                imt_staticTimer += (Time.deltaTime);
            }
        }
        
        //Static overlay when corrupting the environment
        Color staticCol = Color.white;
        staticCol.a = (staticActive) ? ((transition) ? 1f : 0.5f) : ((corruptionPhase > 0) ? staticPassive : 0);
        staticSnd.volume = (staticActive) ? 1f : ((corruptionPhase > 0) ? ((corruptionPhase < phases.Length) ? phases[corruptionPhase].passiveStaticVolume : 0.05f): 0);
        whisperSnd.volume = (corruptionPhase < phases.Length) ? phases[corruptionPhase].passiveStaticVolume : 0;
        
        staticOverlay.color = staticCol;
        
        staticOverlay.transform.localScale = new Vector2(((Random.value < 0.5f) ? -1 : 1), ((Random.value < 0.5f) ? -1 : 1));
        
        staticActive = (((staticTime < staticDuration) && (staticTime > -999f)) || (forceStatic));
        
        if (staticTime > -999f)
        {
            if (staticTime < staticDuration)
            {
                staticTime += Time.deltaTime;
            }
            else
            {
                staticTime = -999f;
                staticActive = false;
                transition = false;
                txt.gameObject.SetActive(false);
            }
        }
        else
        {
            //staticActive = false;
        }
        
        
        txt.rectTransform.anchoredPosition = randomPos + new Vector2(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter));
    
        o_water.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", waterGradient.Evaluate(waterCorruption));
        
        if (corruptionPhase != prev_corruptionPhase)
        {
            //static engaged
            DoStatic();
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
    
    public void DoStatic(float dur = 0.1f, bool doVoiceline = false, bool transitioning = true)
    {
        transition = transitioning;
        staticTime = (0f - dur);
        
        randomPos = new Vector3(Random.Range(-randomPossiblePos.x, randomPossiblePos.x), Random.Range(-randomPossiblePos.y, randomPossiblePos.y));
        
        if (doVoiceline)
        {
            txt.text = flash_entries[Random.Range(0,flash_entries.Length)];
        }
        txt.gameObject.SetActive(doVoiceline);
    }
}
