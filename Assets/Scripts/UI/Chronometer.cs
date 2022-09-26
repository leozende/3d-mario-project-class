using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chronometer : MonoBehaviour
{
    [Tooltip("Max time for the game")]
    [SerializeField] private float m_MaxTime = 300.0f;
    [Tooltip("Variable that shows how many times has passed")]
    [SerializeField] private float m_ElapsedTime = 0.0f;
    [Tooltip("If the player ends the game")]
    [SerializeField] private bool m_FinishLevel = false;
    [Tooltip("Text mask to know how many will appear (Quantas casas serão)")]
    [SerializeField] private string m_Mask = "000";
    
    /// <summary>
    /// Text component
    /// </summary>
    private TextMeshProUGUI m_Text;
    
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    void Update()
    {
        if (m_FinishLevel)
            return;
        
        m_ElapsedTime += Time.deltaTime;
        UpdateUI();

    }

    private void UpdateUI()
    {
        m_Text.text = Mathf.Ceil(m_MaxTime - m_ElapsedTime).ToString(m_Mask);   
    }
}
