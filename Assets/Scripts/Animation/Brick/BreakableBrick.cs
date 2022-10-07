using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    [Tooltip("Block destruction")]
    [SerializeField] private GameObject m_Explosion;
    private Bounds m_Bounds;
    void Start()
    {
        m_Bounds = GetComponent<Collider>().bounds;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (!other.gameObject.CompareTag("BigPlayer")) return;
        Vector3 impact = other.contacts[0].point;
        if(impact.y <= m_Bounds.min.y)
        {
            Instantiate(m_Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
