using UnityEngine;

[CreateAssetMenu(fileName = "ending", menuName = "Game/Ending")]
public class EndingsSO : ScriptableObject
{
   
    public int endingIndex;

    public Sprite image;

    public string localizationKey => $"ENDING_{endingIndex}";
}
