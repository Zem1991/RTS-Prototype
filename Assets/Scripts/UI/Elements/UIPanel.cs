using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Self references")]
    [SerializeField] protected Image backgroundImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        InputManager.Instance.EnterPanelUnderCursor(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputManager.Instance.ExitPanelUnderCursor(this);
    }
}
