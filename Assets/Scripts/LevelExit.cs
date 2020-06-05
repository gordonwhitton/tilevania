using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowFactor = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {

        Time.timeScale = levelExitSlowFactor;

        yield return new WaitForSecondsRealtime(levelLoadDelay);

        Time.timeScale = 1f; //normal timescale

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
