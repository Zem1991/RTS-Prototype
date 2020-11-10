using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_SelectionDetails_Single : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private Image portrait;
    //[SerializeField] private Text healthPointsText;
    //[SerializeField] private Text manaPointsText;

    [Header("Descriptions")]
    [SerializeField] private Text actorName;
    //[SerializeField] private Text actorDescription;
    
    public void UpdatePanel(Actor actor)
    {
        portrait.sprite = actor.GetPortraitImage();
        actorName.text = actor.GetGameName();
    }
}
