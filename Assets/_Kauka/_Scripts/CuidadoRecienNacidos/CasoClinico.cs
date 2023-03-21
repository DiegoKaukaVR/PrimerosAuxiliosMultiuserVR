using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CasoClinico : MonoBehaviour
{
    public static CasoClinico instance;

    public string userName = "Dioego";
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

        Initialize();
    }

    public DataCaso[] allData;
    public DataCaso currentData;
    public int indexCase;

    [SerializeField] bool testing = true;

    ///SCENE CONFIGURATION
    [Header("Scene Configuration")]
    public GameObject[] bebePrefabArray = new GameObject[3];

    [SerializeField] Transform cuna;
    [SerializeField] Transform rotationDummyCuna;

    [SerializeField] Transform incubadora;
    [SerializeField] Transform rotationDummyIncubadora;

    // Se llama desde la UI de usuario para seleccionar el caso
    public void SelectCurrentCase(int index)
    {
        currentData = allData[index];
    }

    private void Initialize()
    {
        allData = GetComponentsInChildren<DataCaso>();

        if (testing)
        {
            //// TESTING
            SelectCurrentCase(indexCase);
            currentData = allData[0];
        }

        for (int i = 0; i < CasoClinico.instance.allData.Length; i++)
        {
            if (CheckCase(i))
            {
                if (currentData.dataBebe.incubadora)
                {
                    bebePrefabArray[i].transform.position = incubadora.transform.position;
                    bebePrefabArray[i].transform.rotation = rotationDummyIncubadora.transform.rotation;
                }
                else
                {
                    bebePrefabArray[i].transform.position = cuna.transform.position;
                    bebePrefabArray[i].transform.rotation = rotationDummyCuna.transform.rotation;
                }
            }
        }
    }

    bool CheckCase(int i)
    {
        indexCase = i;
        if (CasoClinico.instance.currentData == CasoClinico.instance.allData[i])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
