using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem: MonoBehaviour
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
        /*if(health == 0){
            Add here a scene that makes them say "Game Over" and transfer them back into the lobby
        }*/
    }
}
