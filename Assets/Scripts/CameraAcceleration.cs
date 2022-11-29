using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAcceleration : MonoBehaviour
{
    public Vector2 dist;
    public Kart kart;
    public AnimationCurve speedcurve;
    public float curveRange;
    public Transform camobj;
    public float camSmooth = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float camDist = Mathf.Lerp(dist.x, dist.y, speedcurve.Evaluate((Mathf.Clamp(kart.p_acceleration/curveRange, 0,1))));
        camobj.localPosition = Vector3.Lerp(camobj.localPosition, new Vector3(0,0,-camDist), Time.deltaTime*camSmooth);
    }
}
