using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Going to add this once I introduce party members/summons
    //private GameObject[] targets;
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float aggroMaxRange;
    [SerializeField]
    private float aggroMinRange;

    private float deaggroTimer = 0f;
    private const float timeToDeAggro = 5f;

    private float attackCooldown;

    public Vector3 start;
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        start = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position , transform.position) <= aggroMaxRange && Vector3.Distance(target.position, transform.position) >= aggroMinRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (timeToDeAggro <= deaggroTimer)
            { 
                transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
            }
            else
            {
                deaggroTimer += Time.deltaTime;
            }
        }
        
    }
}
