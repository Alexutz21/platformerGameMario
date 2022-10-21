using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    //[SerializeField] GameObject completeLevelUi;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //CompleteLevel();
            StartCoroutine(LoadNextLevel());
            //Invoke("NextLevel", 2f);
        }
    }
    IEnumerator LoadNextLevel()
    {

        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }

    //Or we could do it like this
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     Invoke("NextLevel", 2f);
    // }
    // void NextLevel()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }

    // public void CompleteLevel()
    // {
    //     completeLevelUi.SetActive(true);
    // }

}
