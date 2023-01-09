using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIndicator : MonoBehaviour
{
    [SerializeField] private GameObject UpButton;
    [SerializeField] private GameObject DownButton;
    [SerializeField] private GameObject LeftButton;
    [SerializeField] private GameObject RightButton;

    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            UpButton.SetActive(true);
        }
        else
        {
            UpButton.SetActive(false);
        }
        

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            LeftButton.SetActive(true);
        }
        else
        {
            LeftButton.SetActive(false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RightButton.SetActive(true);
        }
        else
        {
            RightButton.SetActive(false);
        }
        
    }
}
