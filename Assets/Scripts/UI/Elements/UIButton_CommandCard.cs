using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_CommandCard : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] protected Image image;
    [SerializeField] protected Button button;

    [Header("Effect")]
    [SerializeField] protected Action action;

    public void SetAction(Action action)
    {
        if (!action)
        {
            gameObject.SetActive(false);
        }
        else
        {
            this.action = action;
            image.sprite = action.GetButtonImage();
            gameObject.SetActive(true);
        }
    }
}
