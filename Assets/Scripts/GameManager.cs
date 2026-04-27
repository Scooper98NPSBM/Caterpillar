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
    public TextMeshProUGUI GameOverText;
    public Button RetryButton;

    [SerializeField] private ParticleSystem NewHI;

    private ParticleSystem NewHIInstance;
       
    public float score; 

    private Caterpiller player; 
    private SpawnerManager flowerSpawn;
    private Leaf leafSpawn;           
    
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
        player = FindObjectOfType<Caterpiller>();
        flowerSpawn = FindObjectOfType<SpawnerManager>();
        leafSpawn = FindObjectOfType<Leaf>();
                
        ResetGame();        
        NewGame();        
    } 
  
    public void NewGame()
    {               
        player.gameObject.SetActive(true);    
        flowerSpawn.gameObject.SetActive(true);  
        leafSpawn.gameObject.SetActive(true); 
        scoreText.gameObject.SetActive(true);   
        GameOverText.gameObject.SetActive(false);
        RetryButton.gameObject.SetActive(false);               
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

        player.gameObject.SetActive(false); 
        flowerSpawn.gameObject.SetActive(false);
        leafSpawn.gameObject.SetActive(false); 
        scoreText.gameObject.SetActive(false);               
        GameOverText.gameObject.SetActive(true);
        RetryButton.gameObject.SetActive(true);
    }

    private void UpdateHighscore()
    {
        float Highscore = PlayerPrefs.GetFloat("Highscore", 0);

        if (score > Highscore)
        {
            Highscore = score;
            PlayerPrefs.SetFloat("Highscore", Highscore);
            SpawnNewHI();
        }

        HighscoreText.text = Mathf.FloorToInt(Highscore).ToString("D5");
    }   

    private void SpawnNewHI()
    {
        NewHIInstance = Instantiate(NewHI, transform.position, Quaternion.identity);
    }
}
