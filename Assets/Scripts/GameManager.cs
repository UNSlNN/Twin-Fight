using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Transform> characters;
    private Transform character;
    private Health playerHealth1;
    private Health playerHealth2;
    private int whichCharacter;
    private float time = 6f;
    private float countTimer = 6f;

    private TMP_Text textTimer;
    private TMP_Text readyText;
    private TMP_Text winnertext;
    public GameObject winnerUI;

    public static bool pause = false;   // Stop time before starting a new turn, sent to other script
    public bool gameEnded = false;

    void Start()
    {
        // Set UI //
        textTimer = GameObject.Find("Time").GetComponent<TMP_Text>();   // To show Timer
        readyText = GameObject.Find("Ready").GetComponent<TMP_Text>();  // Text "ready" before new turn
        readyText.text = "Ready";
        winnertext = GameObject.Find("Winner").GetComponent<TMP_Text>();
        readyText.gameObject.SetActive(false);
        winnerUI.SetActive(false);
        // Set player Health //
        playerHealth1 = characters[0].GetComponent<Health>();
        playerHealth2 = characters[1].GetComponent<Health>();
        // Set Swapping //
        // Swap to first player in first turn
        if (character == null && characters.Count >= 1){
            character = characters[0];
        }
        Swap();
    }

    void Update()
    {
        // Check Player Winner? | End Game //
        if (!gameEnded)
        {
            if (playerHealth1.currentHealth <= 0 && playerHealth2.currentHealth > 0)
            {
                EndGame("Player Red");
            }
            else if (playerHealth2.currentHealth <= 0 && playerHealth1.currentHealth > 0)
            {
                EndGame("Player Blue");
            }

            ////////////////////////////////////////////////////////////////

            countTimer -= Time.deltaTime;
            if (countTimer <= 0)        // Timer limit turn
            {
                pause = true;
                readyText.gameObject.SetActive(true);

                if (countTimer < -5)    // countdown 5 sec, To swap player
                {
                    if (whichCharacter == 0)
                    {
                        whichCharacter = characters.Count - 1;
                    }
                    else
                    {
                        whichCharacter -= 1;
                    }
                    Swap();

                    countTimer = time;
                    readyText.gameObject.SetActive(false);
                }
            }
            textTimer.SetText("{0}", (int)countTimer);   // Set text value, Change float to int
        }
    }
    public void Swap()
    {
        character = characters[whichCharacter];
        character.GetComponent<Shoot>().enabled = true;

        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i] != character)
            {
                characters[i].GetComponent<Shoot>().enabled = false;
            }
        }
        pause = false;
    }

    ////////////////////////////////////////////////////////////////

    // Applied damage to a specific player
    public void ApplyDamageToPlayer1(float damageAmount){
        playerHealth1.DamageAmount(damageAmount);
    }

    public void ApplyDamageToPlayer2(float damageAmount){
        playerHealth2.DamageAmount(damageAmount);
    }

    void EndGame(string playerName) // When we have winner, Show UI
    {
        gameEnded = true;
        winnerUI.SetActive(true);
        winnertext.text = "The Winner " + playerName;
        Invoke("RestartGame", 10f);
    }

    public void RestartGame()  // Restart new game
    {
        gameEnded = false;
        winnerUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
