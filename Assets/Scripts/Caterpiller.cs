using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpiller : MonoBehaviour

{
   private Vector2 direction = Vector2.right;

   private List<Transform> segments = new List<Transform>();

   public Transform segmentPrefab;

   public int initialSize = 2;

   public CameraShakeManager cameraShake;
   public float shakeDuration = 0.2f;
   public float shakeMagnitude = 0.2f;

        
   [SerializeField] private AudioClip collectSound;
   [SerializeField] private AudioClip deathSound;  
   [SerializeField] private AudioClip flowerSound;


   private void Start()
   {
    segments = new List<Transform>();
    segments.Add(this.transform);

    for (int i = 1; i < this.initialSize; i++)
        segments.Add(Instantiate(this.segmentPrefab));
   }       

   private void Update()
   {
       if (Input.GetKeyDown(KeyCode.W))
       {
           direction = Vector2.up;
       }
       else if (Input.GetKeyDown(KeyCode.S))
       {
           direction = Vector2.down;
       }
       else if (Input.GetKeyDown(KeyCode.A))
       {
           direction = Vector2.left;
       }
       else if (Input.GetKeyDown(KeyCode.D))
       {
           direction = Vector2.right;
       }       
   }

    private void FixedUpdate()
    {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

         this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
         );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    private void ResetState()
    {  
         for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        segments.Add(Instantiate(this.segmentPrefab));
      
        this.transform.position = Vector3.zero;           
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Food"))
         {     
            SoundFxManager.instance.PlaySoundFXClip(collectSound, transform, 1f);       
            GameManager.Instance.score += 25;             
            Grow();              
            Destroy(other.gameObject);                                                
         }
            else if (other.CompareTag("Obstacle"))
            {    
                SoundFxManager.instance.PlaySoundFXClip(deathSound, transform, 1f);           
                cameraShake.TriggerShake(shakeDuration, shakeMagnitude);                
                ResetState(); 
                GameManager.Instance.ResetGame();                                                                          
            } 
            else if (other.CompareTag("Flower"))  
            {
                SoundFxManager.instance.PlaySoundFXClip(flowerSound, transform, 1f);
                GameManager.Instance.score += 100;  
                Destroy(other.gameObject);                
                ResetState(); 
            }                          
    }      
}
