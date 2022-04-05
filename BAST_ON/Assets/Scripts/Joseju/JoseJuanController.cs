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
    /// Referencia al objeto con la posición de Joseju en la fase 1
    /// </summary>
    [SerializeField] private GameObject _firstPhasePositionObject;
    /// <summary>
    /// Referencai a la posición de Joseju en la fase 1
    /// </summary>
    private Vector3 _firstPhasePosition;
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
                else if (_currentPhase == 1)
                {
                    EndFirstPhase();
                    StartSecondPhase();
                }
                else EndSecondPhase();
            }
        }
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
    /// Método que termina la primera fase del boss
    /// </summary>
    public void EndFirstPhase()
    {

    }
    /// <summary>
    /// Método que empieza la segunda fase del boss
    /// </summary>
    public void StartSecondPhase()
    {
        Physics2D.IgnoreCollision(_playerCollider, _josejuCollider, true);
        _currentPhase = 2;
        _josejuCollider.enabled = true;
        _canBeHit = false;
    }
    /// <summary>
    /// Método que deja indefenso a Jose Juan y permite a Chicho golpearle en la segunda fase
    /// </summary>
    public void EndingSecondPhase()
    {
        _canBeHit = true;
        Physics2D.IgnoreCollision(_playerCollider, _josejuCollider, true);
    }
    /// <summary>
    /// Método que termina la segunda fase del boss
    /// </summary>
    public void EndSecondPhase()
    {

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

        _playerCollider = _player.GetComponent<CapsuleCollider2D>();
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
    }
}
