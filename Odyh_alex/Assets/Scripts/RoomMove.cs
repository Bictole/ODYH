using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{

    public Vector2 camerachangemin;
    public Vector2 camerachangemax;

    public Vector3 playerChange;

    private CameraController camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camera.minPosition = camerachangemin;
            camera.maxPosition = camerachangemax;
            other.transform.position += playerChange;
        }
    }
}
