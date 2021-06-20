using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Intro");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}