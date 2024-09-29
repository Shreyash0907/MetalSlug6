using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lvl2 : MonoBehaviour
{
    public GameObject van,bigTank;
    public Transform goPlace;
    public GameObject goSign;
    private bool hasFlaged = false;
    private AudioSource audioSource;
    public Image healthBar;
    private float health;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update(){
        if(van == null && bigTank == null && !hasFlaged){
            audioSource.Play();
            if(goPlace != null && goSign != null){
                Instantiate(goSign, goPlace.position, goPlace.rotation);
            }
            hasFlaged = true;
            
        }
        if(hasFlaged){
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(2f);
        ChangeHighest();
        if(healthBar != null){
            health = healthBar.fillAmount * 800;
        }else{
            health = 800f;
        }
        PlayerPrefs.SetFloat("PlayerHealth", health);
        PlayerPrefs.SetInt("Score",ScoreManager.scoreManagerInstance.GetScore());
        Debug.Log(PlayerPrefs.GetInt("Score"));
        if(SceneManager.GetActiveScene().buildIndex == 3){
            SceneManager.LoadScene("GameComplete");
        }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    } 

    private void ChangeHighest(){
        
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("highestLevel")){
            Debug.Log("highest changed");
            PlayerPrefs.SetInt("highestLevel", SceneManager.GetActiveScene().buildIndex+1);
            Debug.Log(PlayerPrefs.GetInt("highestLevel"));
            PlayerPrefs.Save();
        }
    }
}
