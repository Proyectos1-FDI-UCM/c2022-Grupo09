using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour
{

    #region references
    

    #endregion

    #region parameters
    [SerializeField]
    private float _duration = 5f;
    [SerializeField]
    private float _currentDuration = 5f;

    [SerializeField]
    private float _moreSpeed = 2f;

    

    #endregion

    #region methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovementController check = collision.gameObject.GetComponent<CharacterMovementController>();
        if (check != null)
        {
            check.MoreVelocity(_moreSpeed, _duration, _currentDuration);
            Destroy(this.gameObject);
        }
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
        

    }
   
}
