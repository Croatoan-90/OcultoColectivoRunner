using UnityEngine;

[CreateAssetMenu(fileName = "ending", menuName = "Game/Ending")]
public class EndingsSO : ScriptableObject
{
    [Range(0f, 243f)]
    public int endingIndex;

    public Sprite image;

    [TextArea(2,6)]
    public string description;
}
