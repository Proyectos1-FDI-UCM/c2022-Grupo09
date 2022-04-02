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
    private int _maxSecondFaseHealth = 5;
    #endregion

    #region properties
    private int _currentSecondFaseHealth;
    /// <summary>
    /// Valor que indica si Jose Juan puede ser golpeado por Chicho o no
    /// </summary>
    private bool _canBeHit = true;
    /// <summary>
    /// Valor que indica la fase actual de Jose Juan
    /// </summary>
    private int _currentFase = 0;
    #endregion

    #region references
    PolygonCollider2D _josejuCollider;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canBeHit)
        {
            BastonImpulseController baston = collision.GetComponent<BastonImpulseController>();
            if (baston != null)
            {
                if (_currentFase == 0) StartFirstFase();
                else if (_currentFase == 1)
                {
                    EndFirstFase();
                    StartSecondFase();
                }
                else EndSecondFase();
            }
        }
    }
    /// <summary>
    /// Método que comienza la primera fase del boss
    /// </summary>
    public void StartFirstFase()
    {
        _currentFase = 1;
        _canBeHit = false;
        _josejuCollider.enabled = false;
    }
    /// <summary>
    /// Método que termina la primera fase del boss
    /// </summary>
    public void EndFirstFase()
    {

    }
    /// <summary>
    /// Método que empieza la segunda fase del boss
    /// </summary>
    public void StartSecondFase()
    {
        _currentFase = 2;
        _josejuCollider.enabled = true;
        _canBeHit = false;
    }
    /// <summary>
    /// Método que termina la segunda fase del boss
    /// </summary>
    public void EndSecondFase()
    {

    }
    /// <summary>
    /// Método que deja indefenso a Jose Juan y permite a Chicho golpearle
    /// </summary>
    public void EndingFase()
    {
        _canBeHit = true;
        _josejuCollider.enabled = true;
    }
    /// <summary>
    /// Método que cambia los engranajes restantes para que Jose Juan pueda ser golpeado por Chicho
    /// </summary>
    public void ChangeSecondFaseHealth(int value)
    {
        _currentSecondFaseHealth += value;
        if (_currentSecondFaseHealth <= 0) _canBeHit = true;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _josejuCollider = GetComponent<PolygonCollider2D>();
        _currentSecondFaseHealth = _maxSecondFaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
