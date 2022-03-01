using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _player;
    private CharacterAttackController _AttackController;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _AttackController.SetFloorDetector(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _AttackController.SetFloorDetector(false);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _AttackController = _player.GetComponent<CharacterAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
