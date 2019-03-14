using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Define which Target to follow
    public GameObject followTarget;
    
    // Position of the target
    private Vector3 targetposition;
    
    // Speed of the camera
    public float moveSpeed;
    
    // Test if the cameraExists
    private static bool cameraExists    ;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        var target = followTarget.transform.position;
        var position = transform.position;
        targetposition = new Vector3(target.x, target.y, position.z);
        
        // Allow to switch between position "a" to position "b" with a delay
        position = Vector3.Lerp(position, targetposition, moveSpeed * Time.deltaTime);
        transform.position = position;
    }
}
