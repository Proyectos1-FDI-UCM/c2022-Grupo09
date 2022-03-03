using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    ///<summary>
    ///Referencia al jugador
    ///</summary>
    [SerializeField] private GameObject _player;
    ///<summary>
    ///Vector que desplaza el centro de la cámara.
    ///</summary>
    [SerializeField] private Vector3 _offset = new Vector3(1, 0, -10);
    ///<summary>
    ///Distancia que se aleja la cámara del jugador al mirar arriba o abajo
    ///</summary>
    [SerializeField] private float _verticalOffset = 3f;
    ///<summary>
    ///Tiempo que tiene el jugador que mantener arriba o abajo para mirar ahí
    ///</summary>
    [SerializeField] private float _lookUpTime = 2f;
    ///<summary>
    ///Valor que determina a qué distancia se queda la cámara del punto final
    ///</summary>
    [SerializeField] private float _lerpSpeed = 0.5f;


    ///<summary>
    ///Tiempo que tiene el jugador que mantener arriba o abajo para mirar ahí
    ///</summary>
    private float _lookUpElapsedTime = 0;
    ///<summary>
    ///Referencia al transform del Player
    ///</summary>
    private Transform _playerTransform;
    ///<summary>
    ///Referencia al transform de la cámara
    ///</summary>
    private Transform _cameraTransform;
    ///<summary>
    ///Valor interno para el cálculo de las posiciones
    ///</summary>
    private Vector3 _nextPosition;

    #region methods
    public void SetOffset(Vector3 direction)
    {
        _offset.x = Mathf.Abs(_offset.x) * direction.x;
        ResetVerticalOffset();
    }
    public void SetVerticalOffset(float vDirection)
    {
        _verticalOffset = Mathf.Abs(_verticalOffset) * (vDirection / Mathf.Abs(vDirection));
        if (vDirection != 0 && _lookUpElapsedTime < _lookUpTime) _lookUpElapsedTime += Time.deltaTime;
    }
    public void ResetVerticalOffset()
    {
        _lookUpElapsedTime = 0;
        _offset.y = 0;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Se rellenan las referencias a los Transform
        _playerTransform = _player.GetComponent<Transform>();
        _cameraTransform = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    
    private void Update() {
        
    }
    void LateUpdate()
    {
        if (_lookUpElapsedTime > _lookUpTime && _offset.y != _verticalOffset) _offset.y = _verticalOffset;
        _nextPosition = Vector3.Lerp(_cameraTransform.position, _playerTransform.position + _offset, _lerpSpeed);
        //Actualiza el movimiento de la cam.
        _cameraTransform.position = _nextPosition;
    }
}
