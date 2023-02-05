using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateFaster : MonoBehaviour
{

    public float SPEEN_SPEED = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, 0f, SPEEN_SPEED, Space.World);
    }
}
