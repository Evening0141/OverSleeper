using System.Collections.Generic;
using UnityEngine;

public static class NameGenerator
{
    private static readonly string[] namePool = new string[]
    {
        "Aeris", "Balin", "Celia", "Doran", "Elia",
        "Frey", "Gilda", "Haku", "Isla", "Juno",
        "Kael", "Lyra", "Mira", "Nox", "Orin",
        "Pia", "Quen", "Ravi", "Sora", "Tess",
        "Uri", "Vera", "Wren", "Xeno", "Yuki",
        "Zane", "Akari", "Blitz", "Cyrus", "Dahlia",
        "Emil", "Fina", "Garn", "Hilda", "Irie",
        "Jace", "Kanon", "Lira", "Marlo", "Nina",
        "Owen", "Peri", "Quill", "Rina", "Silva",
        "Toma", "Uta", "Valk", "Waka", "Zen",
        "Alice", "Bob", "Charlie", "Diana", "Eve",
        "Frank", "Grace", "Heidi", "Ivan", "Judy",
        "Ken", "Luna", "Milo", "Oscar", "Jelly",
        "BB", "Osho","Sin","Evening","Kaname","Sekinee"
    };

    private static HashSet<string> usedNames = new HashSet<string>();
    private static System.Random rng = new System.Random();

    // 使用されていない名前からランダム取得
    public static string GetUniqueName()
    {
        List<string> availableNames = new List<string>();
        foreach (var name in namePool)
        {
            if (!usedNames.Contains(name))
                availableNames.Add(name);
        }

        if (availableNames.Count == 0)
        {
            Debug.LogWarning("全ての名前が使用中です");
            return "Unknown";
        }

        string selected = availableNames[rng.Next(availableNames.Count)];
        usedNames.Add(selected);
        return selected;
    }

    // 使用が終わった名前を解放
    public static void ReleaseName(string name)
    {
        usedNames.Remove(name);
    }
}
