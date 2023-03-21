using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ErrorManager {


    public static void Error(string script, string metodo, string msg)
    {
        Debug.LogError("Script: " + script + " |  Method: " + metodo + " |  Message: " + msg);
    }

    public static void Warning(string script, string metodo, string msg, bool show)
    {
        if(show)
            Debug.LogWarning("Script: " + script + " |  Method: " + metodo + " |  Message: " + msg);
    }

    public static void Log(string script, string metodo, string msg, bool show)
    {
        if(show)
            Debug.Log("Script: " + script + " |  Method: " + metodo + " |  Message: " + msg);
    }
}
