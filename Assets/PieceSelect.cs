using GGR_Game_Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceSelect : MonoBehaviour
{
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        _gameManager.PieceSelect(this.gameObject);
    }
}
