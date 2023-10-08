using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    
    void Start()
    {
        totalHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }
    
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }
}
