using UnityEngine.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event UnityAction OnFinished ;

    public void FinishedMaze()
    {
        OnFinished?.Invoke();
    }
    
}
