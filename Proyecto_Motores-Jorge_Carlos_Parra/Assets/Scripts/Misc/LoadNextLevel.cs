using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    private Scene SceneToLoad;

    private void Awake()
    {
        SceneToLoad = SceneManager.GetActiveScene();
    }

    public void loadNextLevelScene()
    {
        SceneManager.LoadScene(SceneToLoad.buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            loadNextLevelScene();
        }
    }
}
