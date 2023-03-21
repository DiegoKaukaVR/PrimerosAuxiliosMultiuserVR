using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class controlAnimManos : MonoBehaviour
{
    public XRController xRController;

    // no tocar bool showController si no se enseña controlador
    public bool showController;
    public InputDeviceCharacteristics controlCharacteristics;
    public List<GameObject> controllersPrefabs;
    public GameObject handModelPrefab;

    InputDevice thisDevice;
    // si se tiene que enseñar el controlador (variable no molesta)
    GameObject spawnedController;
    GameObject spawnedHand;
    Animator handAnim;

    //bool para touch animation
    bool touchA;
    bool touchB;
    bool touchThumb;


    //check objeto siendo grabbed
    public XRDirectInteractor handInteractor;

    SkinnedMeshRenderer manoDerSkin;
    SkinnedMeshRenderer manoIzqSkin;



    private void Start()
    {
        cogerDevice();
        handInteractor = transform.parent.transform.parent.GetComponent<XRDirectInteractor>();
        xRController = transform.parent.transform.parent.GetComponent<XRController>();
    }


    void cogerDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // manera mas larga y precisa de coger control de un device
        InputDevices.GetDevicesWithCharacteristics(controlCharacteristics, devices);

        if (devices.Count > 0)
        {
            thisDevice = devices[0];
            //Debug.Log(thisDevice.name);
            #region siEnseñarControl
            //GameObject prefab = controllersPrefabs.Find(controller => controller.name == thisDevice.name);
            //if (prefab)
            //{
            //    spawnedController = Instantiate(prefab, transform);
            //}
            //else
            //{
            //    spawnedController = Instantiate(controllersPrefabs[0], transform);
            //}
            #endregion siEnseñarControl
            spawnedHand = Instantiate(handModelPrefab, transform);
            handAnim = spawnedHand.GetComponent<Animator>();
        }
    }
    private void Update()
    {
        if (!thisDevice.isValid)
        {
            cogerDevice();
        }
        else
        {
            if (showController)
            {
                spawnedHand.SetActive(false);
                // si se necesita enseñar el controlador descomentar
                //spawnedController.SetActive(true);
            }
            else
            {
                spawnedHand.SetActive(true);
                // si se necesita enseñar el controlador descomentar
                //spawnedController.SetActive(false);
                controlMano();
            }
        }
        cogerSkinnedMeshManos();

        //activar interactors de las manos si el menu usuario esta abierto

    }
    void cogerSkinnedMeshManos()
    {
        if (manoDerSkin == null)
        {
            if (transform.Find("Mano_Der(Clone)"))
            {
                //Debug.Log("CogiendoSkin");
                manoDerSkin = transform.Find("Mano_Der(Clone)").transform.Find("polySurface1137").gameObject.GetComponent<SkinnedMeshRenderer>();
            }
        }
        if (manoIzqSkin == null)
        {
            if (transform.Find("Mano_Izq(Clone)"))
            {
                //Debug.Log("CogiendoSkin");
                manoIzqSkin = transform.Find("Mano_Izq(Clone)").transform.Find("polySurface1137").gameObject.GetComponent<SkinnedMeshRenderer>();
            }
        }
    }
    void controlMano()
    {
        if (thisDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            if (handAnim != null)
            {
                // animation
                handAnim.SetFloat("Grip", gripValue);
            }
            if (gripValue > 0.7f)
            {
                xRController.selectUsage = InputHelpers.Button.Grip;
            }
        }
        else
        {
            if (handAnim != null)
            {
                // animation
                handAnim.SetFloat("Grip", 0);
            }
        }
        if (thisDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (handAnim != null)
            {
                // animation
                handAnim.SetFloat("Trigger", triggerValue);
            }
            if (triggerValue > 0.7f)
            {
                xRController.selectUsage = InputHelpers.Button.Trigger;
            }
            else
            {
                if (handAnim != null)
                {
                    // animation
                    handAnim.SetFloat("Trigger", 0);
                }
            }
            if (thisDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryBool) && primaryBool)
            {

            }

            #region touch animation
            if (thisDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out touchA))
            {
                if (handAnim != null)
                {
                    if (touchA)
                    {
                        if (!touchB && !touchThumb)
                        {
                            handAnim.SetBool("Touch", true);
                        }
                    }
                    else if (!touchA && !touchB && !touchThumb)
                    {
                        handAnim.SetBool("Touch", false);
                    }
                }
            }

            if (thisDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out touchB) && touchB)
            {
                if (handAnim != null)
                {
                    if (touchB)
                    {
                        if (!touchA && !touchThumb)
                        {
                            handAnim.SetBool("Touch", true);
                        }
                    }
                    else if (!touchA && !touchB && !touchThumb)
                    {
                        handAnim.SetBool("Touch", false);
                    }
                }
            }

            if (thisDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out touchThumb) && touchThumb)
            {
                if (handAnim != null)
                {
                    if (touchThumb)
                    {
                        if (!touchA && !touchB)
                        {
                            handAnim.SetBool("Touch", true);
                        }
                    }
                    else if (!touchA && !touchB && !touchThumb)
                    {
                        handAnim.SetBool("Touch", false);
                    }
                }
            }
            #endregion
        }
    }
}
