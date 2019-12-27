using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    [SerializeField] private float angularVelocity;
    
    private void Update()
    {
        transform.Rotate(new Vector3(0, angularVelocity, 0));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
