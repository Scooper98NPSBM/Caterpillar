using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighscoreText;
       
    public float score; 
    
    
     private void Awake()
    {        
        if (Instance == null) {
            Instance = this; 
        }   else {
            DestroyImmediate(gameObject);
        }        
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

     private void Start()
    {
        ResetGame();
    } 
  
         
    private void Update()
    {
        score += Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");        
    }

    public void ResetGame()
    {              
        UpdateHighscore();
        score = 0;
    }

    private void UpdateHighscore()
    {
        float Highscore = PlayerPrefs.GetFloat("Highscore", 0);

        if (score > Highscore)
        {
            Highscore = score;
            PlayerPrefs.SetFloat("Highscore", Highscore);
        }

        HighscoreText.text = Mathf.FloorToInt(Highscore).ToString("D5");
    }
}
