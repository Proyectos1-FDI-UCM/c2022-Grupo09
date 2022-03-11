using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _baston;
    private Transform _bastonTransform;
    private FloorDetector _myFloorDetector;
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

    [SerializeField] private float repelStrenght = 15;

    public float RepelStrenght => repelStrenght;
    ///<summary>
    ///Es cos(22'5�) y sen(3*22'5�).
    ///Utilizado para delimitar las areas de input que corresponden a cada direcci�n de ataque.
    ///</summary>
    private float _cotaSuma = ((Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2);
    ///<summary>
    ///Es sen(22'5�) y cos(3*22'5�).
    ///Utilizado para delimitar las areas de input que corresponden a cada direcci�n de ataque.
    ///</summary>
    private float _cotaResta = ((Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2);
    #endregion

    #region methods
    
    public void modifyStrenght(float strenghtModifier, float duration)
    {
        StartCoroutine(ModifyStrenghtCoroutine(strenghtModifier, duration));
    }

    IEnumerator ModifyStrenghtCoroutine(float strenghtModifier, float duration){
        repelStrenght *= strenghtModifier;
        yield return new WaitForSeconds(duration);
        repelStrenght /= strenghtModifier;
    }

    public void SetDefaultDirection(float dir)
    {
        _defaultDirection = dir;
    }
    private void RedirectFloorAttack(ref float horizontalAttackDirection, ref float verticalAttackDirection)
    {
        if (verticalAttackDirection < 0)
        {
            verticalAttackDirection = 0;
            horizontalAttackDirection /= Mathf.Abs(horizontalAttackDirection);
        }
    }
    // Recibe la direcci�n del ataque y lo activa en esa direcci�n
    public void Bastonazo(float horizontalAttackDirection, float verticalAttackDirection)
    {
        // Solo entra en el m�todo si no hay ya un ataque
        if (!_baston.activeSelf && _elapsedCooldownTime > _attackCooldown)
        {
            if (_myFloorDetector.IsGrounded()) RedirectFloorAttack(ref horizontalAttackDirection, ref verticalAttackDirection);
            _elapsedAttackTime = 0f;
            _bastonTransform.rotation = Quaternion.identity;
            // Si se ha escogido una direcci�n para el ataque
            if (horizontalAttackDirection != 0 || verticalAttackDirection != 0)
            {
                // a (-22'5�, 22'5�) Derecha
                // Es la rotaci�n 0, no hace falta cambiarla

                // b (22'5�, 3*22'5�) Arriba derecha
                if ((horizontalAttackDirection < _cotaSuma) &&
                        (horizontalAttackDirection >= _cotaResta) &&
                        (verticalAttackDirection >= _cotaResta) &&
                        (verticalAttackDirection < _cotaSuma))
                { _bastonTransform.Rotate(Vector3.forward, 45); }
                // c (3*22'5�, 5*22'5�) Arriba
                else if ((horizontalAttackDirection < _cotaResta) &&
                        (horizontalAttackDirection >= -_cotaResta) &&
                        (verticalAttackDirection >= _cotaSuma))
                { _bastonTransform.Rotate(Vector3.forward, 90); }
                // d (5*22'5�, 7*22'5�) Arriba izquierda
                else if ((horizontalAttackDirection < -_cotaResta) &&
                        (horizontalAttackDirection >= -_cotaSuma) &&
                        (verticalAttackDirection < _cotaSuma) &&
                        (verticalAttackDirection >= _cotaResta))
                { _bastonTransform.Rotate(Vector3.forward, 135); }
                // e (7*22'5�, -7*22'5�) Izquierda
                else if ((horizontalAttackDirection < -_cotaSuma) &&
                        (verticalAttackDirection < _cotaResta) &&
                        (verticalAttackDirection >= -_cotaResta))
                { _bastonTransform.Rotate(Vector3.forward, 180); }
                // f (-7*22'5�, -5*22'5�) Abajo izquierda
                else if ((horizontalAttackDirection >= -_cotaSuma) &&
                        (horizontalAttackDirection < -_cotaResta) &&
                        (verticalAttackDirection < -_cotaResta) &&
                        (verticalAttackDirection >= -_cotaSuma))
                { _bastonTransform.Rotate(Vector3.forward, 225); }
                // g (-5*22'5�, -3*22'5�) Abajo
                else if ((horizontalAttackDirection >= -_cotaResta) &&
                        (horizontalAttackDirection < _cotaResta) &&
                        (verticalAttackDirection < -_cotaSuma))
                { _bastonTransform.Rotate(Vector3.forward, 270); }
                // h (-3*22'5�, -22'5�) Abajo derecha
                else if ((horizontalAttackDirection >= _cotaResta) &&
                        (horizontalAttackDirection < _cotaSuma) &&
                        (verticalAttackDirection >= -_cotaSuma) &&
                        (verticalAttackDirection < -_cotaResta))
                { _bastonTransform.Rotate(Vector3.forward, 315); }
            }
            // Si no se escoge direcci�n del ataque, el ataque es en la direcci�n del jugador
            else
            {
                if (_defaultDirection < 0) { _bastonTransform.Rotate(new Vector3(0, 0, 180)); }
                // else Rotaci�n original
            }
            _baston.SetActive(true);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myFloorDetector = GetComponent<FloorDetector>();
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
            // Cuando el ataque se haya completado desactiva el bast�n
            if (_elapsedAttackTime > _attackTime)
            {
                _baston.SetActive(false);
                _elapsedCooldownTime = 0f;
            }
        }
        else if (_elapsedCooldownTime < _attackCooldown) _elapsedCooldownTime += Time.deltaTime;
    }
}
