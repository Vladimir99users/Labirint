using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Hud : InteractWindow
{
    [SerializeField] private CanvasGroup _winPanel;
    public void OpenMenu()
    {
        _winPanel.alpha = 1;
        _winPanel.interactable = true;
        _winPanel.blocksRaycasts = true;
    }
}
