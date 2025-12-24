using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    // Método que se adjunta en los botones para cambiar las características
    public void SelectedLocale(string localeCode)
    {
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);

        LocalizationSettings.SelectedLocale = locale;
    }
}
