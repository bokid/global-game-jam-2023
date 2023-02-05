using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public Transform respawnPoint;

    [SerializeField] private GameObject treeVisual;

    [SerializeField] private Animator treeVisualAnimator;

    private bool onSoil;

    // Start is called before the first frame update
    void Start()
    {
        treeVisualAnimator = treeVisual.GetComponent<Animator>();
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
                treeVisualAnimator.SetTrigger("Grow");
                respawnPoint.position = transform.position + new Vector3(0, 18, 0);
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
