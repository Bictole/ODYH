using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Define which Target to follow
    public Character target;
    
    
    // Speed of the camera
    public float moveSpeed;
    
    // Test if the cameraExists
    private static bool cameraExists;

    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        transform.position = target.transform.position + offset;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != target.transform.position)
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        Vector3 targetPosition;
        if (target.nextdirection != Vector3.zero)
        {
            targetPosition = target.transform.position + target.nextdirection + offset;
        }
        else
        {
            targetPosition = target.transform.position + offset;
        }
        
        transform.position = Vector3.Lerp(target.transform.position, targetPosition, moveSpeed);
        
    }
}
