using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public RespawnPoint respawnPoint;

    [SerializeField] private GameObject treeVisual;

    [SerializeField] private Animator treeVisualAnimator;

    private Transform treeSpawnPoint;

    private bool onSoil;

    // Start is called before the first frame update
    void Start()
    {
        treeVisualAnimator = treeVisual.GetComponent<Animator>();
        treeSpawnPoint = transform.Find("SpawnPoint");
        respawnPoint = RespawnPoint.instance;
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
                respawnPoint.transform.position = treeSpawnPoint.position;
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
