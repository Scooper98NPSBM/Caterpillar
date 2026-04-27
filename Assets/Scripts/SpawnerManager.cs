using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public GameObject Flowerprefab;
    public float spawnTime = 45f;
    private float timeElapsed = 0f; 
    public static int amountSpawned;

    private void Start()
    {
        amountSpawned = 0;
    }
             
    private void RandomizePosition()
    {        
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        
        GameObject instance = Instantiate(Flowerprefab);
        instance.transform.position = new Vector3(x, y, instance.transform.position.z);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed>= spawnTime)
        {
            RandomizePosition();
            timeElapsed = 0f;
        }
    }    
    
    private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.tag == "Player")
         {
            RandomizePosition();
            Destroy(gameObject);
         }
     }       
}
