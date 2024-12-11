using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private Renderer visablitiy;
    private Collider collider;
    Animator anim;

    NewBehaviourScript pc;
    public enum PickupType
    {
        JumpBoost,
        Shrink,
    }

    bool isVisable = true;
    

    public PickupType type;


    // Start is called before the first frame update
    void Start()
    {
        visablitiy = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        pc = GetComponent<NewBehaviourScript>();
    }

    public void ToggleObjectVisibility(bool isVisible)
    {
        if (visablitiy != null)
        {
            // Set the Renderer to visible or invisible
            visablitiy.enabled = isVisible;
        }

        if (collider != null)
        {
            // Enable or disable the Collider based on visibility
            collider.enabled = isVisible;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NewBehaviourScript pc = collision.gameObject.GetComponent<NewBehaviourScript>();

            switch (type)
            {
                case PickupType.JumpBoost:
                    pc.JumpPowerUp();
                    break;
                case PickupType.Shrink:
                    pc.shrinkPowerUp();
                    break;
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && CompareTag("Key"))
        {
            //NewBehaviourScript pc = collision.gameObject.GetComponent<NewBehaviourScript>();
            anim.SetTrigger("fire On");
            ToggleObjectVisibility(isVisable);
            //isVisable = false;
            Debug.Log("is hitting");


        }

       
    }
    
}

