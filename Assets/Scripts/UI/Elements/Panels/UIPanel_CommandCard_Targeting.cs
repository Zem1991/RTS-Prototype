using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_CommandCard_Targeting : MonoBehaviour
{
    [SerializeField] private Image actionImage;
    [SerializeField] private Text helpText;
    [SerializeField] private UIButton_CommandCard cancelBtn;

    public void UpdatePanel(Action action)
    {
        actionImage.sprite = action.GetButtonImage();
        //TODO: more stuff
    }
}
