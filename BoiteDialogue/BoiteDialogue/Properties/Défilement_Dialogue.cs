
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] phrases;
    private int index;
    public float vitesse_écriture;
    public GameObject bouton_continuer;

    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if (textDisplay.text == phrases[index])
        {
            bouton_continuer.SetActive(true);
        }   
    }
    IEnumerator Type()
    {
        foreach(char lettre in phrases[index].ToCharArray())
        {
            textDisplay.text += lettre;
            yield return new WaitForSeconds(vitesse_écriture);
        }
    }

    public void phrase_suivante()
    {
        bouton_continuer.SetActive(false);
        if (index < phrases.Length -1 )
        {
            index += index;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            bouton_continuer.SetActive(false);
        }
    }
    
    
    
    
    
}