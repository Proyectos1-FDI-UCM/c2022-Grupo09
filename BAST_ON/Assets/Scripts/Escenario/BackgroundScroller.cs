using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField, Range(-1.0f,1.0f)] private float _scrollSpeed = 0.5f;
    
    private float offset;
    private Material mat;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * _scrollSpeed / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
