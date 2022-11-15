using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private int maxHealth = 100;
    int health = maxHealth;
    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
