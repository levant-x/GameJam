using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;


enum LocaleType
{
    EN, RU
}


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pageMain;
    [SerializeField] private GameObject pageGameRules;
    [SerializeField] private GameObject pageGameOver;
    [SerializeField] private GameObject pagePause;
    [SerializeField] private GameObject pageGame;
    [SerializeField] private SoundsManager soundsManager;

    private static MenuManager instance;
    private static Dictionary<string, Action> pageSwitcher = new Dictionary<string, Action>()
    {
        { "Start", Play },
        { "Pause", Pause },
        { "Resume", Resume },
        { "ExitToMain", () => SceneManager.LoadScene("Menu") },
        { "Exit", () => Application.Quit() },
        { "Restart", Play }
    }; 

    public void SwitchPage(Button sender)
    {
        var cmdName = sender.name.Replace("btn", null);
        var parent = GetParentPageObj(sender.gameObject);

        if (!pageSwitcher.ContainsKey(cmdName)) throw new Exception($"Command {cmdName} missing");
        parent.SetActive(false);
        pageSwitcher[cmdName]();
    }

    public void ToggleBackgroundSound()
    {
        soundsManager.ToggleBackgroundSound();
    }

    public void SetLocale(Toggle selector)
    {
        if (!selector.isOn) return;

        var localeKey = selector.name.Replace("tgl", null);
        var localeType = (int)Enum.Parse(typeof(LocaleType), localeKey);
        var localesAvail = LocalizationSettings.AvailableLocales.Locales;
        LocalizationSettings.SelectedLocale = localesAvail[localeType];
    }

    private void Start()
    {
        instance = this;
        var allButtons = transform.GetComponentsInChildren<Button>(true);
        foreach (var btn in allButtons) WireupButtonSounds(btn);

        var currSceneName = SceneManager.GetActiveScene().name;
        if (currSceneName == "Menu") pageMain.SetActive(true);
        else if (currSceneName == "Game") pageGame.SetActive(true);
    }

    private void WireupButtonSounds(Button button)
    {
        var evTrigger = button.gameObject.AddComponent<EventTrigger>();

        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
        };        
        entry.callback.AddListener(e => soundsManager.PlayHover());
        evTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener(e => soundsManager.PlayClick());
        evTrigger.triggers.Add(entry);
    }

    private GameObject GetParentPageObj(GameObject target)
    {
        if (target.name.Contains("Page")) return target;
        return GetParentPageObj(target.transform.parent.gameObject);
    }

    private static void Play()
    {
        SceneManager.LoadScene("Game");
    }

    private static void Pause()
    {
        Time.timeScale = 0;
        instance.pagePause.SetActive(true);
    }

    private static void Resume()
    {
        Time.timeScale = 1;
        instance.pageGame.SetActive(true);
    }
}
