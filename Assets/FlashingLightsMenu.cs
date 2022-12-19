using System.Collections;
using System.Collections.Generic;
using UnityEngine;      
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlashingLightsMenu : MonoBehaviour
{
    [SerializeField] private Image blackOverlay;
    
    [SerializeField] private int menuStage;
    public float a;
    public float defaultTime = 8;
    public float timer;
    
    public string mainMenu = "MainMenu";
    
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        menuStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Color col = Color.black;
        col.a = 1-a;
        blackOverlay.color = col;
        
        if (menuStage == 0)
        {
            if (a < 1)
            {
                a = Mathf.Clamp(a+Time.deltaTime*2, 0, 1);
            }
            else
            {
                menuStage++;
            }
        }
        if (menuStage == 1)
        {
            if (timer < defaultTime)
            {
                if (Input.anyKeyDown)
                {
                    timer = defaultTime;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else
            {
                menuStage++;
            }
        }
        if (menuStage == 2)
        {
            if (a > 0)
            {
                a = Mathf.Clamp(a-Time.deltaTime*2, 0, 1);
            }
            else
            {
                Debug.Log("we did it boys, epilepsy is no more");
                SceneManager.LoadScene(mainMenu);
            }
        }
    }
}
