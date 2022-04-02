using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _spawnDelay = 2.0f;
    #endregion

    #region parameters
    private float timer = 0.0f;
    #endregion

    #region references
    [SerializeField] private GameObject whatToSpawn;

    private Transform spawnPosTransform;
    private GameObject spawnInstance = null;
    #endregion


    // Start is called before the first frame update

    private void SpawnNewInstance(){
        spawnInstance = Instantiate(whatToSpawn, spawnPosTransform.position, Quaternion.identity);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        spawnPosTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > _spawnDelay)
            if(spawnInstance == null){
            SpawnNewInstance();
            timer = 0.0f;
            } 
    
        timer += Time.deltaTime;
    }
}
