using UnityEditor;
using UnityEngine;

public class AddUIElementLibrary : EditorWindow
{
    private const string PREFAB_DATA_FOLDER = "Assets/UIEL";
    private const string PREFAB_DATA_PATH = PREFAB_DATA_FOLDER + "/UIPrefabData.asset";
    private UIPrefabData prefabData;
    private Vector2 scrollPosition; // Переменная для хранения позиции прокрутки

    [MenuItem("Window/ADDUIEL")]
    public static void ShowWindow()
    {
        GetWindow<AddUIElementLibrary>("Add UI Element Library");
    }

    private void OnEnable()
    {
        LoadPrefabData();
    }

    private void OnGUI()
    {
        if (prefabData == null)
        {
            GUILayout.Label("No Prefab Data found.");
            if (GUILayout.Button("Create Prefab Data"))
            {
                CreatePrefabData();
            }
            return;
        }

        SerializedObject so = new SerializedObject(prefabData);
        SerializedProperty prefabsProperty = so.FindProperty("uiPrefabs");
        SerializedProperty spritesProperty = so.FindProperty("uiSprites");

        GUILayout.Label("Add Prefabs and Sprites", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition); // Начало области прокрутки

        EditorGUILayout.PropertyField(prefabsProperty, true);
        EditorGUILayout.PropertyField(spritesProperty, true);

        EditorGUILayout.EndScrollView(); // Конец области прокрутки

        if (prefabData.uiPrefabs.Length != prefabData.uiSprites.Length)
        {
            EditorGUILayout.HelpBox("The number of prefabs and sprites must match.", MessageType.Warning);
        }

        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(prefabData);
            AssetDatabase.SaveAssets();
        }

        so.ApplyModifiedProperties();
    }

    private void LoadPrefabData()
    {
        prefabData = AssetDatabase.LoadAssetAtPath<UIPrefabData>(PREFAB_DATA_PATH);
        if (prefabData == null)
        {
            Debug.LogWarning($"Prefab Data not found at {PREFAB_DATA_PATH}. You can create it.");
        }
    }

    private void CreatePrefabData()
    {
        // Создаём папку, если её нет
        if (!AssetDatabase.IsValidFolder(PREFAB_DATA_FOLDER))
        {
            AssetDatabase.CreateFolder("Assets", "UIEL");
        }

        // Создаём ScriptableObject и сохраняем его в папке
        prefabData = ScriptableObject.CreateInstance<UIPrefabData>();
        AssetDatabase.CreateAsset(prefabData, PREFAB_DATA_PATH);
        AssetDatabase.SaveAssets();

        Debug.Log($"Prefab Data created at {PREFAB_DATA_PATH}");
    }
}
