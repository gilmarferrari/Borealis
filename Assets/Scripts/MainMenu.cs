using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject SceneriesPanel;
    public GameObject CreditsPanel;
    public GameObject ControlsPanel;
    public GameObject ConfirmQuitPanel;
    public AudioClip ButtonInteractionClip;
    public AudioClip ButtonHightlightClip;
    public List<Button> SceneriesButtons;

    void Awake()
    {
        var data = SaveSys.LoadData();
        GameSettings.UpdateUnlockedSceneries(data.UnlockedSceneries);

        foreach (var button in SceneriesButtons)
        {
            var lockImages = button.GetComponentsInChildren<Image>().Where(b => b.name.Equals("Locked")).ToList();
            var highestScoreTexts = button.GetComponentsInChildren<TextMeshProUGUI>().Where(t => t.name.Equals("Highest Score Text")).FirstOrDefault();

            foreach (var lockImage in lockImages)
            {
                var buttonScenery = GameSettings.AvailableSceneries.Where(s => lockImage.tag.Equals($"{s.Description}Scenery")).FirstOrDefault();
                var isUnlocked = GameSettings.UnlockedSceneries.Select(s => $"{s.Description}Scenery").Contains(lockImage.tag);

                lockImage.gameObject.SetActive(!isUnlocked);
                button.enabled = isUnlocked;

                if (highestScoreTexts != null && buttonScenery != null)
                {
                    var sceneryHighestScore = data.SceneriesHighestScores.Where(s => s.Key == (int)buttonScenery.Type).FirstOrDefault().Value;

                    highestScoreTexts.gameObject.SetActive(isUnlocked);
                    highestScoreTexts.text = $"Highest Score: {sceneryHighestScore}";
                }
            }
        }

        ShowPanel(MainPanel);
    }

    public void Play()
    {
        PlayButtonInteractionClip();
        ShowPanel(SceneriesPanel);
    }

    public void PlayScenery(int scenery)
    {
        GameSettings.Init((Sceneries)scenery, Difficulties.Adventure);
        SceneManager.LoadScene("In-Game");
    }

    public void ShowCredits()
    {
        PlayButtonInteractionClip();
        ShowPanel(CreditsPanel);
    }

    public void ShowControls()
    {
        PlayButtonInteractionClip();
        ShowPanel(ControlsPanel);
    }

    public void Quit()
    {
        PlayButtonInteractionClip();
        ShowPanel(ConfirmQuitPanel);
    }

    public void ConfirmQuit()
    {
        PlayButtonInteractionClip();
        Application.Quit();
    }

    public void Return()
    {
        PlayButtonInteractionClip();
        ShowPanel(MainPanel);
    }

    void PlayButtonInteractionClip()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = ButtonInteractionClip;
        audioSource.pitch = 3f;
        audioSource.Play();
    }

    public void OnButtonHightlight()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = ButtonHightlightClip;
        audioSource.pitch = 2f;
        audioSource.Play();
    }

    void ShowPanel(GameObject panel)
    {
        MainPanel.SetActive(panel == MainPanel);
        SceneriesPanel.SetActive(panel == SceneriesPanel);
        CreditsPanel.SetActive(panel == CreditsPanel);
        ControlsPanel.SetActive(panel == ControlsPanel);
        ConfirmQuitPanel.SetActive(panel == ConfirmQuitPanel);
    }
}
