using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthBar healthBar; 

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        Debug.Log("Health: " + healthSystem.GetHealth());
    }

    
}
