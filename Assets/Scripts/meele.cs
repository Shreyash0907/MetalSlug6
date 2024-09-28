using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meele : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Debug.Log("Player hit by melee attack!");
            
        }
        if(other.CompareTag("Soldier")){
            // Destroy("");
        }
    }
}
