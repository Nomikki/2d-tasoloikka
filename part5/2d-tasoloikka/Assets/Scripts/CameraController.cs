using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10.0f;
    public Vector2 offset;

    public float limitLeft = 0;
    public float limitRight = 0;
    public float limitTop = 0;
    public float limitBottom = 0;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        //Debug.Log("transform position: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, 0);
        targetPosition.z = transform.position.z;

        targetPosition.x = Mathf.Clamp(targetPosition.x, limitLeft, limitRight);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limitBottom, limitTop);

        transform.position += (targetPosition - transform.position) * Time.deltaTime * followSpeed;
        //Debug.Log("transform position: " + transform.position);
    }
}
