using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public Transform respawnPoint;

    [SerializeField] private GameObject body;

    [SerializeField] private Animator animator;

    private bool onSoil;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>(true);
        respawnPoint = GameObject.Find("RespawnPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (onSoil)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Debug.Log("Grow to the heavens!");
                body.SetActive(true);
                animator.SetTrigger("Grow");
                respawnPoint.position = transform.position + new Vector3(0, 6, 0);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player"))
        {
Debug.Log("On Soil");
            onSoil = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player")))
        {
Debug.Log("Off Soil");
        onSoil = false;
        }
    }
}
