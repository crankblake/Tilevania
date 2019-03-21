using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] float LevelExitSlowMoFactor = .2f;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
         StartCoroutine("LoadNextLevel");
    }
    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        yield return new WaitForSeconds(levelLoadDelay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


}
