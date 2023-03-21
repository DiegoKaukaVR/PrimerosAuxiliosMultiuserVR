using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConversationData : MonoBehaviour
{
    public List<ConversationModule> ModuleConversations;

    [Serializable]
    public struct ConversationModule
    {
        public List<Conversation.ConversationDialogue> listConversations;
    }

   
}
