using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    //Movement Parameters for the character
    public float movement = 0f;
    public float maxMovement = 5f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float rate = 3f;
    public float sprintMultiplier = 1.5f;

    //Basic Stats for the character
    public float health = 100f;
    public float stamina = 100f;
    public float staminaDrainRate = 10f;
    public float staminaRegenerationRate = 10f;

    /* Other Statistics like Strength, Constitution, etc will go here once implemented
     * Could also make the leveling scale off of experience that you get from using weapons
     * and skills similar to Elder Scrolls
     * Levelling could also be a mixture of both, you gain experience in order to increase
     * your base statistics while using different weapons and spells to increase the skill 
     * for the respective type. Have to discuss with others for feedback on the idea
     */

    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;
    private bool isMoving = false;
    private bool isSprinting = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Stamina Regeneration
        while (stamina < 100 && isSprinting == false)
        {
            stamina = stamina + staminaRegenerationRate * Time.deltaTime;
            Debug.Log("Stamina = " + stamina);
        }
        Movement();
    }

    private void FixedUpdate()
    {
        
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
        if (isMoving == true && movement < maxMovement)
        {
            movement = movement + acceleration * rate * Time.fixedDeltaTime;
            Debug.Log("Speed = " + movement);
        }
        else
        {
            if (isMoving == true && movement > deceleration * Time.fixedDeltaTime)
            {
                movement = movement - deceleration * rate * Time.fixedDeltaTime;
                Debug.Log("Speed = " + movement);
            }
            else
            {
                movement = 0;
                isMoving = false;
            }
        }
    }

    private void Movement()
    {
        //Establishes the movement commands
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        

        //Adds a sprint functionality
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 0)
        {
            moveVelocity = moveInput.normalized * movement * sprintMultiplier;
            stamina = stamina - staminaDrainRate * Time.deltaTime;
            isSprinting = true;
            Debug.Log("Stamina = " + stamina);
        }
        else
        {
            moveVelocity = moveInput.normalized * movement; 
        }

        //Currently the only way I can get movement to work with acceleration, will definately change later
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    /*private void Attack()
    {

    }
    */

}
