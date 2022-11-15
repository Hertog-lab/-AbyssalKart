using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject m_Selected;
    [SerializeField] private GameObject m_NotSelected; 
    
    public bool m_IsSelected = false;

    private void Update()
    {
        if(m_IsSelected == true)
        {
            m_Selected.SetActive(true);
            m_NotSelected.SetActive(false);
        }
        else
        {
            m_NotSelected.SetActive(true);
            m_Selected.SetActive(false);
        }
    }
}
