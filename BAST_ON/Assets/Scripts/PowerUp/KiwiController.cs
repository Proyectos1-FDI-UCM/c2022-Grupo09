using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour
{

    #region references
   
    #endregion

    #region parameters

    [SerializeField]
    private float _SlowDown = 0.25f; //Velocidad de relentización de tiempo
    [SerializeField]
    private float _moreVelocity = 2f;


    #endregion

    #region methods

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovementController check = collision.gameObject.GetComponent<CharacterMovementController>();
        if (check != null)
        {
            check.PlusVelocity(_moreVelocity);
            Time.timeScale = _SlowDown;
            Destroy(this.gameObject);
        }
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
        

    }
   
}
