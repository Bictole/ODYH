using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{

    public float moveSpeed;

    public int damageNumber;
    public Text displaynumber;
        
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displaynumber.text = "" + damageNumber;
        Vector3 position = transform.position;
        transform.position = new Vector3(position.x, position.y + (moveSpeed * Time.deltaTime),
            position.z);
    }
}
