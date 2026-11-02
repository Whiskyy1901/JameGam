#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

// Put this file in a folder named "Editor" (Assets/Editor/ConsoleAutoClear.cs).
// Toggle with Tools > Auto Clear Console.
[InitializeOnLoad]
public static class ConsoleClearer
{
    const float Interval = 10f;
    const string PrefKey = "ConsoleAutoClear_Enabled";
    const string MenuPath = "Tools/Auto Clear Console";

    static double nextClear;

    static ConsoleClearer()
    {
        nextClear = EditorApplication.timeSinceStartup + Interval;
        EditorApplication.update += Tick;
    }

    static bool Enabled
    {
        get => EditorPrefs.GetBool(PrefKey, true);
        set => EditorPrefs.SetBool(PrefKey, value);
    }

    [MenuItem(MenuPath)]
    static void Toggle() => Enabled = !Enabled;

    [MenuItem(MenuPath, true)]
    static bool ToggleValidate()
    {
        Menu.SetChecked(MenuPath, Enabled);
        return true;
    }

    static void Tick()
    {
        if (!Enabled || EditorApplication.timeSinceStartup < nextClear) return;
        nextClear = EditorApplication.timeSinceStartup + Interval;
        ClearConsole();
    }

    static void ClearConsole()
    {
        var type = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        type?.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public)?.Invoke(null, null);
    }
}
#endif