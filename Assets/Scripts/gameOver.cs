using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToMainMenu());
    }

    IEnumerator GoToMainMenu(){
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("LevelMenu");
    }
}
