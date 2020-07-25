using Assets.GameEngine;
using UnityEngine;

public class SpaceSelect : MonoBehaviour
{
    GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        _gameManager.SpaceSelect(gameObject);
    }
}
