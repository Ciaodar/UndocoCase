using Fusion;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;
    private Animator animator;
    
    public CameraMovement PlayerCamera;
    private bool isGrounded;
    public float groundDistance = 0.15f;
    public LayerMask groundMask;
    Vector3 velocity;
    public float turnSmoothVelocity = 0.125f;
    public float turnSmoothTime = 0.03f;
    public float speed = 6f;
    
    public float gravity = -9.81f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
    }

    public override void Spawned()
    {
        if (HasStateAuthority == false)
            return;
        
        PlayerCamera = FindObjectOfType<CameraMovement>();
        Debug.Assert(PlayerCamera != null, nameof(PlayerCamera) + " != null");
        PlayerCamera.SetTarget(transform);
    }
    

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
            return;
        
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        velocity.y += gravity * Runner.DeltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("Speed", direction.magnitude);

        
        
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = Quaternion.Euler(0f, PlayerCamera.transform.eulerAngles.y, 0f) * direction;

            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 move = moveDir.normalized * speed * Runner.DeltaTime;
            _controller.Move(move);
        }
        
        _controller.Move(velocity * Runner.DeltaTime);
        
        
    }
}