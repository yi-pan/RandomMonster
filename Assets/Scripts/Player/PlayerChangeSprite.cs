using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeSprite : MonoBehaviour
{
    private GameController _gameController;
    private Sprite _newPlayerSprite;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _newPlayerSprite = _gameController.newPlayerSprite;
        _spriteRenderer = GameObject.Find("Player Graphics").gameObject.GetComponent<SpriteRenderer>();
        ChangeSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSprite()
    {
        if(_spriteRenderer != null && _newPlayerSprite != null)
        {
            _spriteRenderer.sprite = _newPlayerSprite;
        }
    }
}
