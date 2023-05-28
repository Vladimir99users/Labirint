using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagementSystem : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
