using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastonImpulseController : MonoBehaviour
{
    #region references
    [SerializeField]
    GameObject _player;
    CharacterMovementController _characterMovementController;
    FloorDetector _myFloorDetector;
    Transform _bastonTransform;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 impulseDirection;
        impulseDirection.x = Mathf.Cos((Mathf.PI / 180) * _bastonTransform.rotation.eulerAngles.z);
        impulseDirection.y = Mathf.Sin((Mathf.PI / 180) * _bastonTransform.rotation.eulerAngles.z);
        // Llamada al método impulso a enemigos (con duck typing?)
        if (!_myFloorDetector.IsGrounded()) { _characterMovementController.addRepelForce(-impulseDirection); } 
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _bastonTransform = transform;
        _characterMovementController = _player.GetComponent<CharacterMovementController>();
        _myFloorDetector = _player.GetComponent<FloorDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
