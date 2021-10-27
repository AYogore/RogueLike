using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public int _pickupQuantity;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //This might be changed later in favor for enumerators based on
            //the size of the gold/level of the character
            _pickupQuantity = Random.Range(1, 10);
            PlayerStatistics._playerStatistics._goldAmount += _pickupQuantity;
            Destroy(gameObject);
            PlayerStatistics._playerStatistics._goldText.text = PlayerStatistics._playerStatistics._goldAmount.ToString();
        }
    }
}
