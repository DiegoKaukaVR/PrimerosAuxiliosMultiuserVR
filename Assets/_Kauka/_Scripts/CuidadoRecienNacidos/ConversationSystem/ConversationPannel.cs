using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConversationPannel : MonoBehaviour
{
    public Conversation currentConversation;
    [SerializeField] Button buttonContinue;
    AnimatorConfiguration animatorConfiguration;

    [SerializeField] public TextMeshProUGUI nameTxt;
    [SerializeField] public TextMeshProUGUI emissiveTxt;

    private void Awake()
    {
       
        animatorConfiguration = GetComponent<AnimatorConfiguration>();
    }

    public void RegisterDialogueEvents()
    {
        buttonContinue.onClick.AddListener(currentConversation.NextDialogue);
    }

    public void RegisterConversationEvents()
    {
        buttonContinue.onClick.AddListener(currentConversation.NextConversation);
    }

    public void ClearAllEvents()
    {
        buttonContinue.onClick.RemoveAllListeners();
    }

    public void OpenPannel()
    {
        if (animatorConfiguration == null)
        {
            animatorConfiguration = GetComponent<AnimatorConfiguration>();
        }
        animatorConfiguration.SetBoolTrue("open");
       
        
    }

    public void ClosePannel()
    {
        if (animatorConfiguration == null)
        {
            animatorConfiguration = GetComponent<AnimatorConfiguration>();
        }
        animatorConfiguration.SetBoolFalse("open");
    }

 
}
