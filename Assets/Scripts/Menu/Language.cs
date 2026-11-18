using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class Language : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        if (!string.IsNullOrEmpty(GameData.Langage))
        {
            SetLanguage(GameData.Langage);
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

    public void SetBelarusian()
    {
        SetLanguage("be");
        GameData.Langage = "be";
    }
    public void SetRussian()
    {
        SetLanguage("ru");
        GameData.Langage = "ru";
    }
    public void SetEnglish()
    {
        SetLanguage("en");
        GameData.Langage = "en";
    }
    
    public void SetLanguage(string code){
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(code);
        LocalizationSettings.SelectedLocale = locale;
    }
}
