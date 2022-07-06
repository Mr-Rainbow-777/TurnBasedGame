using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamaged;

    [SerializeField] private int health = 100;
    private const int HealthMax = 100;
    public void Damage(int damageAmount)
    {
        health-=damageAmount;

        if(health < 0)
        {
            Die();
        }

        OnDamaged?.Invoke(this,EventArgs.Empty);
    }

    public void Die()
    {
        OnDead?.Invoke(this,EventArgs.Empty);
       // Destroy(gameObject);
    }

    public float GetHealthNormal()
    {
        return (float)health / HealthMax;
    }

}
