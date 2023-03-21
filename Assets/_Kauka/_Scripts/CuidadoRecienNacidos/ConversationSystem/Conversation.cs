using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#region notas
/// <summary>
/// Aunque sea editable desde el editor, el problema está en que no puedo introducir el nombr de cada una de ellas en un texto introducido manualmente, solo por programación. 
/// </summary>
/// 

//INTERACCIÓN CON LOS BOTONES
// 1. El Jugador pulsa A para hablar con susana en un trigger
// 2. Emissive start event
// 3. Emissive text in UI (Si es del player, o si es del npc) 
// 4. Pulsar botón continuar
// 5. Emissive exit event
// 6. Response enter event
// 7. Response text to UI
// 8. Pulsar botón continuar

// Siguiente conversación

#endregion
public class Conversation : MonoBehaviour
{
    ConversationManager conversationManager;

    public int ID_Conversation;
    public int ID_Case;
    public int ID_Module;

    public List<ConversationStructure> conversationList;
    public Queue<ConversationStructure> conversationQueue;
    public ConversationPannel conversationPannel;

    protected int indexConversation;
    protected bool UI;

    public string name1;
    public string name2;

    public enum Communicator
    {
        emmisive,               // Player   // NPC
        receive                 // NPC      // NPC
    }

    public ConversationDialogue dialogue;

    [Serializable]
    public struct ConversationDialogue
    {
        public List<ConversationStructure> ListSentences;
    }

    [Serializable]
    public struct ConversationStructure
    {
        public Communicator communicator;
        public string agentName;
        [Multiline]
        public string message;
        public EventConversation eventConversation;
    }

    [Serializable]
    public struct EventConversation
    {
        public UnityEvent emissiveEventStart;
        public UnityEvent emissiveEventEnd;
    }

    public UnityEvent OnCompleteConversation;
    AnimatorConfiguration animConfig;

    public enum TypeConversation
    {
        Conversation,
        Dialogue
    }

    public TypeConversation currentTypeConvers;

    protected virtual void Awake()
    {
        if (CheckUI()) { UI = true; } else {this.enabled = false; }
        animConfig = conversationPannel.GetComponent<AnimatorConfiguration>();     
    }
    protected virtual void Start() 
    {
        SetNames();
        if (GameManager.instance.testing) 
        {
            if (currentTypeConvers == TypeConversation.Conversation)
            {
                conversationPannel.RegisterConversationEvents();
                StartConversation();
            }
            else if(currentTypeConvers == TypeConversation.Dialogue)
            {
                conversationPannel.RegisterDialogueEvents();
                StartDialogue();
            }
           
        }
    }

    int indexDialogue;

    public void StartDialogue()
    {
        conversationPannel.OpenPannel();
        conversationPannel.nameTxt.text = dialogue.ListSentences[0].agentName;
        conversationPannel.emissiveTxt.text = dialogue.ListSentences[0].message;
    }

    public void NextDialogue()
    {
        indexDialogue++;
        if (indexDialogue+1 > dialogue.ListSentences.Count )
        {
            OnCompleteDialogue();
            OnCompleteDialogueEvent.Invoke();
            return;
        }
        SetConversationText(indexDialogue);
    }

    public UnityEvent OnCompleteDialogueEvent;
    public void OnCompleteDialogue()
    {
        conversationPannel.ClosePannel();
    }

    public void SetConversationText(int value)
    {
        conversationPannel.nameTxt.text = dialogue.ListSentences[value].agentName;
        conversationPannel.emissiveTxt.text = dialogue.ListSentences[value].message;
    }
    public virtual void SetNames()
    {

    }


    /// <summary>
    /// Called from trigger "Input A" activation
    /// </summary>
    public void StartConversation()
    {
        indexConversation = -1;
        conversationList = ConversationManager.instance.GetCurrentConversation(CasoClinico.instance.indexCase, ID_Module, ID_Conversation).ListSentences;
        SetConversation(conversationList);

        NextConversation();
        conversationPannel.OpenPannel();
        conversationPannel.currentConversation = this;
    }

    void SetConversation(List<ConversationStructure> conversationList)
    {
        conversationQueue = new Queue<ConversationStructure>();

        for (int i = 0; i < conversationList.Count; i++)
        {
            conversationQueue.Enqueue(conversationList[i]);
        }
    }
    public void NextConversation()
    {
        indexConversation++; 

        if (indexConversation >= conversationList.Count || conversationQueue.Count == 0)
        {
            Debug.Log("Conversation has ended with Sucess");
            OnComplete();
            return;
        }

        ConversationStructure newConversation = ExtractConversationfromQueue();

        EmmissiveMessage();
        SetUI(newConversation);
    }
    ConversationStructure ExtractConversationfromQueue()
    {
         return conversationQueue.Dequeue();
    }
    public void SetUI(ConversationStructure conversation)
    {
        if (!UI) {return;}
       
        if (conversation.communicator == Communicator.emmisive) { conversationPannel.nameTxt.text = name1; } else{ conversationPannel.nameTxt.text = name2; }
        
        conversationPannel.emissiveTxt.text = conversation.message;
    }

    public void EmmissiveMessage()
    {
        conversationList[indexConversation].eventConversation.emissiveEventStart.Invoke();
    }

    void OnComplete()
    {
        OnCompleteConversation.Invoke();
        conversationPannel.ClosePannel();
        Debug.Log("OnComplete");
    }

    bool CheckUI()
    {
        if (conversationPannel.nameTxt == null || conversationPannel.emissiveTxt == null)
        {
            Debug.LogError("No has configurado la UI, añade los textos de TMP");
            this.enabled = false;
            return false;
        }
        else
        {
            return true;
        }
    }
}
