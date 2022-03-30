using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myDoor;
    private BoxCollider2D _doorCollider;
    private Animator _switchAnimator;
    private Animator _doorAnimator;
    #endregion

    #region properties
    private bool _opened = false;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BastonImpulseController baston = collision.GetComponent<BastonImpulseController>();
        EnemyShot disparo = collision.GetComponent<EnemyShot>();
        if (baston != null || disparo != null)
        {
            _opened = !_opened;
            _doorCollider.enabled = !_opened;
            _switchAnimator.SetBool("ON", _opened);
            _doorAnimator.SetBool("OPEN", _opened);
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _doorCollider = _myDoor.GetComponent<BoxCollider2D>();
        _switchAnimator = GetComponent<Animator>();
        _doorAnimator = _myDoor.GetComponent<Animator>();
        _switchAnimator.SetBool("ON", false);
        _doorAnimator.SetBool("OPEN", false);
    }
}

   
