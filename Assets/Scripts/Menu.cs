using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Awake()
    {
        GameSettings.Init(GameSettings.Scenery, GameSettings.Difficulty);
    }

    void OnGUI()
    {
        
    }

    public void Restart()
    {
        GameSettings.Init(GameSettings.Scenery, GameSettings.Difficulty);
        SceneManager.LoadScene("In-Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
