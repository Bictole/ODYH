using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followTarget;
    private Vector3 targetposition;
    public float moveSpeed;

    private static bool camera_exists;
    
    // Start is called before the first frame update
    void Start()
    {
        // Permet de ne pas détruire la caméra quand on charge une scene -> sans duplication
        if (!camera_exists)
        {
            camera_exists = true;
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
        position = Vector3.Lerp(position, targetposition, moveSpeed * Time.deltaTime);
        transform.position = position;
    }
}
