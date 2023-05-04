using TMPro;
using UnityEngine;

public class FloatingText : UILayerItemBase
{
    static FloatingText i;
    
    public TextMeshProUGUI label;

    void Awake()
    {
        if (i == null)
        {
            i = this;
            Hide();
        }
    }

    public static void ShowWorldPos(Vector3 worldPos, string text)
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        var newText = UILayer.Instantiate(i);
        var halfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        newText.transform.localPosition = screenPoint - halfScreen;
        newText.label.text = text;
        newText.Show();
    }

    public static void Show(Vector3 canvasPos, string text)
    {
        var newText = UILayer.Instantiate(i);
        newText.transform.position = canvasPos;
        newText.label.text = text;
        newText.Show();
    }

    void Update()
    {
        if (i == this)
            return;

        transform.position += Vector3.up * Time.deltaTime;
        label.alpha -= Time.deltaTime;

        if (label.alpha <= 0)
            Destroy(gameObject);
    }
}