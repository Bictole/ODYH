using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Define which Target to follow
    public Transform target;
    
    
    // Speed of the camera
    public float moveSpeed;
    
    // Test if the cameraExists
    private static bool cameraExists;

    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Position reset
    public VectorValue camMin;
    public VectorValue camMax;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;

        maxPosition = camMax.initialValue;
        minPosition = camMin.initialValue;
        transform.position = target.transform.position + offset;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != target.position)
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        float targetClampX = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
        float targetClampY = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);
         
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
        Vector3 destination = transform.position + delta;
         
        Vector3 currentplace = new Vector3(targetClampX, targetClampY, transform.position.z);
        Vector3 cameraMove = Vector3.Lerp(transform.position, currentplace,moveSpeed);
        transform.position = cameraMove;
    }
}
