using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeUI : MonoBehaviour
{

    [Tooltip("Player Life")]
    [SerializeField] private int m_Life = 3;
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

    void LifeUp()
    {
        m_Life++;
        UpdateUI();
    }

    void LifeDown()
    {
        m_Life--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = $"<size=32>x</size>{m_Life.ToString(m_Mask)}";
    }

}
