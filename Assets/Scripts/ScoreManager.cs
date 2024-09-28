using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManagerInstance;
    private int score;
    private TextMeshPro scoreText;
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
        score = 0;
        UpdateScoreText();
    }
    public void UpdateScore(int temp){
        score += temp;
        UpdateScoreText();
    }

    // Update is called once per frame
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
