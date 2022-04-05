using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLimit : MonoBehaviour
{
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GearDamageController gear = collision.GetComponent<GearDamageController>();
        if (gear != null)
        {
            Destroy(gear.gameObject);
        }
    }
    #endregion
}
