using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalCheck : SimpleProcess
{
    public UnityEvent NoNeedEvent;
    public void InitializeGlobalCheck()
    {
        if (NeedGlobalCheck())
        {
            StartProcess();
        }
        else
        {
            NoNeedEvent.Invoke();
        }
    }

    //COMPROBAR SI TIENE QUE HACER GLOBALCHECK
    public bool NeedGlobalCheck()
    {
        if (CasoClinico.instance.currentData.dataBebe.needCheckeo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  


}
