using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform target;
    public float parallax = 0.9f;
    public Vector2 offset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, 0) * parallax;
        targetPosition += new Vector3(offset.x, offset.y, transform.position.z);
        transform.position = targetPosition;    
    }
}
