using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1); // GameScene
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(2); // CreditsScene
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
