using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject completionPanel;
    public GameObject helpPanel;

    [Header("UI Elements")]
    public TextMeshProUGUI riddleText;
    public TextMeshProUGUI feedbackText;
    public Button helpButton;
    public Button closeHelpButton;

    private void Start()
    {
        // Initialize UI
        mainPanel.SetActive(true);
        completionPanel.SetActive(false);
        helpPanel.SetActive(false);

        // Set up button listeners
        helpButton.onClick.AddListener(ShowHelp);
        closeHelpButton.onClick.AddListener(HideHelp);
    }

    public void ShowHelp()
    {
        helpPanel.SetActive(true);
    }

    public void HideHelp()
    {
        helpPanel.SetActive(false);
    }

    public void ShowCompletion()
    {
        completionPanel.SetActive(true);
    }

    public void HideCompletion()
    {
        completionPanel.SetActive(false);
    }

    public void UpdateRiddleText(string text)
    {
        riddleText.text = text;
    }

    public void UpdateFeedbackText(string text)
    {
        feedbackText.text = text;
    }
} 