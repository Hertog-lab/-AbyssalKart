using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScene : MonoBehaviour
{
    public GameObject[] menus;
    public int menu_i;
    
    void Start()
    {
        ChangeMenu(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde))
        {
            SceneManager.LoadScene("SandBox");
        }
    }

    public void ChangeMenu(int n)
    {
        menu_i = n;
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive((i == menu_i));
        }
    }
    
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeFocus(Button button)
    {
        button.Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
