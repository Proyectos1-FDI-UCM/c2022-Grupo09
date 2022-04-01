using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
(de momento) Controlador de el entorno de la bossficht contra J053-J1. Se encarga de activar y desactivar
los bloqueos entre fases.

@TODO Administración de estado de J053-J1 y movimiento del mismo. 



*/

public class BossfightController : MonoBehaviour
{

    [SerializeField] private GameObject _transitionFloor;
    [SerializeField] private GameObject _exitDoor;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    /*La idea de estos metodos es que se puedan llamar desde, por ejemplo, el GameManager
     cuando las cosas que se tengan que cumplir en la bossfight ocurran, de una manera directa, dando legibilidad al codigo.*/
    public void OpenTransitionFloor()
    {
        _transitionFloor.SetActive(false);
    }

    public void openExitDoor()
    {
        _exitDoor.SetActive(false);
    }




}
