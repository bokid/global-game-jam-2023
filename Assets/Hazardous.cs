using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazardous : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player.position = respawnPoint.position;
        }
    }
}
