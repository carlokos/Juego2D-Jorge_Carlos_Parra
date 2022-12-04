using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic_manager : MonoBehaviour
{
    [SerializeField] private AudioSource GameOverMusic;
    [SerializeField] private AudioSource BGMusic;

    public void GameOver()
    {
        BGMusic.Stop();
        GameOverMusic.Play();
    }
}
