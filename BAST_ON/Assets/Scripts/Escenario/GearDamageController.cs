using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDamageController : MonoBehaviour
{
    #region references
    private Transform _gearTransform;
    /// <summary>
    /// Referencia al Collider de Jose Juan si se encuentra con él para poder ignorarlo
    /// </summary>
    private PolygonCollider2D _josejuCollider;
    /// <summary>
    /// Referencia al Collider del engranaje para poder ignorar al Jose Juan
    /// </summary>
    private BoxCollider2D _gearCollider;
    #endregion
    #region parameters
    [SerializeField]
    private int _gearDamage = 1;
    [SerializeField]
    private int _speed = 1;
    #endregion

    #region properties
    /// <summary>
    /// Booleano que indica si el engranaje fue golpeado por el bastón
    /// </summary>
    private bool _wasHit = false;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Daño al jugador
        Character_HealthManager player = collision.gameObject.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            player.ChangeHealthValue(-_gearDamage);
            Destroy(gameObject);
        }

        // Daño a Joseju
        JoseJuanController joseju = collision.gameObject.GetComponent<JoseJuanController>();
        if (joseju != null)
        {
            // Si el jugador ha golpeado al engranaje, el engranaje golpea a Joseju
            if (_wasHit)
            {
                joseju.ChangeSecondPhaseHealth(-_gearDamage);
                Destroy(gameObject);
            }
            // Si el jugador NO ha golpeado al engranaje, ignora la colisión con Joseju
            else
            {
                _josejuCollider = joseju.gameObject.GetComponent<PolygonCollider2D>();
                Physics2D.IgnoreCollision(_josejuCollider, _gearCollider, true);
            }
        }
    }

    public void CambiaRotacion(float rotation)
    {
        _gearTransform.rotation = Quaternion.Euler(0, 0, rotation);
        _wasHit = true;
        // Si se ha ignorado la colisión con Joseju, deja de ignorarla
        if (_josejuCollider != null) Physics2D.IgnoreCollision(_josejuCollider, _gearCollider, false);
    }
    #endregion

    private void Start()
    {
        _gearTransform = transform;
        _gearTransform.rotation = Quaternion.Euler(0, 0, 270);

        _gearCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _gearTransform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
