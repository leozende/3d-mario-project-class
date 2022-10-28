using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Goomba : MonoBehaviour
{
    [Header("Move")]

    [Tooltip ("Player variable speed")]
    [SerializeField] private float m_SpeedMove = 5.0f;
    [Tooltip ("Player speed rotation")]
    [SerializeField] private float m_SpeedRotation = 15f;
    [SerializeField] private float m_ImpulseForce = 10.0f;
    [SerializeField] private int m_Score = 100;

    private bool m_Died = false;

    /// <summary>
    /// Player Movement
    /// </summary>
    private Vector3 m_Movement = Vector3.left;
    /// <summary>
    /// Player Rigidy Body
    /// </summary>
    private Rigidbody m_Body;
    //private Collider m_Collider;
    void Start()
    {
        m_Body = GetComponent<Rigidbody>();
        // [64] [32] [16] [8] [4] [2] [1]
        // [1]  [0]  [1]  [1] [0] [0] [0]
        m_Body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //m_Collider = GetComponent<Collider>();
    }

    void FixedUpdate() 
    {
        Move();  
        Rotate();
    }
    private void Move()
    {
        m_Body.MovePosition(m_Body.position + m_Movement * m_SpeedMove * Time.fixedDeltaTime);
        
    }
    private void Rotate()
    {
        if(m_Movement.sqrMagnitude > 0.001f)
        {
            var forwardRotation = Quaternion.Euler(0,-90, 0) * Quaternion.LookRotation(m_Movement);
            m_Body.MoveRotation(Quaternion.Slerp(m_Body.rotation, forwardRotation, m_SpeedRotation * Time.fixedDeltaTime));
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(m_Died) return;

        if(other.gameObject.CompareTag("Ground")) return;

        if(other.gameObject.CompareTag("Player"))
            Destroy(other.gameObject); // Chamar um método TakeDamage do Player;
        else
            m_Movement.x *= -1;

        // Vector3 impact = other.contacts[0].point;
        // Bounds bound = m_Collider.bounds;
        // if(bound.min.x <= impact.x || impact.x >= bound.max.x)
        // {
            //m_Movement.x *= -1;
        // }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("BigPlayer"))
        {
            m_Died = true;
            Rigidbody body = other.GetComponent<Rigidbody>();
            
            Vector3 velocity = body.velocity;
            velocity.y = 0.0f;

            body.velocity = velocity;

            other.GetComponent<Rigidbody>().AddForce(Vector3.up * m_ImpulseForce, ForceMode.Impulse);
            
            //ScoreUI score = (ScoreUI)GameObject.FindObjectOfType(typeof(ScoreUI));
            ScoreUI score = FindObjectOfType(typeof(ScoreUI)) as ScoreUI;
            score.AddScore(m_Score);

            Destroy(gameObject);

        }
    }
}
