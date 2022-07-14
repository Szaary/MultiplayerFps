using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthBar;
       
    private Damageable damager;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Initialize(UserInterfaceController userInterfaceController)
    {
        damager= userInterfaceController.GetComponent<Damageable>();
        damager.OnHealthChanged += SetHealthBar;
    }

    private void SetHealthBar(float damage, float currentHealth)
    {
        healthBar.text = currentHealth.ToString();
    }
    
    private void OnDisable()
    {
        damager.OnHealthChanged -= SetHealthBar;
    }
}
