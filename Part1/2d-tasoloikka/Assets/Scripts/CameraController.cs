using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10.0f;
    public Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, 0);
        targetPosition.z = transform.position.z;
        transform.position += (targetPosition - transform.position) * Time.deltaTime * followSpeed;
    }
}
