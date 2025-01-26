using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [SerializeField] int maxHP;
    int currentHP;
    public Image healthBarFill;
    public int HP { get => currentHP; }

    public UnityAction OnTakeDamage;
    public UnityAction OnDead;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }
    public bool IsAlive() => currentHP > 0;

    public void TakeDamage(int damage)
    {
        if (!BattleModeManager.Instance.IsInBattle)
            return;

        if (!IsAlive())
            return;

        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDead?.Invoke();
        }
        else
        {
            OnTakeDamage?.Invoke();
        }
        UpdateHealthBar();
    }

    public void Heal(int heal)
    {
        if (!IsAlive())
            return;

        currentHP += heal;

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthBar();
    }

    public void Kill()
    {
        if (!IsAlive())
            return;

        currentHP = 0;
        OnDead?.Invoke();
    }
    public void Reset()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHP / (float)maxHP; // Update the fill based on health percentage
        }
    }

}