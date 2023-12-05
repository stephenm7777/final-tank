using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
    private int health; 
    private int healthMax; 
    public event EventHandler OnHealthChange; 
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
}
