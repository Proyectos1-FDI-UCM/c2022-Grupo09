using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchosDamageController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private int _damage = 1;
    #endregion

    #region references
    Transform _myTransform;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            player.ChangeHealthValue(-_damage, _myTransform.up);
        }
    }
    #endregion

    private void Start()
    {
        _myTransform = transform;
    }
}
