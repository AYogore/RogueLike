using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Going to add this once I introduce party members/summons
    //private GameObject[] targets;
    private Transform target;
    private enum LootTable { Gold, Item}

    LootTable lootTable;

    public GameObject lootDrop;

    public string _nameOfEnemy;
    public float _health;
    public float _maxHealth;
    public float _stamina;

    //These are the different statuses for the enemy when they
    //are instanciated.
    [SerializeField]
    private float speed;
    
    [SerializeField]
    public float experienceValue;

    //These are the ranges for when the enemy notices the player.
    [SerializeField]
    private float aggroMaxRange;
    [SerializeField]
    private float aggroMinRange;

    //These are the parameters for the weapons that the enemy has.
    //These values are temporary for now, they will change once
    //there are items implemented into the game.
    //[SerializeField]
    //private float weaponMinRange = 3;
    //private float weaponMaxRange = 10;

    //The timer for when the enemy no longer is chasing the player
    //After a while they return back to their starting position
    private float deaggroTimer = 0f;
    private const float timeToDeAggro = 5f;

    //Will add once attacks are implemented
    //private float attackCooldown;

    //The variable to store the starting position of the enemy 
    //when they spawn. This will be used for when they return
    //after chasing the player till they are no longer within
    //the aggro range.
    private Vector3 start;
    void Start()
    {
        _health = _maxHealth;
        //This function scans the environment for an object that
        //has the 'PlayerController' script, this marks them as
        //an enemy and enables them to chase once they go within
        //a certain range.
        target = FindObjectOfType<PlayerController>().transform;
        //This function stores the starting position to 'start',
        //this way if I were to create different enemies with the
        //code attached to them they would all have different
        //starting positions
        start = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        //Since the enemy technically sees the player as soon as they get instanced
        //there needs to be a range at which the enemy will give chase. Otherwise the
        //player will be chased right as they enter the game without getting a feel for
        //movement, and that's no fun for anyone.
        //This if statement checks if the player is within range of the enemy and will give
        //chase once they find them. It refreshes for every time update is called.
        if(Vector3.Distance(target.position , transform.position) <= aggroMaxRange &&
            Vector3.Distance(target.position, transform.position) >= aggroMinRange)
        {
            //This moves the enemy towards the player at a constant speed relative to the
            //time through update. Speed is variable and can be changed depending on the enemy
            //in the Unity editor.
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            //This makes it so the deaggroTimer becomes zero once the enemy gives chase to the
            //player. Since the timer constantly is running while the enemy is standing still.
            
            deaggroTimer = 0;
        }
        else
        {
            DeAggro(); 
        }
    }
    private void DeAggro()
    {
        //If the player is no longer within range of the enemy, they will stop giving
        //chase and remain there for a certain amount of time. Once it has reached a
        //certain point they will return to their starting position.
        //This checks if the deaggroTimer has reached/exceeded the timer
        if (timeToDeAggro <= deaggroTimer)
        {
            //Once the timer threshold has been met the enemy will return back to their starting position
            //at half of their normal speed. This gives them more time for them to be an obstacle in the
            //player's way.
            transform.position = Vector3.MoveTowards(transform.position, start, speed/2 * Time.deltaTime);
        }
        else
        {
            //If the timer is not at the threshold, then the timer will simply increment based on the time
            //that has passed.
            deaggroTimer += Time.deltaTime;
        }
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        CheckDeath();
    }

    private void CheckOverHeal()
    {
        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void CheckDeath()
    {
        if(_health <= 0)
        {
            Debug.Log($"{_nameOfEnemy} has died");
            Destroy(gameObject);
        }
    }
}
