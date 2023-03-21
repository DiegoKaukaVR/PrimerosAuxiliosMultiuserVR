using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public List<ConversationData> CaseConversations;
    public static ConversationManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Returns the conversation structure to introduce in UI
    /// </summary>
    public Conversation.ConversationStructure GetCurrentSentence(int CaseIndex, int ModuleIndex, int indexConversation, int indexSentece)
    {
        return CaseConversations[CaseIndex].ModuleConversations[ModuleIndex].listConversations[indexConversation].ListSentences[indexSentece];
    }

    public Conversation.ConversationDialogue GetCurrentConversation(int CaseIndex, int ModuleIndex, int indexConversation)
    {
        return CaseConversations[CaseIndex].ModuleConversations[ModuleIndex].listConversations[indexConversation];
    }
}
