using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartAnimation : MonoBehaviour
{
    [SerializeField] private GameObject WheelFL;
    [SerializeField] private GameObject WheelFR;
    [SerializeField] private GameObject WheelBL;
    [SerializeField] private GameObject WheelBR;
    [SerializeField] private GameObject WheelTurnFL;
    [SerializeField] private GameObject WheelTurnFR;

    private Kart kart;
    private GameObject kartObject;

    private void Start()
    {
        kart = gameObject.GetComponent<Kart>();
    }

    private void Update()
    {
        WheelFL.transform.Rotate(Vector3.right, kart.p_acceleration * Time.deltaTime * 1000);
        WheelFR.transform.Rotate(Vector3.right, kart.p_acceleration * Time.deltaTime * 1000);
        WheelBL.transform.Rotate(Vector3.right, kart.p_acceleration * Time.deltaTime * 1000);
        WheelBR.transform.Rotate(Vector3.right, kart.p_acceleration * Time.deltaTime * 1000);

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            WheelTurnFL.transform.localRotation = Quaternion.Euler(0, -30, 0);
            WheelTurnFR.transform.localRotation = Quaternion.Euler(0, -30, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            WheelTurnFL.transform.localRotation = Quaternion.Euler(0, 30, 0);
            WheelTurnFR.transform.localRotation = Quaternion.Euler(0, 30, 0);
        }
        else
        {
            WheelTurnFL.transform.localRotation = Quaternion.Euler(0, 0, 0);
            WheelTurnFR.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
