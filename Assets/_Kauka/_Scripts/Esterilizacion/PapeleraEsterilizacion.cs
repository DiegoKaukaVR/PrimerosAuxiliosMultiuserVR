using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapeleraEsterilizacion : MonoBehaviour
{
    public SlotsBandeja bandeja;
    int objetosDefectuosos;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        objetosDefectuosos = bandeja.numObjetosDefectuosos;
        img.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HerramientaEsterilizacion" && other.GetComponent<HerramientaBandeja>())
        {
            if (other.GetComponent<HerramientaBandeja>().objetoDefectuoso)
            {
                objetosDefectuosos--;
                other.GetComponent<Collider>().enabled = false;
                Destroy(other.gameObject);
                if(objetosDefectuosos <= 0)
                {
                    img.color = Color.green;
                    GameControllerEsterilizacion.Instance.materialDeshechado = true;
                    GameControllerEsterilizacion.Instance.CheckConditions();
                }
            }
        }
    }
}
