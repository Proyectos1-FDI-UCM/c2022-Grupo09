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
    private WallDetector _myWallDetector;
    private CharacterMovementController _myMovementController;
    #endregion

    #region parameters
    [SerializeField]
    private float _attackTime = 1f;
    [SerializeField]
    private float _attackCooldown = 1f;
    [SerializeField] private float repelStrenght = 15;
    #endregion

    #region properties
    private float _elapsedAttackTime;
    private float _elapsedCooldownTime;
    private float _defaultDirection;
    private Quaternion _originalAttackRotation;
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

            // Walljump
            if (_myWallDetector.isInWall())
            {
                if (_defaultDirection > 0) _bastonTransform.Rotate(Vector3.forward, -52);
                else _bastonTransform.Rotate(Vector3.forward, -127);
            }

            // Si se ha escogido una direcci�n para el ataque
            else if (horizontalAttackDirection != 0 || verticalAttackDirection != 0)
            {
                // Arriba
                if ((horizontalAttackDirection >= 0) &&
                    (horizontalAttackDirection <= _cotaResta) &&
                    (verticalAttackDirection <= 1) &&
                    verticalAttackDirection >= _cotaSuma)
                { _bastonTransform.Rotate(Vector3.forward, 90); }
                // Frente arriba
                else if ((horizontalAttackDirection > _cotaResta) &&
                    (horizontalAttackDirection <= _cotaSuma) &&
                    (verticalAttackDirection < _cotaSuma) &&
                    (verticalAttackDirection >= _cotaResta))
                {
                    if (_defaultDirection > 0) _bastonTransform.Rotate(Vector3.forward, 45);
                    else _bastonTransform.Rotate(Vector3.forward, 135);
                }
                // Frente (default)
                // Frente abajo
                /*else if ((horizontalAttackDirection > _cotaResta) &&
                    (horizontalAttackDirection <= _cotaSuma) &&
                    (verticalAttackDirection > -_cotaSuma) &&
                    (verticalAttackDirection <= -_cotaResta))
                {
                    if (_defaultDirection > 0) _bastonTransform.Rotate(Vector3.forward, -45);
                    else _bastonTransform.Rotate(Vector3.forward, -135);
                }*/
                // Abajo
                else if ((horizontalAttackDirection >= 0) &&
                    (horizontalAttackDirection <= Mathf.Sqrt(2) / 2) &&
                    (verticalAttackDirection >= -1) &&
                    (verticalAttackDirection) <= -Mathf.Sqrt(2) / 2)
                { _bastonTransform.Rotate(Vector3.forward, -90); }
                // hacia la izquierda si se mueve a la izquierda
                else if (horizontalAttackDirection > 0 && _defaultDirection < 0) _bastonTransform.Rotate(Vector3.forward, 180);
            }
            else if (_defaultDirection < 0) _bastonTransform.Rotate(Vector3.forward, 180);

            _originalAttackRotation = _bastonTransform.rotation;
            _baston.SetActive(true);
        }
    }

    public void Dash()
    {
        if (!_baston.activeSelf && _elapsedCooldownTime > _attackCooldown && !_myFloorDetector.IsGrounded())
        {
            _elapsedAttackTime = 0f;
            _bastonTransform.rotation = Quaternion.identity;
            if (_defaultDirection > 0) _bastonTransform.Rotate(Vector3.forward, 225f);
            else _bastonTransform.Rotate(Vector3.forward, -45f);
            _originalAttackRotation = _bastonTransform.rotation;
            _baston.SetActive(true);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myFloorDetector = GetComponent<FloorDetector>();
        _myWallDetector = GetComponent<WallDetector>();
        _myMovementController = GetComponent<CharacterMovementController>();
        _bastonTransform = _baston.transform;
        _baston.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el ataque ha empezado empieza a contar
        if (_baston.activeSelf)
        {
            _bastonTransform.rotation = _originalAttackRotation;
            if (_myWallDetector.isInWall()) _myMovementController.WallWasAttacked(true);
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
