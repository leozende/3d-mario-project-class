using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseBox : MonoBehaviour
{
    /// <summary>
    /// Border of the collider
    /// </summary>
    private Bounds m_Bounds;
    /// <summary>
    /// Control animation
    /// </summary>
    private Animator m_Anim;
    void Start()
    {
        m_Bounds = GetComponent<Collider>().bounds;
        m_Anim = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        var impact = other.contacts[0].point;
        var isBelowBrick = impact.y <= m_Bounds.min.y;
        if(!isBelowBrick) return;

        if (other.gameObject.GetComponent<PlayerMovement>().m_BigPlayer)
            m_Anim.SetTrigger("Break");
        else
            m_Anim.SetTrigger("Bounce");
    }

    void f_Destroy()
    {
        Destroy(gameObject);
    }
}
