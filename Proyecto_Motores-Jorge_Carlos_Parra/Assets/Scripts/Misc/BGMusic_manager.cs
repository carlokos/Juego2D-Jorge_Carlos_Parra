using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic_manager : MonoBehaviour
{
    [SerializeField] private AudioSource GameOverMusic;
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private AudioSource BossMusic;

    public void GameOver()
    {
        BGMusic.Stop();
        GameOverMusic.Play();
    }

    public void BossStart()
    {
        BGMusic.Stop();
        BossMusic.Play();
    }

    public void BossFinish()
    {
        BGMusic.Play();
        BossMusic.Stop();
    }
}
