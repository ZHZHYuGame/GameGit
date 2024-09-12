using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LanguageMgr : MonoBehaviour
{
    readonly LocalizedStringTable stringTable = new LocalizedStringTable { TableReference = "UITextTable" };
    //public static Action<StringTable> ChangedText;

    private void OnEnable()
    {
        stringTable.TableChanged += LoadString;
    }

    private void OnDisable()
    {
        stringTable.TableChanged += LoadString;
    }

    private void LoadString(StringTable value)
    {
        MessageEventMgr.GetInstance().Dispatch(MessageType.TableChanged, value);
        //ChangedText(value);
    }

    public static string GetText(StringTable table, string name)
    {
        var text = table.GetEntry(name);
        return text.GetLocalizedString();
    }

    public static void SwitchChinese()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }
    public static void SwitchEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }
}
