using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    private GameController _gameController;

    private float _bulletSpeed;

    private bool _firing;
    private bool _fireSingle;
    private float _lastFireTime;

    [SerializeField]
    private Transform _gunOffset;

    private float _timeBetweenShots;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _bulletSpeed = _gameController.playerBulletSpeed;
        _timeBetweenShots = _gameController.playerTimeBetweenShots;
    }
    // Update is called once per frame
    void Update()
    {
        if (_firing || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire > _timeBetweenShots)
            {
                FireBullet();
                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }
    private void FireBullet()
    {

        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D bullet_rigidbody = bullet.GetComponent<Rigidbody2D>();
        bullet_rigidbody.velocity = _bulletSpeed * transform.right;
    }

    private void OnFire(InputValue inputValue)
    {
        _firing = inputValue.isPressed;
        if (inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
