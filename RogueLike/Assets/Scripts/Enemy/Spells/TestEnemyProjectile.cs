using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float _damage;
    public float _duration;
    public float _flightDuration;

    private static float _timer;

    void FixedUpdate()
    {
        _timer = Time.fixedDeltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy")
        {
            //This if statement is called to so the 
            if(_timer <= _flightDuration && collision.tag == "Player")
            {
                PlayerStatistics._playerStatistics.DealDamage(_damage);
            }
            else
            {
                //This is where I would instantiate the game object of the projectile landing or
                //exploding before destroying the original projectile game object
                _timer = 0;
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
