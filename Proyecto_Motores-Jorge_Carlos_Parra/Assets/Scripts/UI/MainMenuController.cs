using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //clase que se encarga de manejar las escenas 
    private Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Cerrando juego... no funciona en unity recuerda");
        Application.Quit();
    }

    public void howToPlay()
    {
        SceneManager.LoadScene(5);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void loadCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
