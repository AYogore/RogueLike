using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    //Movement Parameters for the character
    private float movement = 0f;
    public float maxMovement = 5f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float accelerationRate = 3f;
    public float movementRateChange = 3f;
    public float sprintMultiplier = 1.5f;

    //Basic Stats for the character
    public float health = 100f;
    public float maxHealth = 100f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaDrainRate = 10f;
    public float staminaRegenerationRate = 10f;

    /* Other Statistics like Strength, Constitution, etc will go here once implemented
     * Could also make the leveling scale off of experience that you get from using weapons
     * and skills similar to Elder Scrolls
     * Levelling could also be a mixture of both, you gain experience in order to increase
     * your base statistics while using different weapons and spells to increase the skill 
     * for the respective type. Have to discuss with others for feedback on the idea
     */
    
    //Sprint Parameters for the characters
    private float StaminaRegenTimer = 0.0f;
    private const float StaminaTimeToRegen = 3.0f;
    

    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;
    private bool isMoving = false;

    void Start()
   {
       rb2d = GetComponent<Rigidbody2D>();
   }

    // Update is called once per frame
    void Update()
    {
        StaminaBar();
        Movement();
    }

    private void FixedUpdate()
    {
        //Need to clean this up later, looks ugly as sin
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
        if (isMoving == true && movement < maxMovement)
        {
            movement = movement + acceleration * accelerationRate * Time.fixedDeltaTime;
            Debug.Log("Speed = " + movement);
        }
        else
        {
            if (isMoving == false && movement > deceleration * Time.fixedDeltaTime)
            {
                movement = movement - deceleration * accelerationRate * Time.fixedDeltaTime;
                Debug.Log("Speed = " + movement);
            }
        }
    }

    private void Movement()
    {
        //Establishes the movement commands
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //Adds a sprint functionality
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && isMoving)
        {
            moveVelocity = moveInput.normalized * movement * sprintMultiplier;
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

    /* This makes it so the character can sprint if they
       push down the shift key while moving around, however
       it will consume stamina. Stamina will regenerate if
       the character does not sprint.
       Will possibly add an option for auto sprinting similar
       to other roguelikes such as Diablo II.
    */
    private void StaminaBar()
    {
        //Checks to see if the shift key is pressed
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting && isMoving)
        {
            stamina = Mathf.Clamp(stamina - (staminaDrainRate * Time.deltaTime), 0.0f, maxStamina);
            Debug.Log("Stamina = " + stamina);

            StaminaRegenTimer = 0.0f;
        }
        else if (stamina < maxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
            {
                stamina = stamina + (staminaRegenerationRate * Time.deltaTime);
                Debug.Log("Stamina = " + stamina);
            }
            else
            {
                StaminaRegenTimer += Time.deltaTime;
            }
        }
    }

}
