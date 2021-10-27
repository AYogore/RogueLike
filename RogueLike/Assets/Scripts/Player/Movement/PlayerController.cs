using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider _staminaSlider;
    //Movement Parameters for the character

    private float movement;
    public float maxMovement;

    public float acceleration;
    public float deceleration;
    public float accelerationRate;

    public float movementRateChange;
    public float sprintMultiplier;

    public float stamina;
    public float maxStamina;

    public float staminaDrainRate;
    public float staminaRegenerationRate;
    
    //Sprint Parameters for the characters
    private float StaminaRegenTimer;
    private const float StaminaTimeToRegen = 3.0f;
    

    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;
    private bool isMoving = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
        UpdateStaminaBar();

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
            //Debug.Log("Speed = " + movement);
        }
        else
        {
            if (isMoving == false && movement > deceleration * Time.fixedDeltaTime)
            {
                movement = movement - deceleration * accelerationRate * Time.fixedDeltaTime;
                //Debug.Log("Speed = " + movement);
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

    /// <summary>
    /// This makes it so the character can sprint if they
    /// push down the shift key while moving around, however
    /// it will consume stamina.Stamina will regenerate if
    /// the character does not sprint.
    /// Will possibly add an option for auto sprinting similar
    /// to other roguelikes such as Diablo II.
    /// This will be moved to the PlayerStatistics eventually
    /// </summary>
    private void StaminaBar()
    {
        //Checks to see if the shift key is pressed
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting && isMoving)
        {
            stamina = Mathf.Clamp(stamina - (staminaDrainRate * Time.deltaTime), 0.0f, maxStamina);
            //Debug.Log("Stamina = " + stamina);
            UpdateStaminaBar();
            StaminaRegenTimer = 0.0f;
        }
        else if (stamina < maxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
            {
                stamina = stamina + (staminaRegenerationRate * Time.deltaTime);
                UpdateStaminaBar();
            }
            else
            {
                StaminaRegenTimer += Time.deltaTime;
            }
        }
    }

    private float GetStaminaPercentage()
    {
        return stamina / maxStamina;
    }

    private void UpdateStaminaBar()
    {
        _staminaSlider.value = GetStaminaPercentage();
    }

}
