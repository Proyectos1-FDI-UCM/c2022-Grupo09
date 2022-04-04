using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxController : MonoBehaviour
{
    private float _lenght, _startPos;
    private Transform myTrasform, camTransform;
    private SpriteRenderer mySpriteRenderer;


    
    [SerializeField] private GameObject cam;

    [SerializeField, Range(-1.0f,1.0f)] private float parallaxEffect = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        myTrasform = GetComponent<Transform>();
        camTransform = cam.GetComponent<Transform>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();


        _startPos = myTrasform.position.x;
        _lenght = mySpriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_lenght);
        float distance = (camTransform.position.x * parallaxEffect);
        float relativeDistance = (camTransform.position.x * (1 - parallaxEffect));

        myTrasform.position = new Vector3(_startPos + distance, myTrasform.position.y, myTrasform.position.z);
        
        if(relativeDistance > _startPos + _lenght)
        {
            _startPos += _lenght;
        }
        else if(relativeDistance < _startPos - _lenght) 
        {
            _startPos -= _lenght;
        }

    }

    
}
