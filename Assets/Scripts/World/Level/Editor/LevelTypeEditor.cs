#if UNITY_EDITOR
 using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class LevelTypeEditor : EditorWindow
{
    public List<string> levelTypes = new List<string>();

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Silent Parrot Studios/Level Type Generator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(LevelTypeEditor));
    }

    public LevelTypeEditor()
    {
        GetEnum();
    }

    void OnGUI()
    {
        GUILayout.Label("Level Type Generator", EditorStyles.boldLabel);
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty levelTypesProperty = so.FindProperty("levelTypes");
        EditorGUILayout.PropertyField(levelTypesProperty, true);
        so.ApplyModifiedProperties();

        if(GUILayout.Button("Generate"))
        {
            GenerateEnum();
        }
    }

    void GetEnum()
    {
        string enumName = "LevelType";
        string filePathAndName = "Assets/Scripts/World/Level/Enums/" + enumName + ".cs"; //The folder Scripts/Enums/ is expected to exist

        using (StreamReader streamReader = new StreamReader(filePathAndName))
        {
            string line;

            while((line = streamReader.ReadLine()) != null)
            {
                if (line.Contains(","))
                    levelTypes.Add(line.Trim().Split(',')[0]);
            }
        }
    }

    void GenerateEnum()
    {
        string enumName = "LevelType";
        string filePathAndName = "Assets/Scripts/World/Level/Enums/" + enumName + ".cs"; //The folder Scripts/Enums/ is expected to exist

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < levelTypes.Count; i++)
            {
                streamWriter.WriteLine("\t" + levelTypes[i] + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
#endif