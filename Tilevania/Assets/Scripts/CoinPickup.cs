using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinPointsValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collider2D is " + collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddToScore(coinPointsValue);
        }
    }
}
