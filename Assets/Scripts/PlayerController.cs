using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float slideSpeed = 8.5f;
    Animator animator;
    Vector2 moveInput;


    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isSliding = false;

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
        
    public float CurrentMoveSpeed{ get
        {
            if (IsMoving)
            {

                if (IsSliding)
                {
                    return slideSpeed;
                }
                else
                {
                    return walkSpeed;
                } 
            }else
            {
                return 0;
            }
        }
    }
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    
       

    public bool IsSliding 
    { 
        get 
        { 
            return _isSliding;
        } 
    set
        {
            _isSliding = value;
            animator.SetBool("IsSliding", value);
        }
    }

    
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsSliding = true;
        }
        else if (context.canceled)
        {
            IsSliding = false;
        }
    }
}
