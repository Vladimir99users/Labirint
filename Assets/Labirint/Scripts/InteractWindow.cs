using UnityEngine;

public class InteractWindow: MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public bool IsView {get => _canvasGroup.interactable; set => value = _canvasGroup.interactable;}
    public virtual void ViewWindow()
    {
       if(IsView)
       {
           Close();
       } else 
       {
           Open();
       }
    }

    protected virtual void Close() 
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        IsView = _canvasGroup.interactable;
    }

    protected virtual void Open() 
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        IsView = _canvasGroup.interactable;
    }

}
