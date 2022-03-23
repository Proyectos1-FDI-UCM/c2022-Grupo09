using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [SerializeField] private GameObject whatToSpawn;
    [SerializeField] private GameObject spawnPosReference;

    [SerializeField] private float _spawnDelay = 2.0f;

    private Transform spawnPosTransform;
    private GameObject spawnInstance = null;
    private float timer = 0.0f;


    // Start is called before the first frame update
    
    private void SpawnNewInstance(){
        spawnInstance = Instantiate(whatToSpawn, spawnPosTransform.position, Quaternion.identity) as GameObject;
    }

    private IEnumerator SpawnNewInstanceButDelayed(float delay){
        SpawnNewInstance();
        yield return new WaitForSeconds(delay);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        spawnPosTransform = spawnPosReference.GetComponent<Transform>();      
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > _spawnDelay)
        if(spawnInstance == null){
            SpawnNewInstance();
            timer = 0.0f;
        } else timer += Time.deltaTime;
    }
}
