using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem: MonoBehaviour
{
    private int health; 
    private int healthMax; 
    public event EventHandler OnHealthChange; 
    public int attack; 
    public HealthSystem(int healthMax){
        this.healthMax = healthMax; 
        health = healthMax;
    }
    public int GetHealth(){
        return health; 
    }
    public float GetHealthPercentage(){
        return (float)health / healthMax; 
    }
    public void Damage(int damageAmount){
        health -= damageAmount;
        if(health < 0 ) health = 0; 
        if(OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }
    public void DealDamage(GameObject target){
        var atm = target.GetComponent<HealthSystem>();
        if(atm != null){
            atm.Damage(attack);
        }
    }
}
