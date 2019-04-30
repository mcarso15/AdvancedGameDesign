using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WorldManager : MonoBehaviour
{
    // Start is called before the first frame update

    int toiletGoal;

    public string scene0 = "IntroLevel";
    public string scene1 = "level 01";
    public string scene2 = "level 02";
    public string scene3 = "level 03";

    string nextScene;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == scene0){
            toiletGoal = 4;
            nextScene = "Menu";
        }

        else if(SceneManager.GetActiveScene().name == scene1){
            toiletGoal = 4;
            nextScene = scene2;
        }
        else if(SceneManager.GetActiveScene().name == scene2){
            toiletGoal = 8;
            nextScene = scene3;
        }

        else{
            toiletGoal = 8;
            nextScene = "Menu";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(toiletGoal == 0){
            SceneManager.LoadScene(nextScene);
        }
    }

    public void Plunge(){
        toiletGoal --;
    }
}
