using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenu_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTest1()
    {
        try
        {
        SceneManager.LoadSceneAsync("Test1");
        }
        catch(UnassignedReferenceException e)
        {
            Debug.Log("ERROR: escena no implementada: " + e);
        }
    }

    public void GoToTest2()
    {
        try
        {
        SceneManager.LoadSceneAsync("PruebaEnemigos");
        }
        catch(UnassignedReferenceException e)
        {
            Debug.Log("ERROR: escena no implementada: " + e);
        }
    }

    public void GoToTest3()
    {
        try
        {
        SceneManager.LoadSceneAsync("PracticasPowerups");
        }
        catch(UnassignedReferenceException e)
        {
            Debug.Log("ERROR: escena no implementada: " + e);
        }
    }

    public void GoToTest4()
    {
        try
        {
        SceneManager.LoadSceneAsync("PruebaBossfight");
        }
        catch(UnassignedReferenceException e)
        {
            Debug.Log("ERROR: escena no implementada: " + e);
        }
    }

    public void GoToMainScene()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

}
