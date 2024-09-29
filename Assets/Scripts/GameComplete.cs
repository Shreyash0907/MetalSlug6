using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreImage;
    void Awake(){
        int score = PlayerPrefs.GetInt("Score",4366);
        ScoreImage.text = score.ToString();
        StartCoroutine(GoToMainMenu());
    }

    IEnumerator GoToMainMenu(){
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("LevelMenu");
    }
}
