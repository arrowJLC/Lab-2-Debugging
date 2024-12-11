using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IPickup
{
    public void Pickup(GameObject mario)
    {
        NewBehaviourScript pc = GetComponent<NewBehaviourScript>();
        pc.JumpPowerUp();
        pc.shrinkPowerUp();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}