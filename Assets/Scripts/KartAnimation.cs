using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartAnimation : MonoBehaviour
{
    //[SerializeField] private GameObject WheelFL, WheelFR, WheelBL, WheelBR;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private GameObject WheelTurnFL, WheelTurnFR;
    public float wheelSpeed = 400, wheelTurnAngle = 30, kartTurnAngle = 25;

    private Kart kart;
    [SerializeField] private GameObject kartObject;
    private float wheelTurn, kartTurn;
    private AudioSource src;
    public float kartPitch;
    public Vector2 pitchClamp = new Vector2(0.1f, 4f);

    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;
    private float horizontal = 0f;

    private void Start()
    {
        kart = gameObject.GetComponent<Kart>();
        src = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, wheelSpeed * kart.p_acceleration * Time.deltaTime);
        }
        
        if (Input.GetKey(right) && !Input.GetKey(left)) { horizontal = Mathf.Clamp(horizontal += 0.1f * Time.deltaTime, -1f,1f); }
        if (Input.GetKey(left) && !Input.GetKey(right)) { horizontal = Mathf.Clamp(horizontal -= 0.1f * Time.deltaTime, -1f,1f); }
        if (Input.GetKey(left) && Input.GetKey(right) || !Input.GetKey(left) && !Input.GetKey(right)) { horizontal = 0f; }
        
        wheelTurn = Mathf.Lerp(wheelTurn, ((horizontal < 0) ? -wheelTurnAngle : ((horizontal > 0) ? wheelTurnAngle : 0)), Time.deltaTime*12);
        WheelTurnFL.transform.localEulerAngles = new Vector3(0,wheelTurn,0);
        WheelTurnFR.transform.localEulerAngles = new Vector3(0,wheelTurn,0);
        
        //float driftAmt = ((kart.rotationStrenghtModifier-kart.minRotateStrenght)/7);
        float driftAmt = 1;
        
        kartTurn = Mathf.Lerp(kartTurn, ((horizontal < 0) ? -driftAmt*kartTurnAngle : ((horizontal > 0) ? driftAmt*kartTurnAngle : 0)), Time.deltaTime*8);
        kartObject.transform.localEulerAngles = new Vector3(0,kartTurn,0);
        
        src.pitch = Mathf.Clamp(kart.p_direction.magnitude*kartPitch, pitchClamp.x, pitchClamp.y);
    }

}