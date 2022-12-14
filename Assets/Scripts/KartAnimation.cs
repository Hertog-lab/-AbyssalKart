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

    private void Start()
    {
        kart = gameObject.GetComponent<Kart>();
    }

    private void Update()
    {
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, wheelSpeed * kart.p_acceleration * Time.deltaTime);
        }

        wheelTurn = Mathf.Lerp(wheelTurn, ((Input.GetAxisRaw("Horizontal") < 0) ? -wheelTurnAngle : ((Input.GetAxisRaw("Horizontal") > 0) ? wheelTurnAngle : 0)), Time.deltaTime*8);
        WheelTurnFL.transform.localEulerAngles = new Vector3(0,wheelTurn,0);
        WheelTurnFR.transform.localEulerAngles = new Vector3(0,wheelTurn,0);
        
        kartTurn = Mathf.Lerp(wheelTurn, ((Input.GetAxisRaw("Horizontal") < 0) ? -wheelTurnAngle : ((Input.GetAxisRaw("Horizontal") > 0) ? wheelTurnAngle : 0)), Time.deltaTime*8);
        kartObject.transform.localEulerAngles = new Vector3(0,kartTurn,0);
    }
}