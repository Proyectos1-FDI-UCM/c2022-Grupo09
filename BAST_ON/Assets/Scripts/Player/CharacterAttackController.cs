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
    private float _bastonazoTime = 0.2f;
    [SerializeField]
    private float _attackCooldown = 0.3f;
    [SerializeField]
    private float _jumpsCooldown = 0.3f;
    [SerializeField]
    private float _dashCooldown = 0.3f;
    [SerializeField] private float repelStrenght = 15;
    #endregion

    #region properties
    private float _elapsedBastonazoTime = 0;
    private float _elapsedAttackCooldownTime = 0;
    private float _elapsedJumpCooldownTime = 0;
    private float _elapsedDashCooldownTime = 0;
    private float _defaultDirection;
    private Quaternion _originalAttackRotation;
    public float RepelStrenght => repelStrenght;
    private float _originalRepelStrenght;
    #endregion

    #region methods
    public void IncreaseStrenght(float strenghtModifier)
    {
        repelStrenght = _originalRepelStrenght * strenghtModifier;
    }
    public void DecreaseStrenght(float strenghtModifier)
    {
        repelStrenght = _originalRepelStrenght;
    }

    public void SetDefaultDirection(float dir)
    {
        _defaultDirection = dir;
    }
    private void RedirectFloorAttack(ref float angle)
    {
        if (_myFloorDetector.IsGrounded() && angle <= -45)
        {
            angle = 0;
        }
    }

    public void Bastonazo(float angle)
    {
        if (!_baston.activeSelf)
        {
            _bastonTransform.rotation = Quaternion.Euler(0, 0, angle);
            _originalAttackRotation = _bastonTransform.rotation;
            _baston.SetActive(true);
            _elapsedBastonazoTime = 0f;
        }
    }
    public void Golpe(float horizontalAttackDirection, float verticalAttackDirection)
    {
        if (_elapsedAttackCooldownTime > _attackCooldown)
        {
            _elapsedAttackCooldownTime = 0f;
            // Devuelve en 1er y 2o cuadrante
            float angle = Mathf.Rad2Deg * Mathf.Acos(horizontalAttackDirection);
            //  Si está en 3er o 4o cuadrante cambia de signo
            if (verticalAttackDirection < 0) angle = -angle;
            else if (verticalAttackDirection == 0 && horizontalAttackDirection == 0) angle = 0;
            // Si chicho está en el suelo y pega hacia abajo redirige el golpe
            RedirectFloorAttack(ref angle);

            // Arriba
            if (angle <= 90 && angle > 67.5f) angle = 90;
            // Arriba frente
            else if (angle > 22.5f) angle = 45;
            // Frente
            else if (angle > -45) angle = 0;
            // Abajo
            else /*if (angle > -90)*/ angle = -90;

            if (_defaultDirection >= 0) Bastonazo(angle);
            else Bastonazo(180 - angle);
        }
    }

    public void Salto()
    {
        // No en el suelo
        if (_elapsedJumpCooldownTime > _jumpsCooldown && !_myFloorDetector.IsGrounded())
        {
            _elapsedJumpCooldownTime = 0f;
            int inWall = _myWallDetector.isInWall();
            // En el aire
            if (inWall == 0) Bastonazo(-90);
            // En la pared
            else
            {
                // En pared a la derecha
                if (inWall == 1) Bastonazo(-57);
                // En pared a la izquierda
                else if (inWall == -1) Bastonazo(180 + 57);
                _myMovementController.WallWasAttacked(true);
            }
        }
    }

    public void Dash()
    {
        if (_elapsedDashCooldownTime > _dashCooldown && !_myFloorDetector.IsGrounded())
        {
            _elapsedDashCooldownTime = 0f;
            if (_myWallDetector.isInWall() == 1) Bastonazo(180 + 45);
            else if (_myWallDetector.isInWall() == -1) Bastonazo(-45);
            else
            {
                if (_defaultDirection >= 0) Bastonazo(180 + 45);
                else Bastonazo(-45);
            }
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
        _originalRepelStrenght = repelStrenght;
    }

    // Update is called once per frame
    void Update()
    {
        // Si el ataque ha empezado empieza a contar
        if (_baston.activeSelf)
        {
            _bastonTransform.rotation = _originalAttackRotation;
            _elapsedBastonazoTime += Time.deltaTime;
            // Cuando el ataque se haya completado desactiva el bastón
            if (_elapsedBastonazoTime > _bastonazoTime) _baston.SetActive(false);
        }
        // Cooldown del ataque
        else if (_elapsedAttackCooldownTime < _attackCooldown)
        {
            _elapsedAttackCooldownTime += Time.deltaTime;
        }
        // Cooldown de saltos
        else if (_elapsedJumpCooldownTime < _jumpsCooldown)
        {
            _elapsedJumpCooldownTime += Time.deltaTime;
        }
        // Cooldown en dash
        else if (_elapsedDashCooldownTime < _dashCooldown)
        {
            _elapsedDashCooldownTime += Time.deltaTime;
        }

        if (_myWallDetector.isInWall() == 1 && !_myFloorDetector.IsGrounded()) _defaultDirection = -1;
        else if (_myWallDetector.isInWall() == -1 && !_myFloorDetector.IsGrounded()) _defaultDirection = 1;
    }
}