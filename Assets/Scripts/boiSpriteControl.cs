using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boiSpriteControl : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y));
    }
}
