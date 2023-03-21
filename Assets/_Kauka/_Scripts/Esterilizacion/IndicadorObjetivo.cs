using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicadorObjetivo : MonoBehaviour
{
    Camera cam; //Camera to use
    public GameObject objetoDestino;
    Vector3 target;//Target to point at (you could set this to any gameObject dynamically)
    public float offsetBordes;
    public float alturaAlPunto = 0.5f;
    private Vector3 targetPos; //Target position on screen
    private Vector3 screenMiddle; //Middle of the screen
    bool isInScreen;
    bool subiendo;
    Vector3 direccionALaCamara;
    public float minSize;
    public float maxSize;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        target = objetoDestino.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        IsVisible();
        if (isInScreen)
        {
            GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_FaceColor", Color.green);
            transform.position = target + new Vector3(0, alturaAlPunto, 0);
            transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y + 90, 0);
            transform.localScale = new Vector3(maxSize,maxSize,maxSize);
            StartCoroutine(SubirBajar());
        }
        else
        {
            StopCoroutine(SubirBajar());
            GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_FaceColor", Color.red);


            transform.localScale = new Vector3(minSize, minSize, minSize);


            Vector3 cappedTargetScreenPosition = cam.WorldToViewportPoint(target);

            direccionALaCamara = (target - cam.transform.position);

            Quaternion rotDeseadaD = Quaternion.Euler(0, 90, 0);
            Quaternion rotDeseadaI = Quaternion.Euler(0, -90, 0);
            Vector3 vectorDerecho =  rotDeseadaD * direccionALaCamara;
            Vector3 vectorIzquierdo = rotDeseadaI * direccionALaCamara;
           
            if (Vector3.Angle(cam.transform.forward, vectorDerecho) < Vector3.Angle(cam.transform.forward, vectorIzquierdo))
            {
                float distancia = Mathf.Lerp(Screen.width/2,0, Vector3.Angle(cam.transform.forward, vectorDerecho)/45);
                cappedTargetScreenPosition.x = Screen.width - (Screen.width * offsetBordes)-distancia;
                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width - (Screen.width * offsetBordes), Screen.width * offsetBordes);
                //transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y+180, -90);
                transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + 90, cam.transform.eulerAngles.y, -90);
                GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_FaceColor", Color.red);
            }
            else
            {
                float distancia = Mathf.Lerp(Screen.width / 2, 0, Vector3.Angle(cam.transform.forward, vectorIzquierdo) / 45);
                cappedTargetScreenPosition.x = (Screen.width * offsetBordes) + distancia;
                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width - (Screen.width * offsetBordes), Screen.width * offsetBordes);
                //transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y +180, +90);
                transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x - 90, cam.transform.eulerAngles.y , +90);
                GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_FaceColor", Color.red);
            }
            cappedTargetScreenPosition.y = Mathf.Lerp(Screen.height*0.7f ,(Screen.height / 2) * offsetBordes, Vector3.Angle(target - cam.transform.position, cam.transform.forward)/120);
            cappedTargetScreenPosition.z = 1;
            Vector3 pointerWorldPos = cam.ScreenToWorldPoint(cappedTargetScreenPosition);

            transform.position = cam.ScreenToWorldPoint(cappedTargetScreenPosition);
            //transform.rotation = cam.transform.rotation;
        }
        
    }

    void IsVisible()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(target);

        if ((screenPoint.x > 0.25f && screenPoint.x < 0.75f) && (Vector3.Angle(target - cam.transform.position, cam.transform.forward) < 90))
        {
            isInScreen = true;
        }
        else isInScreen = false;
    }

    IEnumerator SubirBajar()
    {
        float t = 0;
        Vector3 posInicial = transform.position;
        if (!subiendo)
        {
            while (t < 1 && isInScreen)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(posInicial, posInicial + new Vector3(0, 0.1f, 0), t / 1);
                yield return null;
            }
            subiendo = true;
        }
        else
        {
            while (t < 1 && isInScreen)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(posInicial, posInicial - new Vector3(0, 0.1f, 0), t / 1);
                yield return null;
            }
            subiendo = false;
        }
        if (isInScreen)
        {
            StartCoroutine(SubirBajar());
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Vector3 direccionObj = target.position - Camera.main.transform.localPosition;
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawLine(Camera.main.transform.position, transform.TransformDirection( new Vector3(direccionObj.x * Mathf.Cos(1) - direccionObj.z * Mathf.Sin(1), cam.transform.localPosition.y,direccionObj.x*Mathf.Sin(1) + direccionObj.z*Mathf.Cos(1))));
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawLine(Camera.main.transform.position, Quaternion.AngleAxis(-90, Vector3.up) * (target.position - Camera.main.transform.localPosition));
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(Camera.main.transform.position, new Vector3(target.transform.position.x, Camera.main.transform.localPosition.y, target.transform.position.z) - Camera.main.transform.localPosition);
    //}


}
