using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenLevel : MonoBehaviour
{
    public Button[] levelButtons;

    private void Awake(){
        int highestLevel = PlayerPrefs.GetInt("highestLevel",1);
        for(int i = 0 ; i < 3; i++){
            levelButtons[i].interactable = false;
        }
        for(int i = 0; i < highestLevel ; i++){
            levelButtons[i].interactable = true;
        }
    }
    public void LoadLevel(int level){
        string levelName = "level" + level.ToString();
        PlayerPrefs.SetFloat("PlayerHealth",1000f);
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelMenu(){
        SceneManager.LoadScene("LevelMenu");
    }

}
