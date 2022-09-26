using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    [Tooltip("Player Score")]
    [SerializeField] private int m_Score;
    [Tooltip("Mask that defines how many zeros will have")]
    [SerializeField] private string m_Mask = "000000000";
    
    /// <summary>
    /// Text component
    /// </summary>
    private TextMeshProUGUI m_Text;

    private void Start() 
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    void AddScore(int score)
    {
        m_Score += score;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = m_Score.ToString(m_Mask);
    }

}
