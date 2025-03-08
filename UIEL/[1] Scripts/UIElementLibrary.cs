using UnityEditor;
using UnityEngine;

public class UIElementLibrary : EditorWindow
{
    private UIPrefabData prefabData;

    private const float buttonHeight = 40f; // Высота кнопок
    private const float buttonWidth = 180f; // Ширина кнопок
    private Vector2 scrollPosition;

    [MenuItem("UIEL/Show")]
    public static void ShowWindow()
    {
        GetWindow<UIElementLibrary>("UI Element Library");
    }

    private void OnEnable()
    {
        CheckForTMP(); // Проверка наличия TMP
        prefabData = AssetDatabase.LoadAssetAtPath<UIPrefabData>("Assets/UIEL/UIPrefabData.asset");
        if (prefabData == null)
        {
            Debug.LogError("Данные о сборке не найдены. Пожалуйста, создайте ее в ADDUIEL.");
        }
    }

    private void OnGUI()
    {
        // Контейнер для прокрутки
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Центровка заголовка
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Библиотека элементов пользовательского интерфейса", EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        if (prefabData == null || prefabData.uiPrefabs == null || prefabData.uiSprites == null ||
            prefabData.uiPrefabs.Length == 0 || prefabData.uiSprites.Length == 0)
        {
            GUILayout.Label("Префабы или спрайты не найдены. Добавьте их в ADDUIEL.");
            return;
        }

        if (prefabData.uiPrefabs.Length != prefabData.uiSprites.Length)
        {
            GUILayout.Label("Несоответствие между количеством префабов и спрайтов. Исправьте в ADDUIEL.");
            return;
        }

        // Основной блок с элементами UI
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical();

        int columns = 3; // Число элементов в ряду
        for (int i = 0; i < prefabData.uiPrefabs.Length; i++)
        {
            GameObject prefab = prefabData.uiPrefabs[i];
            Sprite sprite = prefabData.uiSprites[i];

            if (prefab == null || sprite == null) continue;

            // Начинаем новый горизонтальный ряд, если 3 элемента на ряду
            if (i % columns == 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
            }

            // Пространство для спрайта с учётом пропорций
            Rect spriteRect = GUILayoutUtility.GetRect(100, 100);
            Rect imageRect = new Rect(spriteRect.x, spriteRect.y, sprite.texture.width * 100 / Mathf.Max(sprite.texture.width, sprite.texture.height), sprite.texture.height * 100 / Mathf.Max(sprite.texture.width, sprite.texture.height));

            // Отображаем изображение с правильным масштабом
            EditorGUI.DrawPreviewTexture(imageRect, sprite.texture);

            // Обрабатываем клик по изображению
            if (Event.current.type == EventType.MouseUp && imageRect.Contains(Event.current.mousePosition))
            {
                AddPrefabToCanvas(prefab);
                Event.current.Use();
            }

            GUILayout.Space(5);

            // Завершаем горизонтальный контейнер после последнего элемента в ряду
            if (i % columns == columns - 1 || i == prefabData.uiPrefabs.Length - 1)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

        // Область для кнопок внизу окна
        Rect buttonArea = new Rect(0, position.height - buttonHeight, position.width, buttonHeight);

        // Фон для области кнопок
        Texture2D grayTexture = new Texture2D(1, 1);
        grayTexture.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f));
        grayTexture.Apply();
        GUI.DrawTexture(buttonArea, grayTexture);

        GUILayout.BeginArea(buttonArea);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Перейти в Telegram канал", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            Application.OpenURL("https://t.me/lunasundesign");
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Рандомная кнопка", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            AddRandomPrefabToCanvas();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void CheckForTMP()
    {
        string[] tmpAssets = AssetDatabase.FindAssets("t:Script TextMeshPro");

        if (tmpAssets.Length == 0)
        {
            EditorUtility.DisplayDialog(
                "TextMesh Pro не найден",
                "TextMesh Pro не установлен в проекте. Пожалуйста, установите его из Unity Package Manager.",
                "Понятно"
            );
            Debug.LogError("TextMesh Pro не установлен в проекте. Установите его через Unity Package Manager.");
        }
    }

    private void AddPrefabToCanvas(GameObject prefab)
    {
        GameObject selectedCanvas = Selection.activeGameObject;

        if (selectedCanvas == null || selectedCanvas.GetComponentInParent<Canvas>() == null)
        {
            EditorUtility.DisplayDialog("Ошибка", "Пожалуйста, выберите канвас в иерархии.", "Хорошо");
            return;
        }

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, selectedCanvas.transform);

        Undo.RegisterCreatedObjectUndo(instance, "Add UI Element");
        Selection.activeGameObject = instance;
    }

    private void AddRandomPrefabToCanvas()
    {
        if (prefabData.uiPrefabs.Length == 0)
        {
            EditorUtility.DisplayDialog("Ошибка", "Готовых конструкций в наличии нет.", "Хорошо");
            return;
        }

        GameObject randomPrefab = prefabData.uiPrefabs[Random.Range(0, prefabData.uiPrefabs.Length)];
        AddPrefabToCanvas(randomPrefab);
    }
}
