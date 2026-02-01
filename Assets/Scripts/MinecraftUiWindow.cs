using UnityEngine;
using UnityEngine.UI;

public class MinecraftUiWindow : MonoBehaviour
{
    [Header("Window")]
    [SerializeField] private Vector2 windowSize = new Vector2(720f, 520f);
    [SerializeField] private Color windowColor = new Color(0.22f, 0.22f, 0.22f, 0.96f);
    [SerializeField] private Color borderColor = new Color(0.12f, 0.12f, 0.12f, 1f);

    [Header("Slots")]
    [SerializeField] private int inventoryRows = 3;
    [SerializeField] private int inventoryColumns = 9;
    [SerializeField] private Vector2 slotSize = new Vector2(48f, 48f);
    [SerializeField] private Vector2 slotSpacing = new Vector2(8f, 8f);
    [SerializeField] private Color slotColor = new Color(0.6f, 0.6f, 0.6f, 1f);
    [SerializeField] private Color slotInnerColor = new Color(0.75f, 0.75f, 0.75f, 1f);

    [Header("Typography")]
    [SerializeField] private int titleFontSize = 24;
    [SerializeField] private int labelFontSize = 18;

    private void Awake()
    {
        BuildUi();
    }

    private void BuildUi()
    {
        Canvas canvas = CreateCanvas();
        RectTransform window = CreateWindow(canvas.transform);

        CreateHeader(window);
        CreateInventoryGrid(window);
        CreateHotbar(window);
        CreateFooterHint(window);
    }

    private Canvas CreateCanvas()
    {
        Canvas existingCanvas = GetComponentInChildren<Canvas>();
        if (existingCanvas != null)
        {
            return existingCanvas;
        }

        GameObject canvasObject = new GameObject("MinecraftUI_Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasObject.transform.SetParent(transform, false);

        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280f, 720f);

        return canvas;
    }

    private RectTransform CreateWindow(Transform parent)
    {
        RectTransform window = CreatePanel("Minecraft_Window", parent, windowColor, windowSize);
        window.anchorMin = new Vector2(0.5f, 0.5f);
        window.anchorMax = new Vector2(0.5f, 0.5f);
        window.pivot = new Vector2(0.5f, 0.5f);
        window.anchoredPosition = Vector2.zero;

        RectTransform border = CreatePanel("Border", window, borderColor, windowSize + new Vector2(16f, 16f));
        border.SetAsFirstSibling();

        return window;
    }

    private void CreateHeader(RectTransform window)
    {
        RectTransform header = CreatePanel("Header", window, new Color(0.2f, 0.2f, 0.2f, 1f), new Vector2(windowSize.x - 32f, 64f));
        header.anchorMin = new Vector2(0.5f, 1f);
        header.anchorMax = new Vector2(0.5f, 1f);
        header.pivot = new Vector2(0.5f, 1f);
        header.anchoredPosition = new Vector2(0f, -16f);

        CreateText("Title", header, "Minecraft Inventory", titleFontSize, TextAnchor.MiddleCenter);
    }

    private void CreateInventoryGrid(RectTransform window)
    {
        RectTransform gridRoot = new GameObject("InventoryGrid", typeof(RectTransform)).GetComponent<RectTransform>();
        gridRoot.SetParent(window, false);
        gridRoot.sizeDelta = new Vector2(windowSize.x - 64f, 260f);
        gridRoot.anchorMin = new Vector2(0.5f, 0.5f);
        gridRoot.anchorMax = new Vector2(0.5f, 0.5f);
        gridRoot.pivot = new Vector2(0.5f, 0.5f);
        gridRoot.anchoredPosition = new Vector2(0f, 10f);

        GridLayoutGroup grid = gridRoot.gameObject.AddComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = inventoryColumns;
        grid.cellSize = slotSize;
        grid.spacing = slotSpacing;
        grid.childAlignment = TextAnchor.MiddleCenter;

        int totalSlots = inventoryRows * inventoryColumns;
        for (int i = 0; i < totalSlots; i++)
        {
            RectTransform slot = CreatePanel($"Slot_{i + 1}", gridRoot, slotColor, slotSize);
            RectTransform inner = CreatePanel("Inner", slot, slotInnerColor, slotSize - new Vector2(10f, 10f));
            inner.anchorMin = new Vector2(0.5f, 0.5f);
            inner.anchorMax = new Vector2(0.5f, 0.5f);
            inner.pivot = new Vector2(0.5f, 0.5f);
            inner.anchoredPosition = Vector2.zero;
        }

        CreateText("InventoryLabel", gridRoot, "Inventory", labelFontSize, TextAnchor.UpperLeft, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(-gridRoot.rect.width / 2f + 4f, gridRoot.rect.height / 2f + 24f));
    }

    private void CreateHotbar(RectTransform window)
    {
        RectTransform hotbarRoot = new GameObject("Hotbar", typeof(RectTransform)).GetComponent<RectTransform>();
        hotbarRoot.SetParent(window, false);
        hotbarRoot.sizeDelta = new Vector2(windowSize.x - 64f, 70f);
        hotbarRoot.anchorMin = new Vector2(0.5f, 0f);
        hotbarRoot.anchorMax = new Vector2(0.5f, 0f);
        hotbarRoot.pivot = new Vector2(0.5f, 0f);
        hotbarRoot.anchoredPosition = new Vector2(0f, 24f);

        GridLayoutGroup grid = hotbarRoot.gameObject.AddComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = inventoryColumns;
        grid.cellSize = slotSize;
        grid.spacing = slotSpacing;
        grid.childAlignment = TextAnchor.MiddleCenter;

        for (int i = 0; i < inventoryColumns; i++)
        {
            RectTransform slot = CreatePanel($"HotbarSlot_{i + 1}", hotbarRoot, slotColor, slotSize);
            RectTransform inner = CreatePanel("Inner", slot, slotInnerColor, slotSize - new Vector2(10f, 10f));
            inner.anchorMin = new Vector2(0.5f, 0.5f);
            inner.anchorMax = new Vector2(0.5f, 0.5f);
            inner.pivot = new Vector2(0.5f, 0.5f);
            inner.anchoredPosition = Vector2.zero;
        }

        CreateText("HotbarLabel", hotbarRoot, "Hotbar", labelFontSize, TextAnchor.UpperLeft, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(-hotbarRoot.rect.width / 2f + 4f, hotbarRoot.rect.height / 2f + 20f));
    }

    private void CreateFooterHint(RectTransform window)
    {
        RectTransform footer = new GameObject("Footer", typeof(RectTransform)).GetComponent<RectTransform>();
        footer.SetParent(window, false);
        footer.sizeDelta = new Vector2(windowSize.x - 32f, 40f);
        footer.anchorMin = new Vector2(0.5f, 0f);
        footer.anchorMax = new Vector2(0.5f, 0f);
        footer.pivot = new Vector2(0.5f, 0f);
        footer.anchoredPosition = new Vector2(0f, 8f);

        CreateText("Hint", footer, "Press E to close", labelFontSize, TextAnchor.MiddleCenter);
    }

    private RectTransform CreatePanel(string name, Transform parent, Color color, Vector2 size)
    {
        RectTransform rect = new GameObject(name, typeof(RectTransform), typeof(Image)).GetComponent<RectTransform>();
        rect.SetParent(parent, false);
        rect.sizeDelta = size;
        rect.GetComponent<Image>().color = color;
        return rect;
    }

    private Text CreateText(
        string name,
        Transform parent,
        string content,
        int fontSize,
        TextAnchor alignment,
        Vector2? anchorMin = null,
        Vector2? anchorMax = null,
        Vector2? pivot = null,
        Vector2? anchoredPosition = null)
    {
        GameObject textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
        textObject.transform.SetParent(parent, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin ?? new Vector2(0.5f, 0.5f);
        rect.anchorMax = anchorMax ?? new Vector2(0.5f, 0.5f);
        rect.pivot = pivot ?? new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = anchoredPosition ?? Vector2.zero;
        rect.sizeDelta = new Vector2(300f, 40f);

        Text text = textObject.GetComponent<Text>();
        text.text = content;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = new Color(0.95f, 0.95f, 0.95f, 1f);

        return text;
    }
}
