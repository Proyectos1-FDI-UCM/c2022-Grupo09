using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDamageController : MonoBehaviour
{
    #region references
    private Transform _gearTransform;
    #endregion
    #region parameters
    [SerializeField]
    private int _gearDamage = 1;

    private int _speed = 1;
    #endregion

    #region methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character_HealthManager player = collision.gameObject.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            player.ChangeHealthValue(-_gearDamage);
            Destroy(gameObject);
        }
    }
    //Falta hacer que haga daño a Jose01
    /*public void CambiaRotacion(float rotation)
    {
        _gearTransform.rotation = Quaternion.identity;
        _gearTransform.Rotate(Vector3.forward, rotation);
    }*/

    #endregion

    private void Start()
    {
        _gearTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
