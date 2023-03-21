using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class that is used to control a collection of objects that must be completed.
/// </summary>
public class GrabCollection : MonoBehaviour
{
    [SerializeField, Min(0)]
    int maxSlots = 4;

    [SerializeField, Min(0)]
    int currentSlots;

    [SerializeField]
    List<MyGrabObject> SlotsList = new List<MyGrabObject>();

    bool[] boolSlots;
    [SerializeField]
    MyGrabObject[] CollectionArr;

    [Header("Transform")]
    public Transform[] transformsCollection;

    public UnityEvent onCompletedTask;

    [SerializeField] GameObject GrabsItems;

    private void Start()
    {
        currentSlots = 0;
        GetGrabObjects();
        SetDebug(false);
    }

    private void GetGrabObjects()
    {
        //CollectionArr = GetComponentsInChildren<MyGrabObject>();
        AuxiliarSetUp();
        NormalSetUp();
        maxSlots = CollectionArr.Length;
        boolSlots = new bool[maxSlots];
    }

    void NormalSetUp()
    {
        for (int i = 0; i < CollectionArr.Length; i++)
        {
            CollectionArr[i].id = i;
        }
    }

    void AuxiliarSetUp()
    {
        if (CollectionArr.Length == 0)
        {

            CollectionArr = GrabsItems.GetComponentsInChildren<MyGrabObject>();
            for (int i = 0; i < maxSlots; i++)
            {
                CollectionArr[i].grabCollection = this;
                CollectionArr[i].id = i;
            }
        }
    }

    public GameObject debug1;
    public GameObject debug2;

    void SetDebug(bool value)
    {
        if (debug1 != null && debug2 != null)
        {
            debug1.SetActive(value);
            debug2.SetActive(value);
        }
    }

    public void StartLogic()
    {
        SetAvaibleInteraction();

        SetDebug(true);

    }
    public void SetAvaibleInteraction()
    {
        for (int i = 0; i < CollectionArr.Length; i++)
        {
            CollectionArr[i].enabled = true;
            CollectionArr[i].grabActivated = true;
        }
    }
    public void AddObjectToCollection(MyGrabObject objectInteractuable, int indexArrayTransform)
    {
        if (objectInteractuable == null) return;

        for (int i = 0; i < SlotsList.Count; i++)
        {
            if (objectInteractuable == SlotsList[i])
            {
                return;
            }
        }

       
        if (boolSlots[indexArrayTransform] == true)
        {
            Debug.Log("Slot Ocupado");
            return;
        }
        else
        {
            boolSlots[indexArrayTransform] = true;
        }
      

        SnapObjectToBody(objectInteractuable, indexArrayTransform);
        GameManager.instance.audioManager.PlayComplete();

        SlotsList.Add(objectInteractuable);
        objectInteractuable.enabled = false;
        currentSlots++;
        CheckSlots();
    }

    Vector3 prevScale;
    Rigidbody rb;

    void SnapObjectToBody(MyGrabObject objectInteractuable, int indexTransform)
    {

        objectInteractuable.grabActivated = false;
        prevScale = objectInteractuable.transform.lossyScale;
        objectInteractuable.transform.position = transformsCollection[indexTransform].position;
        objectInteractuable.transform.up = transformsCollection[indexTransform].forward;
        objectInteractuable.transform.parent = transformsCollection[indexTransform];
        objectInteractuable.transform.localScale = prevScale;
        objectInteractuable.GetComponent<Rigidbody>().isKinematic = true;
    }
    bool CheckSlots()
    {
        if (currentSlots >= maxSlots)
        {
            OnComplete();
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnComplete() 
    {
        onCompletedTask.Invoke();
        this.enabled = false;
        
        Debug.Log("Todos los slots completados, prueba superada");
    }

    private void OnValidate()
    {
        if (maxSlots == 0)
            GetGrabObjects();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .25f);

        if (transformsCollection.Length == 0)
        {
            return;
        }
        for (int i = 0; i < maxSlots; i++)
        {
            if (transformsCollection[i] == null)
            {
                break;
            }
            Gizmos.DrawCube(transformsCollection[i].position, Vector3.one * .1f);
    
            Gizmos.DrawLine(transformsCollection[i].position, transformsCollection[i].position + transformsCollection[i].forward * .2f);
        }
    }
}
