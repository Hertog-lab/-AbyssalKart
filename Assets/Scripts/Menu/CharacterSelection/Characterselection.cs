using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characterselection : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    private int currentCharacter;

    private void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }

        ChangeCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentCharacter > 0)
            {
                currentCharacter--;
                ChangeCharacter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentCharacter < (characters.Length - 1))
            {
                currentCharacter++;
                ChangeCharacter();
            }
        }
    }

    private void ChangeCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if(i == currentCharacter) characters[i].SetActive(true);
            else characters[i].SetActive(false);
        }
    }
}
