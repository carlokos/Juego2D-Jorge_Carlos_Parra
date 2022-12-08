using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic_manager : MonoBehaviour
{
    //funciones publicas que llamamos cuando queremos cambiar la musica
    [SerializeField] private AudioSource GameOverMusic;
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private AudioSource BossMusic;

    public void GameOver()
    {
        BossMusic.Stop();
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
        BossMusic.Stop();
        BGMusic.Play();
    }
}
