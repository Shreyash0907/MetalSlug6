using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2 : MonoBehaviour
{
    public GameObject van,bigTank;
    public Transform goPlace;
    public GameObject goSign;
    private bool hasFlaged = false;
    private AudioSource audioSource;
    
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update(){
        if(van == null && bigTank == null && !hasFlaged){
            audioSource.Play();
            Instantiate(goSign, goPlace.position, goPlace.rotation);
            hasFlaged = true;
        }
        if(hasFlaged){
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    private void ChangeHighest(){
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("highestLevel")){
            PlayerPrefs.SetInt("highestLevel", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }
}
