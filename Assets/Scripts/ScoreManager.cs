using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManagerInstance;
    private int score;
    public TextMeshProUGUI scoreText,livesText;

    public int lives;
    void Awake(){
        if(scoreManagerInstance == null){
            scoreManagerInstance = this;
        }else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lives  = 2;

        score = PlayerPrefs.GetInt("Score",0);
        // scoreText.text = "Score:" + score.ToString();
        UpdateScoreText();
    }
    public void UpdateScore(int temp){
        score += temp;
        UpdateScoreText();
    }

    public int GetScore(){
        return score;
    }
    public void GetScoreNextLevel(){
        score = PlayerPrefs.GetInt("Score",0);
        UpdateScoreText();
    }

    // Update is called once per frame
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        if(livesText != null){
            livesText.text = "Lives : " + lives.ToString();
        }
    }
}
