using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using NUnit.Framework;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction jump;
    private InputAction move;
    private InputAction sprint;
    private Vector3 moveAmt;

    [Header("移動調整の数値")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpAmt = 5f;
    [SerializeField] private float multiplier = 2f;
    
    public float stamina = 100f;
    private float staminaCost = 30f;
    private bool isGrounded;
    public bool isSprinting;
    private Rigidbody rb;
    private Camera cam;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
        jump.performed += PlayerJump;
        sprint.performed += Sprint;
        sprint.canceled += Sprint;
        
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
        jump.performed -= PlayerJump;
        sprint.performed -= Sprint;
        sprint.canceled -= Sprint;
    }

    private void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        sprint = InputSystem.actions.FindAction("Sprint");

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        
    }

    private void FixedUpdate()
    {
        float sprintMult = isSprinting ? multiplier: 1f;
        moveAmt = move.ReadValue<Vector2>(); 
        rb.position +=  ((moveAmt.y * cam.transform.forward) + (moveAmt.x * cam.transform.right)).normalized * moveSpeed * sprintMult * Time.deltaTime;
        StaminaRecovery();
    }
    private void PlayerJump(InputAction.CallbackContext context)
    {
        if(isGrounded){rb.AddForceAtPosition(Vector3.up * jumpAmt, rb.position, ForceMode.Impulse);} // プレーヤーは地面にいる場合に、ジャンプを発動できる
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            if(contact.normal.y > 0.5){isGrounded = true;}
        }
    }
    private void OnCollisionExit(Collision collision){isGrounded = false;}
    private void Sprint(InputAction.CallbackContext ctx)
    {
        isSprinting = ctx.performed;
        StaminaUse();
    }

    private void StaminaUse()
    {
        if (isSprinting && stamina > 0)
        {
            stamina -= staminaCost;
        }
        if(stamina < 0){stamina = 0;}

    }

    private void StaminaRecovery()
    {
        if (!isSprinting && stamina < 100)
        {
            stamina += 10 * Time.deltaTime;
        }
        if(stamina > 100){stamina = 100;}
    }
}
