using UnityEngine;

public class Brick : MonoBehaviour
{
    [Tooltip("Time for the block to go up and down")]
    [SerializeField] private float m_BounceTime = 0.3f;
    [Tooltip("Distance for the block to move")]
    [SerializeField] private float m_BounceDistance = 0.3f;
    [Tooltip("Animation Curve to move the block")]
    [SerializeField] private AnimationCurve m_Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0)); // Creating key frames
    // for the animation.

    /// <summary>
    /// Movement in vector 3
    /// </summary>
    private Vector3 m_Source;
    /// <summary>
    /// if the block can bounce.
    /// </summary>
    private bool m_CanBounce;
    /// <summary>
    /// Time that the game start.
    /// </summary>
    private float m_StartTime;
    /// <summary>
    /// Collision border;
    /// </summary>
    private Bounds m_Bounds;
    void Start()
    {
        m_Source = transform.position;
        m_Bounds = GetComponent<Collider>().bounds;
    }

    void Update()
    {
        if(!m_CanBounce) return;

        var time = (Time.time - m_StartTime) / m_BounceTime; 
        transform.position = m_Source + Vector3.up * m_BounceDistance * m_Curve.Evaluate(time);
        m_CanBounce = time < 1.0f;
        //if(!m_CanBounce) this.enabled = false;
    }

    private void OnCollisionEnter(Collision other) 
    {
        Vector3 impact = other.contacts[0].point;
        if(impact.y <= m_Bounds.min.y)
        {
            m_StartTime = Time.time;
            m_CanBounce = true;
        }
    }
}
