using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AppMenu : MonoBehaviour
{    
    static string packageFile = "Res2D_Game(base).unitypackage";
    static int DumyNum = 10;    
    [MenuItem("Edit/Export BackUp", false, 0)]
    static void action01()
    {
        string[] exportpaths = new string[]
        {
            "Assets/Animations",
            "Assets/Resources",            
            "Assets/Scenes",
            "Assets/Plugins",
            "Assets/Scripts",
            "Assets/Editor",
            "Assets/Fonts",
            "Assets/Sprites",
            "ProjectSettings/TagManager.asset",
            "ProjectSettings/InputManager.asset",
            "ProjectSettings/EditorBuildSettings.asset",
            "ProjectSettings/EditorSettings.asset"
        };

        AssetDatabase.ExportPackage(exportpaths, packageFile, ExportPackageOptions.Interactive
            | ExportPackageOptions.Recurse |
            ExportPackageOptions.IncludeDependencies | ExportPackageOptions.IncludeLibraryAssets);

        print("Backup Export Complete!");
    }

    [MenuItem("Edit/Improt BackUp", false, 1)]
    static void action02()
    {
        AssetDatabase.ImportPackage(packageFile, true);

    }
    [MenuItem("PlayerPrefs/Delete all", false, 1)]
    static void PlayerPrefsDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("PlayerPrefs/CreatePlayer", false, 2)]
    static void CreatePlayer()
    {
        string s = "";
        for (int i = 0; i < DumyNum - 1; i++)
            s += i.ToString() + '/';
        s += (DumyNum - 1).ToString();
        PlayerPrefs.SetString("Player", s);
    }
    [MenuItem("PlayerPrefs/Create Dumy Data", false, 3)]
    static void CreateDumyData()
    {
        for (int i = 0; i < DumyNum; i++)
        {
            PlayerPrefs.SetInt('2' + i.ToString(), i); PlayerPrefs.SetInt('3' + i.ToString(), i); PlayerPrefs.SetString(i.ToString(), "100");
        }
    }
    [MenuItem("PlayerPrefs/DebugPlayerString", false, 4)]
    static void DebugPlayerString()
    {
        Debug.Log(PlayerPrefs.GetString("Player", "Player Null"));
    }
    [MenuItem("Create Prefabs/Alert Create", false, 0)]
    static void CreateAlert()
    {
        GameObject alert = Resources.Load("AlertUI") as GameObject;
        Transform transform = GameObject.Find("Canvas").transform;
        Instantiate(alert,transform);
    }
    //[MenuItem("PlayerPrefs/DebugPlayerPrefs", false, 5)]
    //static void DebugPlayerPrefs()
    //{        
    //    for (int i = 0; i < AppMgr.App.playerInfo.Count; i++)
    //    {
    //        Debug.Log("Nick = " + i + " " + PlayerPrefs.GetInt('F' + i.ToString(), -1) + " " + 
    //        PlayerPrefs.GetInt('A' + i.ToString(), -1) + " "+  PlayerPrefs.GetString(i.ToString(), "Coin Null"));
    //    }
    //}
}
