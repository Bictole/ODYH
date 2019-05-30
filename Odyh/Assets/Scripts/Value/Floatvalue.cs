using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Floatvalue : ScriptableObject, ISerializationCallbackReceiver
{
    //Value running in game
    public float initialvalue;
    
    [HideInInspector]
    public float Runtimevalue;

    public void OnAfterDeserialize()
    {
        Runtimevalue = initialvalue;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
