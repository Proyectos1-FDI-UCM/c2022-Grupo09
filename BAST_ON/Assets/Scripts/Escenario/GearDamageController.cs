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
    [SerializeField]
    private int _speed = 1;
    #endregion

    #region parameters
    private bool _wasHit = false;
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

        if (_wasHit)
        {
            JoseJuanController joseju = collision.gameObject.GetComponent<JoseJuanController>();
            if (joseju != null) joseju.ChangeSecondFaseHealth(-1);
            Destroy(gameObject);
        }
    }
    
    public void CambiaRotacion(float rotation)
    {
        _gearTransform.rotation = Quaternion.Euler(0, 0, rotation);
        _wasHit = true;
    }
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
