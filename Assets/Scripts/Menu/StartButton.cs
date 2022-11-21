using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]private Button startbutton;
    void Start()
    {
        startbutton.Select();
    }
}
