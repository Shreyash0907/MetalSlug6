using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenLevel : MonoBehaviour
{
    public static OpenLevel openLevel;
    public Button[] levelButtons;
    public Canvas canvas;

    private void Awake(){
        if(openLevel != null){
            Destroy(gameObject);
        }else{
            openLevel = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void loadLevels(){
        int highestLevel = PlayerPrefs.GetInt("highestLevel",1);
        highestLevel = Mathf.Max(1,highestLevel);
        highestLevel = Mathf.Min(highestLevel,3);
        int levels = levelButtons.Length;
        for(int i = 0 ; i < levels; i++){
            levelButtons[i].interactable = false;
        }
        for(int i = 0; i < highestLevel ; i++){
            levelButtons[i].interactable = true;
        }
        Debug.Log(highestLevel);
        DontDestroyOnLoad(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name != "LevelMenu") 
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(true);
            loadLevels();
        }
    }
    public void LoadLevel(int level){
        string levelName = "level" + level.ToString();
        PlayerPrefs.SetFloat("PlayerHealth",800f);
        PlayerPrefs.SetInt("Score",0);
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelMenu(){
        SceneManager.LoadScene("LevelMenu");
    }

}
