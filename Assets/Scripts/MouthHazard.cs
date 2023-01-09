using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthHazard : MonoBehaviour
{
    [System.Serializable]
    public class MouthEntry
    {
        public float restPos = 15;
        public Transform upperJaw, lowerJaw;
    }
    
    public GameObject player;
    Animator anim;
    public bool debounce = false;
    public bool doMouthStuff = false;
    AudioSource src;
    private float mouthOpenRaw;
    [SerializeField] private AudioClip[] bites;
    [SerializeField] private float mouthOpen = 0, mouthOpenMult = 1;
    public MouthEntry[] speakingMouths;
    private Manager manager;

    public float mouthSmooth = 4;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
        mouthOpen = 0;
        mouthOpenRaw = 0;
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("eat", debounce);
        
        if (doMouthStuff)
        {
            //Get audio data for mouth
            float[] audioSpectrum = new float[64]; //32 samples van de audio track
            src.GetSpectrumData(audioSpectrum, 0, FFTWindow.Triangle); //populeert de audioSpectrum met data (Hanning is the FFTWindow interpolation methode)
            for (int i = 1; i < audioSpectrum.Length - 1; i++)
            {
                mouthOpenRaw = Mathf.Log(audioSpectrum[0]);
            }

            if (!System.Single.IsNaN(mouthOpen))
            {
                Debug.Log("not nan");
            mouthOpen = Mathf.Lerp(mouthOpen, mouthOpenRaw+5, Time.deltaTime*mouthSmooth);

                //Make mouth speak
                foreach (MouthEntry entry in speakingMouths)
                {
                    entry.upperJaw.localEulerAngles = new Vector3(Mathf.Min(-entry.restPos, -entry.restPos - (mouthOpen*mouthOpenMult)), 0, 0);
                    entry.lowerJaw.localEulerAngles = new Vector3(Mathf.Max(entry.restPos, entry.restPos + (mouthOpen*mouthOpenMult)), 0, 0);
                }
            }
            else
            {
                mouthOpen = 0;
                Debug.Log("nan");
            }
        }
    }
    
    public void Bite()
    {
        PlayBiteSound(0);
        if (debounce == true)
        {
            PlayBiteSound(1);
            Debug.Log("Player was bitten!");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>() && !debounce)
        {
            debounce = true;
            Debug.Log("nom nom nom");
            manager.GameOver();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (debounce)
        {
            debounce = false;
        }
    }
    
    public void PlayBiteSound(int id)
    {
        src.PlayOneShot(bites[id]);
    }
}
