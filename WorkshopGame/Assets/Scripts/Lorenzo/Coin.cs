using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event EventHandler Triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() == null)
            return;
        Triggered?.Invoke(this, EventArgs.Empty);                
    }
}
