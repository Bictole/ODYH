using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    //on recupere la prefab du slot
    [SerializeField] 
    private GameObject slotprefab;


    //ref au component present canvasgroup
    private CanvasGroup canvasgr;


    public bool Open
    {
        get
        {
            return canvasgr.alpha > 0;
        }
    }
    
    
    //on cree les slots correspondant au bag
    public void Initslots(int nbslots)
    {
        for (int i = 0; i < nbslots; i++)
        {
            Instantiate(slotprefab, transform);
        }
    }

    public void OpenOrClose()
    {
        canvasgr.alpha = canvasgr.alpha > 0 ? 0 : 1;        //set le canvasgr a 0 ou 1 selon l'ouverture

        canvasgr.blocksRaycasts = canvasgr.blocksRaycasts == true ? false : true;
    }

    private void Awake() //on doit recuperer le component canvasgroup des le debut 
    {
        canvasgr = GetComponent<CanvasGroup>();
    }
}
