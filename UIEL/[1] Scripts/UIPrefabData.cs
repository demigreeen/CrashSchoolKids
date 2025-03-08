using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "UIPrefabData", menuName = "UIEL/UIPrefab Data")]
public class UIPrefabData : ScriptableObject
{
    public GameObject[] uiPrefabs;
    public Sprite[] uiSprites;

    [MenuItem("Assets/Create/UIEL/UIPrefab Data")]
    public static void CreateUIPrefabData()
    {
        string folderPath = "Assets/UIEL";
        
        // Проверяем, существует ли папка, если нет, создаём её
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "UIEL");
        }

        // Создаем экземпляр ScriptableObject
        UIPrefabData instance = CreateInstance<UIPrefabData>();

        // Сохраняем объект в указанной папке
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/UIPrefabData.asset");
        AssetDatabase.CreateAsset(instance, assetPath);
        AssetDatabase.SaveAssets();

        // Выделяем и открываем созданный файл
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = instance;

        Debug.Log($"UIPrefabData создан и сохранен в {assetPath}");
    }
}