using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Transform> characters;
    private Transform character;
    private int whichCharacter;
    private float time = 6f;
    private float countTimer = 6f;

    private TMP_Text textTimer;
    private TMP_Text readyText;

    public static bool pause = false;   // Stop time before starting a new turn, sent to other script

    void Start()
    {
        textTimer = GameObject.Find("Time").GetComponent<TMP_Text>();   // To show Timer
        readyText = GameObject.Find("Ready").GetComponent<TMP_Text>();  // Text "ready" before new turn
        readyText.gameObject.SetActive(false);

        // Swap to first player in first turn
        if (character == null && characters.Count >= 1){
            character = characters[0];
        }
        Swap();
    }

    void Update()
    {
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
        textTimer.SetText("{0}",(int)countTimer);   // Set text value, Change float to int
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
}
