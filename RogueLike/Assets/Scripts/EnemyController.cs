using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
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
    private float attackCooldown;
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position , transform.position) <= aggroMaxRange && Vector3.Distance(target.position, transform.position) >= aggroMinRange)
        {
            followPlayer();
        }
        
    }
    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
