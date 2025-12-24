using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Ending Database")]
public class EndingDatabase : ScriptableObject
{
    public List<EndingsSO> endings;

    private Dictionary<int, EndingsSO> cache;

    public void Init()
    {
        cache = new Dictionary<int, EndingsSO>();

        foreach (var ending in endings)
        {
            cache[ending.endingIndex] = ending;
        }
    }

    public EndingsSO GetEnding(int id)
    {
        if(cache == null) Init();
        return cache.TryGetValue(id, out var ending) ? ending : null;
    }

}
