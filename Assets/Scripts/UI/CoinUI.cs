using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{

    [Tooltip("Value")]
    [SerializeField] private int m_Value;
    [Tooltip("Mask that defines how many zeros will have")]
    [SerializeField] private string m_Mask = "00";
    
    /// <summary>
    /// Text component
    /// </summary>
    private TextMeshProUGUI m_Text;

    private void Start() 
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    void AddCoins(int value)
    {
        m_Value += value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = m_Value.ToString(m_Mask);
    }

}
