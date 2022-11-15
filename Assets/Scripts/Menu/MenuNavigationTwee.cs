using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuNavigationTwee : MonoBehaviour
{
    [SerializeField] private MenuButton[,] m_buttons;
    [SerializeField] private int[,] m_buttonPosition;
    [SerializeField] private Vector2 m_currentPosition;

    private void Start()
    {
        m_buttonPosition = new int[,] 
        { { 0, 0, 0 }, 
        { 0, 2, 0 }, 
        { 0, 0, 0 } };


        for (int i = 0; i < m_buttonPosition.GetLength(0); i++)
        {
            for (int j = 0; j < m_buttonPosition.GetLength(1); j++)
            {
                if(m_buttonPosition[i, j] == 2)
                {
                    m_currentPosition = new Vector2(i, j);
                    m_buttonPosition[i, j] = 1;
                }
            }
        }
    }

    private void Update()
    {
        print(m_buttonPosition[0, 0] + " " + m_buttonPosition[0, 1] + " " + m_buttonPosition[0, 2] + " \n" +
              m_buttonPosition[1, 0] + " " + m_buttonPosition[1, 1] + " " + m_buttonPosition[1, 2] + " \n" +
              m_buttonPosition[2, 0] + " " + m_buttonPosition[2, 1] + " " + m_buttonPosition[2, 2]);


        for (int i = 0; i < m_buttonPosition.GetLength(0); i++)
        {
            for (int j = 0; j < m_buttonPosition.GetLength(1); j++)
            {
                if (m_buttonPosition[i, j] == 1)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        m_buttonPosition[i, j] = 0;
                        m_buttonPosition[(i - 1), j] = 1;
                        m_currentPosition = new Vector2((i - 1), j);
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        m_buttonPosition[i, j] = 0;
                        m_buttonPosition[(i + 1), j] = 1;
                        m_currentPosition = new Vector2((i + 1), j);
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        m_buttonPosition[i, j] = 0;
                        m_buttonPosition[i, (j - 1)] = 1;
                        m_currentPosition = new Vector2(i, (j - 1));
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        m_buttonPosition[i, j] = 0;
                        m_buttonPosition[i, (j + 1)] = 1;
                        m_currentPosition = new Vector2(i ,(j + 1));
                    }
                }
            }
        }
    }
}
