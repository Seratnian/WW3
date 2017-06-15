using System;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject PanelMenu;
    public GameObject PanelButtons;

    private static GameObject _panelMenu;
    private RectTransform _panelButtons;

	void Start ()
    {
        _panelMenu = PanelMenu;

        _panelMenu.SetActive(false);
    }

    public void CloseMenuInstance()
    {
        CloseMenu();
    }
	
    public static void OpenMenu()
    {
        _panelMenu.SetActive(true);
        EventCatalogue.OnMenuOpened(null, EventArgs.Empty);
    }

    public static void CloseMenu()
    {
        _panelMenu.SetActive(false);
        EventCatalogue.OnMenuClosed(null, EventArgs.Empty);
    }

    public static bool MenuIsOpen()
    {
        return _panelMenu.activeSelf;
    }
}
