using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]

    [Tooltip ("Player variable speed")]
    [SerializeField] private float m_SpeedMove;
    [Tooltip ("Player running speed")]
    [SerializeField] private float m_SpeedRun;
    [Tooltip ("Player speed rotation")]
    [SerializeField] private float m_SpeedRotation = 15f;
    /// <summary>
    /// To confirm if the player is running.
    /// </summary>
    private bool m_IsRunning;
    /// <summary>
    /// Player Movement
    /// </summary>
    private Vector3 m_Movement;

    [Header("Ground")]
    [Tooltip ("Minimum distance for the ground")]
    [SerializeField] private float m_GroundDistance = 0.01f;
    [Tooltip ("Layer for the ground")]
    [SerializeField] private LayerMask m_GroundLayer;
    [Tooltip("The feet children")]
    [SerializeField] private Transform m_Feet;
    /// <summary>
    /// To confirm if the player on the ground.
    /// </summary>
    private bool m_IsGrounded;

    [Header("Jump")]
    [Tooltip("The force for the jump")]
    [SerializeField] private float m_JumpForce;
    [Tooltip("Time to verify if the player is holding the buttom")]
    [SerializeField] private float m_JumpTime = 0.33f;
    /// <summary>
    /// To confirm if the player is jumping.
    /// </summary>
    private bool m_IsJumping;
    /// <summary>
    /// Time counter for the jump.
    /// </summary>
    private float m_JumpElapsedTime;

    [Header("Verify")]
    public bool m_BigPlayer;

    /// <summary>
    /// Player Rigidy Body
    /// </summary>
    private Rigidbody m_Body;

    // Start is called before the first frame update
    void Start()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_IsGrounded = Physics.CheckSphere(m_Feet.position, m_GroundDistance, m_GroundLayer, QueryTriggerInteraction.Ignore);
        m_Movement.x = Input.GetAxis("Horizontal");
        m_IsRunning = Input.GetButton("Fire1");

        if(Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_IsJumping = true;
            m_JumpElapsedTime = 0;
        }

    }

    void FixedUpdate() 
    {
        Move();  
        Rotate();
        Jump();  
    }

    private void Move()
    {
        float speed = m_IsRunning ? m_SpeedRun : m_SpeedMove;
        m_Body.MovePosition(m_Body.position + m_Movement * speed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        if(m_Movement.sqrMagnitude > 0.001f)
        {
            var forwardRotation = Quaternion.Euler(0,-90, 0) * Quaternion.LookRotation(m_Movement);
            m_Body.MoveRotation(Quaternion.Slerp(m_Body.rotation, forwardRotation, m_SpeedRotation * Time.fixedDeltaTime));
        }
    }

    private void Jump()
    {
        if(m_IsJumping && m_JumpElapsedTime > (m_JumpTime/3))
        {
            if(!Input.GetButton("Jump"))
            {
                m_IsJumping = false;
            }
        }
        if(m_IsJumping && m_JumpElapsedTime < m_JumpTime)
        {
            m_JumpElapsedTime += Time.fixedDeltaTime;
            float proportionCompleted = Mathf.Clamp01(m_JumpElapsedTime / m_JumpTime);
            float currentForce = Mathf.Lerp(m_JumpForce, 0.0f, proportionCompleted);

            m_Body.AddForce(Vector3.up * currentForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
            m_IsJumping = false;
    }
    
}
