using UnityEngine;

public class Finished : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            player.FinishedMaze();
        }
    }
}
