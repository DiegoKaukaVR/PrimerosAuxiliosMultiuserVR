using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsControllerPersonalizado : MonoBehaviour
{
    // no tocar bool showController si no se enseña controlador
    public bool notShowControllers;
    public InputDeviceCharacteristics controlCharacteristics;
    public List<GameObject> controllersPrefabs;
    public GameObject handModelPrefab;

    InputDevice thisDevice;
    // si se tiene que enseñar el controlador (variable no molesta)
    GameObject spawnedController;
    GameObject spawnedHand;

    Animator handAnim;
    public bool hasAnimator = true;

    public void ChangeHands()
    {
        spawnedHand.gameObject.SetActive(false);
        spawnedHand = null;
        cogerDevice();

    }
    public void cogerDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // manera mas larga y precisa de coger control de un device
        InputDevices.GetDevicesWithCharacteristics(controlCharacteristics, devices);

        if (devices.Count > 0)
        {
            thisDevice = devices[0];
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

            if (spawnedHand.GetComponent<Animator>())
            {
                handAnim = spawnedHand.GetComponent<Animator>();
                if (handAnim == null)
                {
                    hasAnimator = false;
                }
            }
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
            if (notShowControllers)
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
    }



    void controlMano()
    {
        if (!hasAnimator)
        {
            return;
        }
        if (thisDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnim.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnim.SetFloat("Grip", 0);
        }
        if (thisDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnim.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnim.SetFloat("Trigger", 0);
        }

        //if (DatosImportantes.pellizcarAvaible)
        //{
        //    if (thisDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryValue))
        //    {
        //        if (primaryValue)
        //        {
        //            handAnim.SetBool("Pellizcar", true);
        //        }
        //        else
        //        {
        //            handAnim.SetBool("Pellizcar", false);
        //        }
        //    }
        //}
        
        
    }
}
