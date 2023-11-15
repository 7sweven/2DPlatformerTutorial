using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemCollecter : MonoBehaviour
{
    private int pineapples;
    [SerializeField] private TMP_Text pineappleCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pineapple"))
        {
            Destroy(collision.gameObject);
            pineapples++;
            pineappleCounter.text = "Pineapples: " + pineapples;
        }
    }
}
