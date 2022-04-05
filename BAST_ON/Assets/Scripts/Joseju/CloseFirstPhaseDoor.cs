using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFirstPhaseDoor : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _door;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            _door.SetActive(true);
        }
    }
    #endregion
}
