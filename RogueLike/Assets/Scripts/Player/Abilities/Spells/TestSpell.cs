using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject _projectile;
    public float _minDamage;
    public float _maxDamage;
    public float _projectileSpeed;
    public float _lifetime;
    private static float _timer;

     void Update()
    {
        _timer = Time.deltaTime;
        if (Input.GetMouseButtonDown(1))
        {
            //Turns wherever the mouse is pointing into a coordinate point
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _playerPos = transform.position;
            Vector2 _direction = (_mousePos - _playerPos).normalized;

            GameObject spell = Instantiate(_projectile, _playerPos, Quaternion.identity);
            spell.GetComponent<Rigidbody2D>().velocity = _direction * _projectileSpeed;
            spell.GetComponent<TestProjectile>()._damage = Random.Range(_minDamage, _maxDamage);
        }
        if(_timer >= _lifetime)
        {
            _timer = 0;
            Destroy(gameObject);
        }
    }

}
