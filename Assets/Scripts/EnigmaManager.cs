using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EnigmaManager : MonoBehaviour
{
    [System.Serializable]
    public class Enigma
    {
        public string title;
        public string riddle;
        public string targetLocation;
        public GameObject virtualToken;
        public Transform targetAnchor;
        [HideInInspector] public GameObject virtualTokenInstance;
    }

    [Header("Enigma Settings")]
    public List<Enigma> enigmas = new List<Enigma>();
    public int currentEnigmaIndex = 0;

    [Header("UI References")]
    public TextMeshProUGUI riddleText;
    public TextMeshProUGUI feedbackText;
    public GameObject completionPanel;

    private void Start()
    {
        InitializeEnigmas();
        DisplayCurrentEnigma();
    }

    private void InitializeEnigmas()
    {
        // Instantiate all tokens but only activate the first one
        for (int i = 0; i < enigmas.Count; i++)
        {
            if (enigmas[i].virtualToken != null)
            {
                Vector3 spawnPos = Camera.main != null ? Camera.main.transform.position + Camera.main.transform.forward * 1.5f : Vector3.zero;
                enigmas[i].virtualTokenInstance = Instantiate(enigmas[i].virtualToken, spawnPos, Quaternion.identity);
                enigmas[i].virtualTokenInstance.SetActive(i == 0);
            }
        }
    }

    public void DisplayCurrentEnigma()
    {
        if (currentEnigmaIndex < enigmas.Count)
        {
            Enigma currentEnigma = enigmas[currentEnigmaIndex];
            riddleText.text = currentEnigma.riddle;
            feedbackText.text = "Find the correct location and place your token!";
            // Activate only the current token
            for (int i = 0; i < enigmas.Count; i++)
            {
                if (enigmas[i].virtualTokenInstance != null)
                    enigmas[i].virtualTokenInstance.SetActive(i == currentEnigmaIndex);
            }
        }
    }

    public void OnCorrectPlacement()
    {
        Enigma currentEnigma = enigmas[currentEnigmaIndex];
        feedbackText.text = "Well done! You found the right place!";
        
        // Show completion panel
        completionPanel.SetActive(true);

        // Move to next enigma after a delay
        Invoke("AdvanceToNextEnigma", 3f);
    }

    private void AdvanceToNextEnigma()
    {
        // Hide current token
        if (enigmas[currentEnigmaIndex].virtualTokenInstance != null)
            enigmas[currentEnigmaIndex].virtualTokenInstance.SetActive(false);
        currentEnigmaIndex++;
        if (currentEnigmaIndex < enigmas.Count)
        {
            // Show next token
            if (enigmas[currentEnigmaIndex].virtualTokenInstance != null)
                enigmas[currentEnigmaIndex].virtualTokenInstance.SetActive(true);
            DisplayCurrentEnigma();
        }
        else
        {
            // Game completed
            feedbackText.text = "Congratulations! You've completed all enigmas!";
        }
        completionPanel.SetActive(false);
    }
} 