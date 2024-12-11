using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinConditions : MonoBehaviour
{
    public Transform playerTransform;
    void OnTriggerEnter2D(Collider2D collider)
    {
        //if (collision.gameObject.CompareTag("Player"))
        {
            var p = collider.gameObject.GetComponent<NewBehaviourScript>();

            if (p != null)
            {
                SceneManager.LoadScene("Win Scene");
            }
        }
    }
}


