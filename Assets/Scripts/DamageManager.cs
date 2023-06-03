using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public Health player1Health;
    public Health player2Health;

    // Applied damage to a specific player
    public void ApplyDamageToPlayer1(float damageAmount)
    {
        player1Health.DamageAmount(damageAmount);
    }

    public void ApplyDamageToPlayer2(float damageAmount)
    {
        player2Health.DamageAmount(damageAmount);
    }
}
