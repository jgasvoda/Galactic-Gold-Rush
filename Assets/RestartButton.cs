using Assets.GameEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { _gameManager.RestartGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnMouseUp()
    //{
    //    _gameManager.RestartGame();
    //}
}
