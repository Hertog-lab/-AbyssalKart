using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public GameObject[] menus;
    public int menu_i;
    
    void Start()
    {
        ChangeMenu(0);
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
