using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    /*
     * Script que se encarga del menu de pausa, si hay algun UI especial (como la barra de vida de un boss) esta desaparece temporalmente)
     */
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CombatPlayer player;
    [SerializeField] private GameObject[] cancelEnable;
    private bool isPaused;
    private bool haveSpecialUI;

    public bool HaveSpecialUI { get => haveSpecialUI; set => haveSpecialUI = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Resume();
        haveSpecialUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused && !player.IsTalking && !player.Dead)
        {
            if (cancelEnable.Length != null)
            {
                for (int i = 0; i < cancelEnable.Length; i++)
                {
                    cancelEnable[i].SetActive(false);
                }
            }
            pauseMenu.SetActive(true);
            player.CanAttack = false;
            isPaused = true;
            Time.timeScale = 0;
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Resume();
            if (cancelEnable.Length != null && haveSpecialUI)
            {
                for (int i = 0; i < cancelEnable.Length; i++)
                {
                    cancelEnable[i].SetActive(true);
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        player.CanAttack = true;
    }
}
