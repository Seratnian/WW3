using UnityEngine;

public class MenuScript : MonoBehaviour {

    public GameObject PanelMenu;
    public GameObject PanelButtons;

    private static GameObject _panelMenu;
    private RectTransform _panelButtons;

	void Start () {
        _panelMenu = PanelMenu;

        _panelButtons = PanelButtons.GetComponent<RectTransform>();
        _panelButtons.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 3);
        _panelButtons.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height - 100);
    }
	
    public static void ToggleMenu()
    {
        _panelMenu.SetActive(!_panelMenu.activeSelf);
    }
}
