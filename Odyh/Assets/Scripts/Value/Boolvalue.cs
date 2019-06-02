using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Boolvalue : ScriptableObject, ISerializationCallbackReceiver
{
    //Value running in game
    public bool initialvalue;
    
    [HideInInspector]
    public bool Runtimevalue;

    public void OnAfterDeserialize()
    {
        Runtimevalue = initialvalue;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
