using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoseJuanController : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Número de engranajes que recibe Jose Juan en la segunda fase hasta poder ser golpeado por Chicho.
    /// </summary>
    [SerializeField]
    private int _maxSecondPhaseHealth = 5;
    /// <summary>
    /// Velocidad a la que Joseju se dirige a su posición de la fase 1
    /// </summary>
    [SerializeField] private float _toFirstPhasePositionSpeed = 5;
    #endregion

    #region properties
    /// <summary>
    /// Valor que indica si Jose Juan puede ser golpeado por Chicho o no
    /// </summary>
    private bool _canBeHit = true;
    /// <summary>
    /// Valor que indica la fase actual de Jose Juan
    /// </summary>
    private int _currentPhase = 0;
    /// <summary>
    /// Valor que indica la oleada actual de la primera fase (posición en el array de oleadas)
    /// </summary>
    private int _currentFirstPhaseWave = 0;
    /// <summary>
    /// Valor que indica los engranajes restantes para que Chicho pueda golpear a Jose Juan
    /// </summary>
    private int _currentSecondPhaseHealth;
    /// <summary>
    /// Valor que indica si Joseju tiene que ir a su posición en la primera fase
    /// </summary>
    private bool _moveToFirstFase = false;
    #endregion

    #region references
    /// <summary>
    /// Referencia al jugador
    /// </summary>
    [SerializeField] private GameObject _player;
    private Transform _playerTransform;
    [SerializeField] private GameObject _baston;
    private BoxCollider2D _bastonCollider;
    /// <summary>
    /// Referencia al collider del jugador
    /// </summary>
    private CapsuleCollider2D _playerCollider;
    /// <summary>
    /// Referencia al collider de Joseju
    /// </summary>
    private PolygonCollider2D _josejuCollider;
    /// <summary>
    /// Referencia al spawner del inicio de la bossfight
    /// </summary>
    [SerializeField] private GameObject _phaseZeroSpawner;
    /// <summary>
    /// Referencia a la puerta entre fase 0 y 1
    /// </summary>
    [SerializeField] private GameObject _toFirstPhaseDoor;
    /// <summary>
    /// Referencia a todas las oleadas de enemigos (plataformas y enemigos) de la primera fase
    /// </summary>
    [SerializeField] private GameObject[] _firstPhaseWaves;
    /// <summary>
    /// Referencia a los enemigos de la oleada actual
    /// </summary>
    private List<WaveEnemy> _waveEnemies;
    /// <summary>
    /// Referencia al transform de Joseju
    /// </summary>
    private Transform _josejuTransform;
    /// <summary>
    /// Referencia al objeto con la posición de Joseju en la fase 0
    /// </summary>
    [SerializeField] private GameObject _phaseZeroPositionObject;
    /// <summary>
    /// Referencia al objeto con la posición de Joseju en la fase 1
    /// </summary>
    [SerializeField] private GameObject _firstPhasePositionObject;
    /// <summary>
    /// Referencai a la posición de Joseju en la fase 1
    /// </summary>
    private Vector3 _firstPhasePosition;
    /// <summary>
    /// Referencia a la puerta entre la fase 1 y 2
    /// </summary>
    [SerializeField] private GameObject _toSecondPhaseDoor;
    /// <summary>
    /// Referencia al spawner de engranajes
    /// </summary>
    [SerializeField] private GameObject _gearSpawner;
    private SpawnerGearController _gearSpawnerController;
    [SerializeField] private GameObject _parkourFinishObject;
    private Vector3 _parkourFinish;
    [SerializeField] GameObject _exitSecondPhaseDoor;
    /// <summary>
    /// Referencia a la cámara
    /// </summary>
    [SerializeField] private GameObject _camera;
    private CameraController _cameraController;
    private Transform _cameraTransform;
    [SerializeField] private GameObject _cameraPhaseZeroPositionObject;
    private Vector3 _cameraPhaseZeroPosition;
    [SerializeField] private GameObject _cameraFirstPhasePositionObject;
    private Vector3 _cameraFirstPhasePosition;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canBeHit)
        {
            BastonImpulseController baston = collision.GetComponent<BastonImpulseController>();
            if (baston != null)
            {
                if (_currentPhase == 0) StartFirstPhase();
                else if (_currentPhase == 1) StartSecondPhase();
                else EndSecondPhase();
            }
        }
    }
    public void StartJoseju()
    {
        _cameraController.enabled = false;
        _currentPhase = 0;
        _canBeHit = true;
        _currentFirstPhaseWave = 0;
        _currentSecondPhaseHealth = _maxSecondPhaseHealth;
        _gearSpawner.SetActive(false);
        _moveToFirstFase = false;
        Physics2D.IgnoreCollision(_josejuCollider, _playerCollider, false);
        Physics2D.IgnoreCollision(_josejuCollider, _bastonCollider, false);
        _phaseZeroSpawner.SetActive(true);
        _toFirstPhaseDoor.SetActive(true);
        _josejuTransform.position = _phaseZeroPositionObject.transform.position;
        _toSecondPhaseDoor.SetActive(true);
        _exitSecondPhaseDoor.SetActive(true);
        _camera.transform.position = _cameraPhaseZeroPosition;
    }
    /// <summary>
    /// Método que coloca la cámara en la posición que tendrá en la primera fase
    /// </summary>
    public void PlaceFirstPhaseCamera()
    {
        _cameraTransform.position = _cameraFirstPhasePosition;
    }

    /// <summary>
    /// Método que comienza la primera fase del boss
    /// </summary>
    public void StartFirstPhase()
    {
        _phaseZeroSpawner.SetActive(false);
        _toFirstPhaseDoor.SetActive(false);

        _moveToFirstFase = true;

        _firstPhaseWaves[_currentFirstPhaseWave].SetActive(true);

        _currentPhase = 1;
        _canBeHit = false;
        _josejuCollider.enabled = false;
    }
    /// <summary>
    /// Método que deja indefenso a Jose Juan y permite a Chicho golpearle en la primera fase
    /// </summary>
    public void EndingFirstPhase()
    {
        _canBeHit = true;
        _josejuCollider.enabled = true;
    }
    /// <summary>
    /// Método que empieza la segunda fase del boss
    /// </summary>
    public void StartSecondPhase()
    {
        Physics2D.IgnoreCollision(_playerCollider, _josejuCollider, true);
        Physics2D.IgnoreCollision(_bastonCollider, _josejuCollider, true);
        _currentPhase = 2;
        _josejuCollider.enabled = true;
        _canBeHit = false;

        _cameraController.enabled = true;
        _toSecondPhaseDoor.SetActive(false);
        _gearSpawner.SetActive(true);
    }
    /// <summary>
    /// Método que deja indefenso a Jose Juan y permite a Chicho golpearle en la segunda fase
    /// </summary>
    public void EndingSecondPhase()
    {
        _canBeHit = true;
        Physics2D.IgnoreCollision(_playerCollider, _josejuCollider, false);
        Physics2D.IgnoreCollision(_bastonCollider, _josejuCollider, false);
    }
    /// <summary>
    /// Método que termina la segunda fase del boss
    /// </summary>
    public void EndSecondPhase()
    {
        _gearSpawner.SetActive(false);
        _exitSecondPhaseDoor.SetActive(false);
    }
    /// <summary>
    /// Método que cambia los engranajes restantes para que Jose Juan pueda ser golpeado por Chicho
    /// </summary>
    public void ChangeSecondPhaseHealth(int value)
    {
        _currentSecondPhaseHealth += value;
        if (_currentSecondPhaseHealth <= 0) EndingSecondPhase();
    }

    /// <summary>
    /// Método que cambia a la siguiente oleada de enemigos
    /// </summary>
    public void NextWave()
    {
        _waveEnemies.Clear();
        _firstPhaseWaves[_currentFirstPhaseWave].SetActive(false);
        _currentFirstPhaseWave++;
        _firstPhaseWaves[_currentFirstPhaseWave].SetActive(true);
    }
    public void RegisterWaveEnemy(WaveEnemy enemy)
    {
        _waveEnemies.Add(enemy);
    }
    public bool WaveEnd(List<WaveEnemy> waveEnemies)
    {
        bool emptyWave = true;
        int i = 0;
        while (i < waveEnemies.Count && emptyWave)
        {
            if (waveEnemies[i] != null) emptyWave = false;
            i++;
        }
        return emptyWave;
    }

    public void FollowCamera()
    {
        _josejuTransform.Translate((Vector2)_cameraTransform.position - (Vector2)_josejuTransform.position);
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _josejuCollider = GetComponent<PolygonCollider2D>();
        // Valor inicial de la vida
        _currentSecondPhaseHealth = _maxSecondPhaseHealth;

        _josejuTransform = transform;
        _firstPhasePosition = _firstPhasePositionObject.transform.position;
        // Inicializar lista de enemigos
        _waveEnemies = new List<WaveEnemy>();

        _bastonCollider = _baston.GetComponent<BoxCollider2D>();
        _playerCollider = _player.GetComponent<CapsuleCollider2D>();
        _playerTransform = _player.transform;

        _gearSpawnerController = _gearSpawner.GetComponent<SpawnerGearController>();
        _parkourFinish = _parkourFinishObject.transform.position;

        _cameraTransform = _camera.transform;
        _cameraController = _camera.GetComponent<CameraController>();
        _cameraPhaseZeroPosition = _cameraPhaseZeroPositionObject.transform.position;
        _cameraFirstPhasePosition = _cameraFirstPhasePositionObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveToFirstFase)
        {
            _josejuTransform.Translate((_firstPhasePosition - _josejuTransform.position).normalized * _toFirstPhasePositionSpeed * Time.deltaTime);

            if (_josejuTransform.position.x >= _firstPhasePosition.x) _moveToFirstFase = false;
        }

        if (_currentPhase == 1)
        {
            if (WaveEnd(_waveEnemies))
            {
                if (_currentFirstPhaseWave < _firstPhaseWaves.Length - 1) NextWave();
                else EndingFirstPhase();
            }
        }
        if (_currentPhase == 2)
        {
            _cameraTransform.position = _cameraTransform.position.y * Vector3.up + _cameraFirstPhasePosition.x * Vector3.right + _cameraTransform.position.z * Vector3.forward;
            if (_playerTransform.position.y > _parkourFinish.y) _gearSpawnerController.DuplicateFrecuence();
            FollowCamera();
        }
    }
}
