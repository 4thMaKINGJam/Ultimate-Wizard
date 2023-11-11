using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : MonoBehaviour
{
    public Image hpBarImage; 
    public float maxHP; // 총 HP
    private float currentHP; // 현재 HP

    private Monster monster;

    void Start()
    {
        hpBarImage = gameObject.GetComponent<Image>();
        monster = GameObject.FindObjectOfType<Monster>();
        maxHP = monster.maxHealth;
        currentHP = maxHP;
    }

    void Update()
    {
        currentHP = monster.health;
        UpdateHPBar(); // HP 바 업데이트
    }

    // HP 바 업데이트
    private void UpdateHPBar()
    {
        if (hpBarImage != null)
        {
            hpBarImage.fillAmount = currentHP / maxHP;
        }
    }
}
