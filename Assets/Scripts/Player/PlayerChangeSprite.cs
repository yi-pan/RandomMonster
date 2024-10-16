using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeSprite : MonoBehaviour
{
    private GameController _gameController;
    private GameObject _playerGraphics;
    private Sprite _newPlayerSprite;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _originalSpriteSize;
    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _newPlayerSprite = _gameController.newPlayerSprite;
        _playerGraphics = GameObject.Find("Player Graphics").gameObject;
        _spriteRenderer = _playerGraphics.GetComponent<SpriteRenderer>();
        // Get the current sprite's size in world units
        _originalSpriteSize = _spriteRenderer.sprite.bounds.size;
        Debug.Log(_originalSpriteSize);
        ChangeSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSprite()
    {
        if (_newPlayerSprite != null)
        {
            // Set the new sprite
            _spriteRenderer.sprite = _newPlayerSprite;

            // Get the new sprite's size in world units
            Vector2 newSpriteSize = _spriteRenderer.sprite.bounds.size;
            Debug.Log(newSpriteSize);
            // Calculate the scale needed to match the original sprite's size
            Vector3 newScale = transform.localScale;
            newScale.x *= _originalSpriteSize.x / newSpriteSize.x;
            newScale.y *= _originalSpriteSize.y / newSpriteSize.y;

            // Apply the new scale to maintain the original sprite's size
            _playerGraphics.transform.localScale = newScale;
        }
    }
}
