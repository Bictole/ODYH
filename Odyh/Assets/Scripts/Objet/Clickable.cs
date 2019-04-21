using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Clickable
{
    Sprite Sprite { get; }

    void Use();
}

