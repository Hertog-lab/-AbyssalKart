using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private MenuButton[] menuButtons;
    [SerializeField] private Vector2[] locations;
    [SerializeField] private List<CustomButton> m_buttons = new List<CustomButton>();
    [SerializeField] private Vector2 m_currentPosition;

    private CustomButton lastButton;
    private CustomButton currentButton;
    [SerializeField]private int highestX = 0;
    [SerializeField]private int highestY = 0;
    private void Start()
    {
        m_currentPosition = new Vector2Int(0, 0);
        for (int i = 0; i < menuButtons.Length; i++)
        {
            m_buttons.Add(new CustomButton(menuButtons[i], locations[i]));
        }

        for (int i = 0; i < m_buttons.Count; i++)
        {
            highestX = (int)Mathf.Max(highestX, m_buttons[i].Location.x);
            highestY = (int)Mathf.Max(highestY, m_buttons[i].Location.y);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(m_currentPosition.x > 0)
            {
                m_currentPosition += new Vector2Int(-1, 0);
                CheckLocation();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(m_currentPosition.x < highestX)
            {
                m_currentPosition += new Vector2Int(1, 0);
                CheckLocation();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_currentPosition.y > 0)
            {
                m_currentPosition += new Vector2Int(0, -1);
                CheckLocation();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_currentPosition.y < highestY)
            {
                m_currentPosition += new Vector2Int(0, 1);
                CheckLocation();
            }
        }
    }

    private void CheckLocation()
    {
        for (int i = 0; i < m_buttons.Count; i++)
        {
            if (m_buttons[i].Location == m_currentPosition)
            {
                currentButton = m_buttons[i];
                SetButtonActive();
                break;
            }
        }
    }

    private void SetButtonActive()
    {
        if(lastButton != null) lastButton.menuButton.m_IsSelected = false;
        currentButton.menuButton.m_IsSelected = true;
        lastButton = currentButton;
    }
}

[System.Serializable]
public class CustomButton
{
    public MenuButton menuButton;
    public Vector2 Location { get; private set; }
    public CustomButton(MenuButton button, Vector2 location)
    {
        menuButton = button;
        Location = location;
    }
}
