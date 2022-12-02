using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {  
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Resume();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
