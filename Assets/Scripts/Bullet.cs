using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageManager damageamount = FindObjectOfType<DamageManager>();

        if (collision.gameObject.tag == "Player1")
        {
            // Apply damage to Player 1
            damageamount.ApplyDamageToPlayer1(10f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player2")
        {
            // Apply damage to Player 2
            damageamount.ApplyDamageToPlayer2(10f);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
