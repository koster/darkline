using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class WrittenTextHandle
{
    public UnityAction onComplete;
}

public class WrittenText
{
    public string text;
    public bool skipInput;
    public bool dontHide = true;
}

public class NarrativeBoxUI : UILayerItemBase
{
    public static NarrativeBoxUI i;

    public TextMeshProUGUI label;
    public GameObject skipHint;

    public float charCooldown = 0.2f;
    public float defaultWait = 1f;

    public CanvasGroup canvasGroup;
    
    bool isWriting;

    bool skip;

    WrittenText textContext;

    void Awake()
    {
        i = this;
        label.text = "";
        Hide();
    }

    public static WrittenTextHandle Write(string text)
    {
        var writtenTextHandle = i.WriteText(new WrittenText { text = text });
        return writtenTextHandle;
    }

    public static WrittenTextHandle Write(WrittenText text)
    {
        var writtenTextHandle = i.WriteText(text);
        return writtenTextHandle;
    }

    WrittenTextHandle WriteText(WrittenText text)
    {
        i.Show();
        var twxt = new WrittenTextHandle();
        StartCoroutine(WriteTextCoroutine(twxt, text));
        return twxt;
    }

    IEnumerator WriteTextCoroutine(WrittenTextHandle handle, WrittenText writtenText)
    {
        isWriting = true;
        
        textContext = writtenText;

        skip = false;
        label.text = "";
        skipHint.SetActive(false);

        UILayer.PushOnTop(i);

        yield return PrintTextLetterByLetter();
        yield return SkippableDefaultWait();
        yield return WaitForFinalInput();

        handle.onComplete?.Invoke();
        
        
        isWriting = false;
    }

    IEnumerator WaitForFinalInput()
    {
        if (!textContext.skipInput)
        {
            skipHint.SetActive(true);
            skip = false;
            while (!skip)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator SkippableDefaultWait()
    {
        float wait = 0f;
        while (wait < defaultWait)
        {
            wait += Time.deltaTime;
            if (!skip)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    int everyOther;
    
    IEnumerator PrintTextLetterByLetter()
    {
        for (var i = 0; i < textContext.text.Length; i++)
        {
            label.text += textContext.text[i];
            if (!skip)
            {
                if (everyOther % 2 == 0)
                    textContext.text.GetCharacter().voice.PlayClip(0.25f);
                everyOther++;

                yield return new WaitForSeconds(charCooldown);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S))
        {
            skip = true;
        }

        var rectTransform = GetComponent<RectTransform>();
        var anchoredPosition = rectTransform.anchoredPosition;
        if (StatPanelUI.IsVisible())
        {
            anchoredPosition.y = -226;
        }
        else
        {
            anchoredPosition.y = -126;
        }
        rectTransform.anchoredPosition = anchoredPosition;

        var shouldShowBox = isWriting;
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, shouldShowBox ? 1f : 0f, Time.fixedDeltaTime);
    }

    public static void Close()
    {
        i.gameObject.SetActive(false);
        i.canvasGroup.alpha = 0;
    }

    public static void Restore()
    {
        i.gameObject.SetActive(true);
    }

    public static bool IsWriting()
    {
        return i.isWriting;
    }
}