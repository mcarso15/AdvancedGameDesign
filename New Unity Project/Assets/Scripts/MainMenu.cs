using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject FloatingTextPrefab; 
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayTutorial()
    {
        //play the introLevel
        SceneManager.LoadScene(2); 

        //Trigger floating text to explain game.




    }


}
