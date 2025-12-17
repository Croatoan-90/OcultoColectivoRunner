using TMPro;
using UnityEngine;

public class UIResultManager : MonoBehaviour
{
    public static UIResultManager instance;
    [SerializeField] private TextMeshProUGUI resultText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    // Actualia el texto del UI con el resultado final
    public void UpdateResultText(int result)
    {
        resultText.text = "El final resultante es el número: " + (result + 1);
    }
  
}
