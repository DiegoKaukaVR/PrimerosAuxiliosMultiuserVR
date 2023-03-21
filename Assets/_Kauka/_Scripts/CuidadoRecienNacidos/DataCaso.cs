using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataCaso : MonoBehaviour
{
   
    public string descriptionCase;
    public DataBebe dataBebe;
    public DataMadre dataMadre;

    [Serializable]
    public struct DataBebe
    {
        public string name;

        public float temperature;

        public float frecuenciaCardiaca;
        public float frecuenciaRespiratoria;
        public float tensionArterial;
        public float oxigeno;

        public float peso;
        public float talla;
        public float tallacabeza;

        public bool needCheckeo;
        public bool hasVia;
        public bool incubadora;
    }

    [Serializable]
    public struct DataMadre
    {
        public string nameMother;
        public bool hasProblem;
    }

    public ConversationData conversationData;
    private void Start()
    {
        conversationData = GetComponent<ConversationData>();
    }



}
