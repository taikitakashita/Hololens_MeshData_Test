using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : Singleton<MenuManagement> {

    [SerializeField]
    private GameObject m_MainMenu;

    [SerializeField]
    private TextMesh m_debugText;

    private string m_MenuSelect;
    private bool m_MenuShow = true;

    public string MenuSelect
    {
        get { return m_MenuSelect; }
        //set { m_MenuSelect = value; }
    }

    public bool MenuShow
    {
        get { return m_MenuShow; }
        //set { m_MenuSelect = value; }
    }

    public TextMesh DebugTextObject
    {
        get { return m_debugText; }
        //set { m_debugText = value; }
    }

    public void SelectSetting()
    {
        m_MenuSelect = "Setting";
        //MainMenuOff();
        Debug.Log("Setting Menu Selected.");

        if (m_debugText != null)
        {
            m_debugText.text = string.Format("Setting Menu Selected.");
        }
    }

    public void SelectSave()
    {
        m_MenuSelect = "Save";
        //MainMenuOff();
        Debug.Log("Save Menu Selected.");
        if (m_debugText != null)
        {
            m_debugText.text = string.Format("Save Menu Selected.");
        }
    }

    public void MainMenuOn()
    {
        m_MainMenu.SetActive(true);
        m_MenuShow = true;
    }

    public void MainMenuOff()
    {
        m_MainMenu.SetActive(false);
        m_MenuShow = false;
    }
}
