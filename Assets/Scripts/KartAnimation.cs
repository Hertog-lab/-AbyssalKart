using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartAnimation : MonoBehaviour
{
    [SerializeField] private GameObject WheelFL;
    [SerializeField] private GameObject WheelFR;
    [SerializeField] private GameObject WheelBL;
    [SerializeField] private GameObject WheelBR;

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
    }
}
