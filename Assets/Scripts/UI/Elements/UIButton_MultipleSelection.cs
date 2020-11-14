using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_MultipleSelection : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] protected Image image;
    [SerializeField] protected Button button;

    [Header("More UI")]
    [SerializeField] protected Image profileImage;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Image healthFill;
    [SerializeField] protected Image manaBar;
    [SerializeField] protected Image manaFill;

    [Header("Effect")]
    [SerializeField] protected Actor actor;

    public void SetActor(Actor actor)
    {
        if (!actor)
        {
            gameObject.SetActive(false);
        }
        else
        {
            this.actor = actor;
            image.sprite = null;
            profileImage.sprite = actor.GetButtonImage();
            gameObject.SetActive(true);
        }
    }

    public void OnClick()
    {
        List<Actor> actorList = new List<Actor>();
        actorList.Add(actor);
        UIManager um = UIManager.Instance;
        um.MultipleSelectionButton(actorList);
    }
}
