using UnityEngine;
using UnityEditor;

public class EditorExtension
{
    // Deep clone

    [MenuItem("OctoBox/Erase player prefs", false, 500)]
    static void DoErasePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.LogWarning("PlayerPrefs erased");
    }

    [MenuItem("Edit/Move Down #DOWN")]
    static void DoSortDown()
    {
        foreach (Transform transform in Selection.transforms)
        {
            int index = transform.GetSiblingIndex();
            transform.SetSiblingIndex(index + 1);
        }
    }

    [MenuItem("Edit/Move Up #UP")]
    static void DoSortUp()
    {
        foreach (Transform transform in Selection.transforms)
        {
            int index = transform.GetSiblingIndex();
            transform.SetSiblingIndex(index - 1);
        }
    }

    [MenuItem("Assets/Build AssetBundles Android")]
    static void BuildAllAssetBundles()
    {
        //BuildPipeline.BuildAssetBundles("AssetBundles");
        BuildPipeline.BuildAssetBundles("AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
    [MenuItem("Assets/Build AssetBundles iOS")]
    static void BuildAllAssetBundlesIPhone()
    {
        //BuildPipeline.BuildAssetBundles("AssetBundles");
        BuildPipeline.BuildAssetBundles("AssetBundles/iOS", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }

    [MenuItem("Assets/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }

}

