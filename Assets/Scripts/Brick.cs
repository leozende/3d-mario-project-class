using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [Tooltip("Time for the block to go up and down")]
    [SerializeField] private float m_BounceTime;
    [Tooltip("Distance for the block to move")]
    [SerializeField] private float m_BounceDistance;
    [Tooltip("Animation Curve to move the block")]
    [SerializeField] AnimationCurve m_Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0)); // Creating key frames
    // for the animation.

    private Vector3 m_Source;
    /// <summary>
    /// if the block can bounce.
    /// </summary>
    private bool m_CanBounce;
    /// <summary>
    /// Time that the game start.
    /// </summary>
    private float m_StartTime;
    void Start()
    {
        m_Source = transform.position;
    }

    void Update()
    {
        if(!m_CanBounce) return;

        var time = (Time.time - m_StartTime) / m_BounceTime; 
        transform.position = m_Source + Vector3.up * m_BounceDistance * m_Curve.Evaluate(time);
    }
}
