using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System.Linq;

public class Cepillo : MonoBehaviour
{
    public bool cepilloTubular;
    public bool cepilloNormal;
    public GameObject colliderCepillo;

    Transform parentObj;
    Vector3 originalScale;
    bool puedeLimpiar;
    bool interactuando;
    ConfigurableJoint cjCC;
    bool jointCreado;
    System.Reflection.FieldInfo[] fields;
    XRGrabInteractable grabCC;
    Vector3 ejeInicial;
    
    XRNode xrNode;
    protected List<InputDevice> devices = new List<InputDevice>();
    protected InputDevice device;
    bool triggerM;
    bool puedeCogerInput;
    // Start is called before the first frame update
    void Start()
    {
        puedeCogerInput = true;
        cjCC = GetComponent<ConfigurableJoint>();
        grabCC = GetComponent<XRGrabInteractable>();
        originalScale = transform.localScale;
        colliderCepillo.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeLimpiar)
        {
            if (puedeCogerInput)
            {
                GetDevice();
                puedeCogerInput = false;
            }
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerM);
            if (triggerM && interactuando && !jointCreado)
            {
                AdherirAHerramienta();
            }

            if (jointCreado && interactuando)
            {
                transform.SetParent(parentObj);
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                
                if (!triggerM)
                {
                    SepararHerramienta();
                }
            }
        }
    }

    public void AdherirAHerramienta()
    {
        colliderCepillo.GetComponent<Collider>().enabled = true;
        grabCC.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        grabCC.trackRotation = false;
        grabCC.smoothPosition = true;
        cjCC.connectedBody = parentObj.GetComponent<Rigidbody>();
        cjCC.angularXMotion = ConfigurableJointMotion.Locked;
        cjCC.angularYMotion = ConfigurableJointMotion.Locked;
        cjCC.angularZMotion = ConfigurableJointMotion.Locked;
        cjCC.zMotion = ConfigurableJointMotion.Locked;
        cjCC.yMotion = ConfigurableJointMotion.Limited;
        cjCC.xMotion = ConfigurableJointMotion.Locked;
        jointCreado = true;
    }
    public void Soltar()
    {
        puedeCogerInput = true;
        interactuando = false;
        SepararHerramienta();
    }
    public void SepararHerramienta()
    {
        puedeLimpiar = false;
        colliderCepillo.GetComponent<Collider>().enabled = false;
        transform.parent = null;
        grabCC.movementType = XRBaseInteractable.MovementType.Instantaneous;
        grabCC.trackRotation = true;
        grabCC.smoothPosition = false;
        cjCC.connectedBody = null;
        cjCC.angularXMotion = ConfigurableJointMotion.Free;
        cjCC.angularYMotion = ConfigurableJointMotion.Free;
        cjCC.angularZMotion = ConfigurableJointMotion.Free;
        cjCC.zMotion = ConfigurableJointMotion.Free;
        cjCC.yMotion = ConfigurableJointMotion.Free;
        cjCC.xMotion = ConfigurableJointMotion.Free;

        jointCreado = false;
    }

    public void Interactuar()
    {
        interactuando = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((other.tag == "ContenedorCepilloTubular" && cepilloTubular) || (other.tag == "ContenedorCepilloNormal" && cepilloNormal)) && interactuando)
        {
            print("limpiando " + this.gameObject.name);
            puedeLimpiar = true;
            parentObj = other.transform;
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
