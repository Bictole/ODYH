using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//interface des objets utilisable
public interface Utilisable
{
    Sprite Sprite { get; }

    void Use();
}

//objets cliquable
public interface Cliquable
{
    Image Image { get; set; }
    
    int Itemscount { get; }

    Text StackText { get; }
}
