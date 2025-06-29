using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus
{
    public float maxHealth;
    public float hitpoints;
    public float weight;
    public float knockBack;
    public float damage;

    public EnemyStatus(float maxHealth, float hitpoints, float weight, float knockBack, float damage)
    {
        this.maxHealth = maxHealth;
        this.hitpoints = hitpoints;
        this.weight = weight;
        this.knockBack = knockBack;
        this.damage = damage;
    }

    public void HitPlayer(PlayerStatus player)
    {
        player.TakeDamage(this.damage);
    }
}
