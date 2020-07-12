using GGR_Game_Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceSelect : MonoBehaviour
{
    GameManager _gameManager;
    public PieceSelect()
    {
        _gameManager = GameManager.Instance;
    }
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
        //GetComponent<Renderer>().material.SetColor("_Color", new Color32(255, 171, 0, 255));
        _gameManager.PieceSelect(this.gameObject);
    }
}
