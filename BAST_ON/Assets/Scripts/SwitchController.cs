using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    #region references

    [SerializeField]
    private GameObject _myDoor;

    public Animator _switch;
    public Animator _door;
    #endregion

    #region methods
    private void OnTriggerEnter(Collider collision)
    {
        _switch.SetBool("ON", true);
        _door.SetBool("OPEN", true);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _switch.SetBool("ON", false);
        _door.SetBool("OPEN", false);

    }
}

   
