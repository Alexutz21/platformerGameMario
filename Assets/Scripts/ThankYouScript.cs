using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankYouScript : MonoBehaviour
{
    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
