using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastonImpulseController : MonoBehaviour
{
    #region properties
    private bool _onFloor;
    #endregion
    #region methods
    public void SetFloorDetector(bool a)
    {
        _onFloor = a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Llamada al método impulso a enemigos (con duck typing?)
        if (!_onFloor) { } // Llamada al método impulso al jugador
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
