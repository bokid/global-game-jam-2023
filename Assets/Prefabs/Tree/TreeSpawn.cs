using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public Transform respawnPoint;

    [SerializeField] private GameObject body;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.W)))
                {
Debug.Log("Button Pressed");                  
                }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (body.activeSelf == true) 
            {
Debug.Log("already active");
                
            }
            else 
            {
            body.SetActive(true);
            animator.SetTrigger("Grow");
            StartCoroutine(OnTreeGrown(collider.gameObject));
            }
        }
    }

    public IEnumerator OnTreeGrown(GameObject player) 
    {
Debug.Log(animator.GetCurrentAnimatorClipInfo(0));
        AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        
        while (clipInfo.clip.length > animator.playbackTime) {
            yield return null;
        }

        player.transform.position = respawnPoint.position;
    }
}
