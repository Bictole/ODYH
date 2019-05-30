using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Player : Character
{
    private SFXManager sfx;

    public GameObject flèche;

    public GameObject fireball;

    public GameObject bomb;

    // l'inventaire du perso
    [SerializeField]
    private Inventory inventory;

    [SerializeField] 
    private int spellcost;
    [SerializeField]
    private GameObject spellstartpoint;
    [SerializeField]
    private GameObject spelleffect;
    [SerializeField]
    private GameObject explosioneffect;

    public VectorValue startingPosition;

    private bool bowAttackPossible;

    public bool BowAttackPossible
    {
        get => bowAttackPossible;
        set => bowAttackPossible = value;
    }

    private bool magicAttackPossible;

    public bool MagicAttackPossible
    {
        get => magicAttackPossible;
        set => magicAttackPossible = value;
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sfx = FindObjectOfType<SFXManager>();

        inventory = FindObjectOfType<Inventory>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    protected override void Update()
    {
        sfx = FindObjectOfType<SFXManager>();

         if (stopmove)
             return;
         
         Getinput();
         base.Update();
    }
    private void Getinput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        
        nextdirection = direction;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !InInventory)
        {
            if (!IsAttacking)
            // Allow to put a delay in the method
            StartCoroutine(Attack());
        }

        // Attaque à distance à l'arc
        if (Input.GetKeyDown(KeyCode.Mouse1) && !InInventory && bowAttackPossible)
        {
            if (!IsAttackingrangewithbow && !IsAttackingrangewithstaff)
            {
                if (Arrowavailable())
                {
                    Vector3 way = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                
                    Range projectile = Instantiate(flèche, transform.position, transform.rotation)
                        .GetComponent<Range>();
            
                    myAnimator.SetFloat("x", way.x);
                    myAnimator.SetFloat("y", way.y);
                
                    projectile.Setup(way);
            
                    projectile.Isattacking = true;

                    StartCoroutine(Bowattack());

                    
                }
                
            }
        }
        // Attaqie à distance avec Magic staff
        if (Input.GetKeyDown(KeyCode.A) && !InInventory && magicAttackPossible)
        {
            if (!IsAttackingrangewithbow && !IsAttackingrangewithstaff)
            {
                GetComponent<PlayerMana>().Usespell(spellcost);
                if (Manaavailable())
                {
                    Vector3 way = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    
                    myAnimator.SetFloat("x", way.x);
                    myAnimator.SetFloat("y", way.y);

                    StartCoroutine(Spellattack());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && !InInventory)
        {
            if (Bombavailable())
            {
                StartCoroutine(Explosebomb());
            }
        }
    }

    
    private IEnumerator Attack()
    {
        sfx.player_is_attacking.Play();
        IsAttacking = true;
        myAnimator.SetBool("attack", IsAttacking);

        // delay between each attack
        yield return new WaitForSeconds(0.4f);
        
        StopAttack();
    }
    
    private IEnumerator Bowattack()
    {
        sfx.player_is_attacking.Play();
        IsAttackingrangewithbow= true;
        myAnimator.SetBool("attackrangebow", IsAttackingrangewithbow);

        // delay between each attack
        yield return new WaitForSeconds(0.6f);
        
        StopAttackrangewithbow();
    }
    
    
    
    private IEnumerator Spellattack()
    {
        IsAttackingrangewithstaff= true;
        myAnimator.SetBool("attackrangestaff", IsAttackingrangewithstaff);

        // delay between each attack
        yield return new WaitForSeconds(1f);
        
        Vector3 way = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Instantiate(spelleffect, spellstartpoint.transform.position,spellstartpoint.transform.rotation);
                    
        Range projectile = Instantiate(fireball, spellstartpoint.transform.position, transform.rotation)
            .GetComponent<Range>();
        
        projectile.Setup(way);
            
        projectile.Isattacking = true;
        
        StopAttackrangewithstaff();
    }

    // Vérifie si une flèche est disponible dans l'inventaire
    private bool Arrowavailable()
    {
        foreach (var bag in inventory.bags)
        {
            foreach (var slot in bag.BagScr.slotscrList)
            {
                if (slot.TheItem is Flèche)
                {
                    slot.Delete_Item(slot.TheItem);
                    return true;
                }
                    
            }
        }

        return false;
    }
    
    // Vérifie si une bombe est disponible dans l'inventaire
    private bool Bombavailable()
    {
        foreach (var bag in inventory.bags)
        {
            foreach (var slot in bag.BagScr.slotscrList)
            {
                if (slot.TheItem is Bombes)
                {
                    slot.Delete_Item(slot.TheItem);
                    return true;
                }
                    
            }
        }

        return false;
    }
    

    // Vérifie si du mana est disponible pour lancer un sort
    private bool Manaavailable()
    {
        return !GetComponent<PlayerMana>().manaempty;
    }
    
    public IEnumerator Explosebomb()
    {
        GameObject b = Instantiate(bomb, transform.position, transform.rotation);
        
        
        yield return new WaitForSeconds(3f);

        b.transform.GetChild(0).gameObject.SetActive(true);
        Instantiate(explosioneffect, b.transform.position, b.transform.rotation);
    }


}
