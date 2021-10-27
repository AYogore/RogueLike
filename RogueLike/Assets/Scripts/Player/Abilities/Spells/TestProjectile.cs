using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player" && collision.tag != "Gold" && collision.tag != "Item")
        {
            if(collision.GetComponent<EnemyRecieveDamage>() != null)
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(_damage);
            }
            Destroy(gameObject);
        }


    }
}
