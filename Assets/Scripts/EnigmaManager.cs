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
        // Initialize the first enigma
        if (enigmas.Count > 0)
        {
            enigmas[0].virtualToken.SetActive(true);
            for (int i = 1; i < enigmas.Count; i++)
            {
                enigmas[i].virtualToken.SetActive(false);
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
        enigmas[currentEnigmaIndex].virtualToken.SetActive(false);
        
        currentEnigmaIndex++;
        
        if (currentEnigmaIndex < enigmas.Count)
        {
            // Show next token
            enigmas[currentEnigmaIndex].virtualToken.SetActive(true);
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