using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _baston;
    private Transform _bastonTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float _attackTime = 1f;
    [SerializeField]
    private float _attackCooldown = 1f;
    #endregion

    #region properties
    private float _elapsedAttackTime;
    private float _elapsedCooldownTime;
    private float _defaultDirection;
    ///<summary>
    ///Es cos(22'5º) y sen(3*22'5º).
    ///Utilizado para delimitar las areas de input que corresponden a cada dirección de ataque.
    ///</summary>
    private float _cotaSuma = ((Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2);
    ///<summary>
    ///Es sen(22'5º) y cos(3*22'5º).
    ///Utilizado para delimitar las areas de input que corresponden a cada dirección de ataque.
    ///</summary>
    private float _cotaResta = ((Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2);
    #endregion

    #region methods
    public void SetDefaultDirection(float dir)
    {
        _defaultDirection = dir;
    }
    // Recibe la dirección del ataque y lo activa en esa dirección
    public void Bastonazo(float horizontalAttackDirection, float verticalAttackDirection)
    {
        // Solo entra en el método si no hay ya un ataque
        if (!_baston.activeSelf && _elapsedCooldownTime > _attackCooldown)
        {
            _elapsedAttackTime = 0f;
            _bastonTransform.rotation = Quaternion.identity;
            // Si se ha escogido una dirección para el ataque
            if (horizontalAttackDirection != 0 || verticalAttackDirection != 0)
            {
                // a (-22'5º, 22'5º) Derecha
                // Es la rotación 0, no hace falta cambiarla

                // b (22'5º, 3*22'5º) Arriba derecha
                if ((horizontalAttackDirection < _cotaSuma) &&
                        (horizontalAttackDirection >= _cotaResta) &&
                        (verticalAttackDirection >= _cotaResta) &&
                        (verticalAttackDirection < _cotaSuma))
                { _bastonTransform.Rotate(new Vector3(0, 0, 45)); }
                // c (3*22'5º, 5*22'5º) Arriba
                else if ((horizontalAttackDirection < _cotaResta) &&
                        (horizontalAttackDirection >= -_cotaResta) &&
                        (verticalAttackDirection >= _cotaSuma))
                { _bastonTransform.Rotate(new Vector3(0, 0, 90)); }
                // d (5*22'5º, 7*22'5º) Arriba izquierda
                else if ((horizontalAttackDirection < -_cotaResta) &&
                        (horizontalAttackDirection >= -_cotaSuma) &&
                        (verticalAttackDirection < _cotaSuma) &&
                        (verticalAttackDirection >= _cotaResta))
                { _bastonTransform.Rotate(new Vector3(0, 0, 135)); }
                // e (7*22'5º, -7*22'5º) Izquierda
                else if ((horizontalAttackDirection < -_cotaSuma) &&
                        (verticalAttackDirection < _cotaResta) &&
                        (verticalAttackDirection >= -_cotaResta))
                { _bastonTransform.Rotate(new Vector3(0, 0, 180)); }
                // f (-7*22'5º, -5*22'5º) Abajo izquierda
                else if ((horizontalAttackDirection >= -_cotaSuma) &&
                        (horizontalAttackDirection < -_cotaResta) &&
                        (verticalAttackDirection < -_cotaResta) &&
                        (verticalAttackDirection >= -_cotaSuma))
                { _bastonTransform.Rotate(new Vector3(0, 0, 225)); }
                // g (-5*22'5º, -3*22'5º) Abajo
                else if ((horizontalAttackDirection >= -_cotaResta) &&
                        (horizontalAttackDirection < _cotaResta) &&
                        (verticalAttackDirection < -_cotaSuma))
                { _bastonTransform.Rotate(new Vector3(0, 0, 270)); }
                // h (-3*22'5º, -22'5º) Abajo derecha
                else if ((horizontalAttackDirection >= _cotaResta) &&
                        (horizontalAttackDirection < _cotaSuma) &&
                        (verticalAttackDirection >= -_cotaSuma) &&
                        (verticalAttackDirection < -_cotaResta))
                { _bastonTransform.Rotate(new Vector3(0, 0, 315)); }
            }
            // Si no se escoge dirección del ataque, el ataque es en la dirección del jugador
            else
            {
                if (_defaultDirection < 0) { _bastonTransform.Rotate(new Vector3(0, 0, 180)); }
                // else Rotación original
            }
            _baston.SetActive(true);
            Debug.Log(horizontalAttackDirection);
            Debug.Log(verticalAttackDirection);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _bastonTransform = _baston.transform;
        _baston.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el ataque ha empezado empieza a contar
        if (_baston.activeSelf)
        {
            _elapsedAttackTime += Time.deltaTime;
            // Cuando el ataque se haya completado desactiva el bastón
            if (_elapsedAttackTime > _attackTime)
            {
                _baston.SetActive(false);
                _elapsedCooldownTime = 0f;
            }
        }
        else if (_elapsedCooldownTime < _attackCooldown) _elapsedCooldownTime += Time.deltaTime;
    }
}
