using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characterselection : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private bool isSelectingCharacter = true;

    private int currentCharacter;
    private Manager manager;

    private void Start()
    {
        StopSelection();
        manager = FindObjectOfType<Manager>();
        StartSelection();
    }

    private void Update()
    {
        if (isSelectingCharacter)
        {
            CheckInput();
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

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentCharacter > 0)
            {
                currentCharacter--;
                ChangeCharacter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentCharacter < (characters.Length - 1))
            {
                currentCharacter++;
                ChangeCharacter();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectCharacter();
        }
    }

    private void SelectCharacter()
    {
        if(manager.characters.Length < currentCharacter)
        {
            return;
        }
        else
        {
            manager.StartGame(currentCharacter);
            StopSelection();
        }
    }

    public void StartSelection()
    {
        isSelectingCharacter = true;
        ChangeCharacter();
    }

    public void StopSelection()
    {
        isSelectingCharacter = false;
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }
}
