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
    private int _currentFirstPhaseWave = 1;
    /// <summary>
    /// Valor que indica los engranajes restantes para que Chicho pueda golpear a Jose Juan
    /// </summary>
    private int _currentSecondPhaseHealth;
    #endregion

    #region references
    PolygonCollider2D _josejuCollider;
    /// <summary>
    /// Referencia a todas las oleadas de enemigos (plataformas y enemigos) de la primera fase
    /// </summary>
    [SerializeField] GameObject[] _firstPhaseWaves;
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
        // Quitar oleada actual
        // aumentar _currentFirstPhaseWave
        // Colocar nueva oleada actual
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _josejuCollider = GetComponent<PolygonCollider2D>();
        _currentSecondPhaseHealth = _maxSecondPhaseHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
