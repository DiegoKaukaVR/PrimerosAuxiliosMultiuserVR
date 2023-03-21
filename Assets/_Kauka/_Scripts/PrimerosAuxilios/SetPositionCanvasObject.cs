using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionCanvasObject : MonoBehaviour
{

    RectTransform rectTransform;
    Canvas canvas;

    [SerializeField] Transform target;
    [SerializeField] Vector2 offSet;

  
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        //ChangePosition(target.position);
        ChangePositionTarget(target.position);


    }
    private void Update()
    {
        ChangePositionTarget(target.position);
    }

    private void OnValidate()
    {
        if (target != null)
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
            ChangePositionTarget(target.position);
        }
       
    }
    public void ChangePositionTarget(Vector3 targetPos)
    {
        rectTransform.transform.position = targetPos;
    }

    Vector3 A;
    Vector3 B;

    float dist;
    Vector3 dir;
    [SerializeField] bool changed;
    void ChangePosition(Vector3 targetPos)
    {
      
        changed = true;
        rectTransform.localPosition = Vector3.zero;
        A = rectTransform.position;
        B = targetPos;

        dist = Vector3.Distance(A, B);
        dir = new Vector3(B.x - A.x, B.y - A.y, B.z - A.z);

        if (dist < Mathf.Epsilon || dir.magnitude < Mathf.Epsilon)
        {
            Debug.LogError("No tiene sentido cambiar posición de milimetros");
            return;
        }

        rectTransform.localPosition = rectTransform.localPosition + dist * dir.normalized;


    }

    [SerializeField] bool debug = true;

    private void OnDrawGizmosSelected()
    {
        //if (!debug)
        //    return;
        
        //if (rectTransform == null)
        //{
        //    rectTransform = GetComponent<RectTransform>();
        //}
        //if (!changed)
        //{
        //    ChangePosition(target.position);
        //}
        

        //Gizmos.DrawCube(A, Vector3.one * 0.1f);
        //Gizmos.DrawCube(B, Vector3.one * 0.1f);

        //Gizmos.DrawLine(A, B);
        //Gizmos.DrawRay(A, rectTransform.localPosition + dist * dir.normalized);

    }
    void SetPosition()
    {
        //rectTransform.transform.position = target.position + (Vector2)offSet;
    }

    void MovetoClickPoint(Vector3 objectTransformPosition)
    {
        // Get the position on the canvas
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * rectTransform.sizeDelta.x ,
            ViewportPosition.y * rectTransform.sizeDelta.y);

        // Set the position and remove the screen offset
        this.rectTransform.localPosition = proportionalPosition - offSet;
    }


    
}
