using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        GetComponent<Renderer>().material.SetColor("_Color", new Color32(255, 171, 0, 255));
    }
}
