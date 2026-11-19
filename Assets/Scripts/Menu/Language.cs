using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class Language : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        SetLanguage(GameData.Langage);
        
        switch(GameData.Langage){
            case "be-BY": dropdown.SetValueWithoutNotify(0);
                break;
            case "en-US": dropdown.SetValueWithoutNotify(1);
                break;
            case "ru-RU": dropdown.SetValueWithoutNotify(2);
                break;
        }
    }

    public void LanguageSelection(int index){
        switch(index){
            case 0: SetLanguage("be-BY");
                GameData.Langage = "be-BY";
                break;
            case 1: SetLanguage("en-US");
                GameData.Langage = "en-US";
                break;
            case 2: SetLanguage("ru-RU");
                GameData.Langage = "ru-RU";
                break;
        }
    }
    
    public void SetLanguage(string code){
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(code);
        LocalizationSettings.SelectedLocale = locale;
    }
}
