using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastonImpulseController : MonoBehaviour
{
    #region references
    [SerializeField]
    GameObject _player;
    FloorDetector _myFloorDetector;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Llamada al método impulso a enemigos (con duck typing?)
        if (!_myFloorDetector.IsGrounded()) { } // Llamada al método impulso al jugador
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myFloorDetector = _player.GetComponent<FloorDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
