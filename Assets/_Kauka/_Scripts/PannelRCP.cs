using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PannelRCP : MonoBehaviour
{
    public GameObject marker;
    public RectTransform rectTransform;

    public TextMeshProUGUI textCounter;
    public TextMeshProUGUI textTimer;

    [Header("UI Configuration")]
    [Min(0)]
    public float porcentage = 0f;

    public Vector3 posA;
    public Vector3 posB;

    public float horizontalLenght = 1f;
    public float offSetY;

    public float speed = 0.5f;

    bool started;

    public bool ejeX;
    public bool flip;


    private void Start()
    {
        rectTransform = marker.GetComponent<RectTransform>();

        posA = new Vector3(rectTransform.position.x, rectTransform.position.y, rectTransform.position.z);

        
        //posB = new Vector3(rectTransform.right)

        if (ejeX)
        {
            if (!flip)
            {
                posB = new Vector3(rectTransform.position.x + horizontalLenght * 2, rectTransform.position.y, rectTransform.position.z);
            }
            else
            {
                posB = new Vector3(rectTransform.position.x - horizontalLenght * 2, rectTransform.position.y, rectTransform.position.z);
            }
            
        }
        else
        {
            if (!flip)
            {
                posB = new Vector3(rectTransform.position.x, rectTransform.position.y, rectTransform.position.z - horizontalLenght * 2);
            }
            else
            {
                posB = new Vector3(rectTransform.position.x, rectTransform.position.y, rectTransform.position.z + horizontalLenght * 2);
            }
         
        }

        Debug.DrawLine(posA, posB, Color.red, 10f);
        started = true;
    }

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        textTimer.text = string.Format("{0:0.0}", timer);
        BucleMovement();
    }

    [Header("Valid Configuration")]
    public float minValid;
    public float maxValid;

    #region Feedback
    public Image validImage;
    public Image invalidImage1;
    public Image invalidImage2;

    public float alphaSelection = 1f;
    public float scaleSelection = 0.25f;
    Color colorValid;
    #endregion

 
    void BucleMovement()
    {
        if (ejeX)
        {
            rectTransform.localPosition += rectTransform.right * speed * Time.deltaTime;
            //rectTransform.localPosition = new Vector3(rectTransform.localPosition.x - Time.deltaTime * speed, rectTransform.localPosition.y, rectTransform.localPosition.z);
            Porcentaje();

            if (!flip)
            {
                if (rectTransform.position.x > posB.x)
                {
                    timer = 0;
                    rectTransform.position = posA;
                }
            }
            else
            {
                if (rectTransform.position.x < posB.x)
                {
                    timer = 0;
                    rectTransform.position = posA;
                }
            }
        
        }
        else
        {
          
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x  - Time.deltaTime * speed, rectTransform.localPosition.y, rectTransform.localPosition.z);
            Porcentaje();

            //Debug.Log("Marker X" + rectTransform.localPosition.x);
            //Debug.Log("Limit X" + posB.z);
            if (!flip)
            {
                if (rectTransform.position.z < posB.z)
                {
                    timer = 0;
                    rectTransform.position = posA;
                }
            }
            else
            {
                if (rectTransform.position.z > posB.z)
                {
                    timer = 0;
                    rectTransform.position = posA;
                }
            }
           
        }


    }

    public void Porcentaje()
    {
        float currentLenght = Vector3.Distance(posA, rectTransform.position);
        float maxLenght = Vector3.Distance(posA, posB);
        porcentage = CalculatePorcentage(currentLenght, maxLenght);
        //Debug.Log("Porcentage:" + porcentage);


    }

    public void SetCounterRCP(int value)
    {
        textCounter.text = value.ToString();
    }

    /// <summary>
    /// Called from RCP script
    /// </summary>
    public bool MakePush()
    {
        if (porcentage>minValid && porcentage <maxValid)
        {
            Debug.Log("Valid Push");
            GameManager.instance.audioManager.PlayComplete();
            timer = 0;
            rectTransform.position = posA;
            return true;

            #region feedback
            //Change Color and scale
            //colorValid = validImage.color;
            //validImage.color = new Color(colorValid.r, colorValid.g, colorValid.b, alphaSelection);
            //validImage.rectTransform.localScale *= scaleSelection;
            #endregion
        }
        else
        {
            Debug.Log("Not valid push");
            //GameManager.instance.audioManager.PlayFail();
            return false;
        }
    }

    public float CalculatePorcentage(float currentPos, float maxPos)
    {
        return (currentPos / maxPos) * 100;
    }

    Vector3 posAA;
    Vector3 posBB;
    private void OnDrawGizmos()
    {
        if (rectTransform == null)
        {
            rectTransform = marker.GetComponent<RectTransform>();
        }

        if (started)
        {
            return;
        }


        //posA = new Vector3(rectTransform.position.x, rectTransform.position.y, rectTransform.position.z);
        //posB = new Vector3(rectTransform.position.x, rectTransform.position.y, rectTransform.position.z - horizontalLenght * 2);
        //Debug.DrawLine(posA, posB);

    }


}
