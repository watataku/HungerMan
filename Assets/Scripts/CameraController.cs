using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var angle = player.rotation.y * Mathf.PI / 180;
        transform.position = player.position + new Vector3(-4*Mathf.Sin(angle), 2, -4*Mathf.Cos(angle));
        transform.Rotate(new Vector3(0, player.rotation.y, 0));
    }
}
