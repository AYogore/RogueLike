using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject _enemyProjectile;
    private GameObject _player;
    public float _minDamage;
    public float _maxDamage;
    public float _projectileSpeed;
    public float _cooldown;

    //Will add these in the future for differing ranged enemies
    //as well as enemy casters
    private float _minRange;
    private float _maxRange;
    private int _ammoCount;

    void Start()
    {
        StartCoroutine(ShootPlayer());
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(_cooldown);
        if (_player != null)
        {
            GameObject _testSpell = Instantiate(_enemyProjectile, transform.position, Quaternion.identity);
            Vector2 _myPos = transform.position;
            Vector2 _targetPosition = _player.transform.position;
            Vector2 _direction = (_targetPosition - _myPos).normalized;
            _testSpell.GetComponent<Rigidbody2D>().velocity = _direction * _projectileSpeed;
            _testSpell.GetComponent<TestEnemyProjectile>()._damage = Random.Range(_minDamage, _maxDamage);

            //This creates a loop so they dont only shoot their projectiles once, it can be further detailed
            StartCoroutine(ShootPlayer());
        }
    }
}

