using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _baston;
    private CharacterAttackController _attackController;
    private CharacterMovementController _movementController;
    private BastonImpulseController _impulseController;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _attackController.SetFloorDetector(true);
            _movementController.SetFloorDetector(true);
            _impulseController.SetFloorDetector(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _attackController.SetFloorDetector(false);
            _movementController.SetFloorDetector(false);
            _impulseController.SetFloorDetector(false);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _attackController = GetComponent<CharacterAttackController>();
        _movementController = GetComponent<CharacterMovementController>();
        _impulseController = _baston.GetComponent<BastonImpulseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
