using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
//[ExecuteAlways]
public class SafeAreaUIAdapt : MonoBehaviour
{
    private static CanvasScaler scaler;
    private RectTransform rectTransform;

    private static void Init(CanvasScaler scaler)
    {
        SafeAreaUIAdapt.scaler = scaler;
    }

    private void Awake()
    {
        SafeAreaUIAdapt.Init(GameObject.FindObjectOfType<CanvasScaler>());
        rectTransform = GetComponent<RectTransform>();
        Adapt();
    }

    private void Update()
    {
        Adapt();
    }

    public void Adapt()
    {
        if (scaler == null)
            return;

        var safeArea = Screen.safeArea;

        int width = (int)(scaler.referenceResolution.x * (1 - scaler.matchWidthOrHeight) +
            scaler.referenceResolution.y * Screen.width / Screen.height * scaler.matchWidthOrHeight);

        int height = (int)(scaler.referenceResolution.y * scaler.matchWidthOrHeight -
            scaler.referenceResolution.x * Screen.height / Screen.width * (scaler.matchWidthOrHeight-1));

        float ratio = scaler.referenceResolution.y * scaler.matchWidthOrHeight/
            Screen.height - scaler.referenceResolution.x * (scaler.matchWidthOrHeight - 1)/Screen.width;

        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        rectTransform.offsetMin = new Vector2(safeArea.position.x*ratio, safeArea.position.y*ratio);
        rectTransform.offsetMax = new Vector2(safeArea.position.x * ratio + safeArea.width * ratio - width,
            -(height - safeArea.position.y * ratio - safeArea.height * ratio));
    }
}
