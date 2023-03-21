using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversacionRecienNacidos : Conversation
{
    public override void SetNames()
    {
        name1 = "TCAE";
        name2 = CasoClinico.instance.currentData.dataMadre.nameMother;
    }

}
