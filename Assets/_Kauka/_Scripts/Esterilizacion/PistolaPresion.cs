using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System.Linq;

public class PistolaPresion : MonoBehaviour
{
    public GameObject agua;
    public GameObject aire;
    public GameObject pegatina;

    public bool esAgua;
    public bool esAire;
    public bool esPegatina;
    bool cogiendo; 
    XRNode xrNode;
    protected List<InputDevice> devices = new List<InputDevice>();
    protected InputDevice device;
    XRBaseInteractable xrbaseint;
    public bool triggerM;
    public bool puedePegatina;
    // Start is called before the first frame update
    void Start()
    {
        if (esAgua && !esAire)
        {
            ActivarAgua(false);
        }
        else if (esAire && !esAgua)
        {
            ActivarAire(false);
        }
        puedePegatina = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cogiendo)
        {
            CogerPistola();
        }
    }
    public void CogerPistola()
    {
        cogiendo = true;
        GetDevice();
       
        //device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerM);
        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerM);
        

        if (triggerM)
        {
            if (esAgua && !esAire)
            {
                ActivarAgua(true);
            }
            else if (esAire && !esAgua)
            {
                ActivarAire(true);
            }
            else if (esPegatina && puedePegatina)
            {

                
            }
        }
        else
        {
            if (esAgua && !esAire)
            {
                ActivarAgua(false);
            }
            else if (esAire && !esAgua)
            {
                ActivarAire(false);
            }
            puedePegatina = true;
        }
    }
    public void SoltarPistola()
    {
        cogiendo = false;
        if (esAgua && !esAire)
        {
            ActivarAgua(false);
        }
        else if (esAire && !esAgua)
        {
            ActivarAire(false);
        }

    }

    public void ActivarAgua(bool activo)
    {
        if (activo)
        {
            agua.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            agua.GetComponent<ParticleSystem>().Stop();
        }
    }
    public void ActivarAire(bool activo)
    {
        if (activo)
        {
            GameControllerEsterilizacion.Instance.utilizandoPistolaPresionSecado = true;
            GameControllerEsterilizacion.Instance.CheckConditions();
            aire.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            aire.GetComponent<ParticleSystem>().Stop();
            GameControllerEsterilizacion.Instance.utilizandoPistolaPresionSecado = false;
        }
    }
    public void GetDevice()
    {
        if (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.name == "LeftHand") { xrNode = XRNode.LeftHand; }
        if (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.name == "RightHand") { xrNode = XRNode.RightHand; }
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }
}
