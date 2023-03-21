using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Caso : MonoBehaviour
{
    public DataCase dataPaciente;
    [Serializable]
    public struct DataCase
    {
        [Multiline]
        public string Description;
        public string Name;
        public int age;
    }

}
