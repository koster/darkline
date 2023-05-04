using UnityEngine;

public class UILayerItemBase : MonoBehaviour, UILayerItem
{
    public void Show()
    {
        gameObject.SetActive(true);
        BroadcastMessage("OnShowMessage", SendMessageOptions.DontRequireReceiver);
        OnShow();
    }

    protected virtual void OnShow()
    {
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        BroadcastMessage("OnHideMessage", SendMessageOptions.DontRequireReceiver);
        OnHide();
    }

    protected virtual void OnHide()
    {
    }

    public Transform GetTransform()
    {
        return transform;
    }
}