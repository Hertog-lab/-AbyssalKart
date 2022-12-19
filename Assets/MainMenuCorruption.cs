using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCorruption : MonoBehaviour
{
    [SerializeField] private float timer;
    public Vector2 rand_del = new Vector2(12,80);
    public Vector2 rand_dur = new Vector2(0.2f, 1f);
    [SerializeField] private TrackCorruption cor;
    
    private float del, dur;
    // Start is called before the first frame update
    void Start()
    {
        del = Random.Range(rand_del.x, rand_del.y);
        dur = Random.Range(rand_dur.x, rand_dur.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < del)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (cor.corruption != 0)
            {
                del = Random.Range(rand_del.x, rand_del.y);
                cor.corruption = 0;
            }
            else
            {
                cor.corruption = Random.Range(0,3);
                del = Random.Range(rand_del.x/10, rand_del.y/10);
            }
            timer = 0;
        }
        
    }
}
