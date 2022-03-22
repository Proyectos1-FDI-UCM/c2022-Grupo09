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

    [SerializeField] private float parallaxEffect = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        myTrasform = gameObject.GetComponent<Transform>();
        camTransform = gameObject.GetComponent<Transform>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();


        _startPos = myTrasform.position.x;
        _lenght = mySpriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (camTransform.position.x * parallaxEffect);

        myTrasform.position = new Vector3(_startPos + distance, myTrasform.position.y, myTrasform.position.z);

    }
}
