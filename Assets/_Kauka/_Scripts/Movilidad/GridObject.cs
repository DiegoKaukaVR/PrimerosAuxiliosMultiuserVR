using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class GridObject : MonoBehaviour
{
    bool select;
    private GridPerso grid;
    public Vector3 lastGridPos;
    Collider colliderSecundario;

    public GameObject agarreObjetivo;

    public int indiceAgarre;
    public bool pie;
    private void Awake()
    {
        grid = FindObjectOfType<GridPerso>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (select)
        {
            PlaceCubeNear2(colliderSecundario);
        }
    }

    public void PlaceCubeNear2(Collider colliderAux)
    {
        colliderSecundario = colliderAux;
        if (select == false)
        {
            GameControllerMovilidad.Instance.VisualizeActualObjective(indiceAgarre);
            //StartCoroutine(grid.EncenderPuntoVisual(colliderSecundario, transform));
        }
        select = true;

        //grid.GetNearestVisualPointOnGrid(transform.position, colliderAux);

        //grid.CambiarAlfa(colliderAux, agarreObjetivo.transform);

        Vector3 posFinal = GetLimit(transform.position);
        if (posFinal == Vector3.zero)
        {
            transform.position = lastGridPos;
        }
        else
        {
            lastGridPos = posFinal;
        }


        //Actualizar Aqui el lastgridpos

        //Vector3 finalPosition = grid.GetNearestPointOnGrid(transform.position, colliderAux);
        //if (finalPosition != Vector3.zero)
        //{
        //    lastGridPos = finalPosition;
        //}
        //else
        //{
        //    transform.position = lastGridPos;
        //}

        //if (select == false)
        //{
        //    GameControllerMovilidad.Instance.VisualizeActualObjective(indiceAgarre);
        //    StartCoroutine(grid.EncenderPuntoVisual(colliderSecundario, agarreObjetivo.transform));
        //}
        //select = true;

        //grid.GetNearestVisualPointOnGrid(transform.position, colliderAux);

        //grid.CambiarAlfa(colliderAux, agarreObjetivo.transform);

        //Vector3 finalPosition = GetLimit(transform.position);
        //if (finalPosition != Vector3.zero)
        //{
        //    lastGridPos = finalPosition;
        //}
        //else
        //{
        //    transform.position = lastGridPos;
        //}
        CheckExitPosition();

    }

    public void Soltar(Collider colliderAux)
    {
        select = false;

        //Vector3 finalPosition = grid.GetNearestPointOnGrid(transform.position, colliderAux);

        //if (finalPosition == Vector3.zero)
        //{
        //    transform.position = lastGridPos;
        //}
        //else
        //{
        //    transform.position = finalPosition;
        //    lastGridPos = finalPosition;
        //}
        //grid.BorrarPActual();
        //StartCoroutine(grid.ApagarPuntoVisual());
        //GameControllerMovilidad.Instance.HideActualObjective();
        //grid.CheckValidPoint(transform.position, indiceAgarre, this.gameObject);
        //if (indiceAgarre == GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().indiceAgarreObjetivo)
        //{
        //    StartCoroutine(SetRotation());
        //}
        CheckExitPosition();
    }

    public void SetDestinyTransform()
    {
        if (GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().tomarRot)
        {
            if (!pie)
            {
                Vector3 dir = (transform.position - agarreObjetivo.transform.position).normalized;
                float dist = Vector3.Distance(transform.position, agarreObjetivo.transform.position);

                //transform.position = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position - transform.TransformDirection(dir * dist);
                if ((dir.x < 0 && dir.y < 0) || (dir.x < 0 && dir.z < 0) || (dir.y < 0 && dir.z < 0))
                {
                    transform.position = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position + (dir * dist);
                }
                else
                {
                    transform.position = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position + (dir * dist);
                }

            }
            else
            {
                transform.position = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position;
            }

            transform.eulerAngles = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.eulerAngles;
            
            //transform.rotation = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.rotation;
        }
        else
        {
         //   transform.position = GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position;
        }
        if (GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().agarre2Pos)
        {
            GameControllerMovilidad.Instance.agarres[GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().indiceAgarre2].transform.position =
                GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().agarre2Pos.transform.position;
        }
        //transform.position = Vector3.Lerp(pos,GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position, t);
    }

    Vector3 GetLimit(Vector3 pos)
    {
        if (GameControllerMovilidad.Instance.colliderGeneral.bounds.Contains(pos) && colliderSecundario.bounds.Contains(pos))
        {

            return pos;
        }
        else
        {
            return Vector3.zero;
        }
    }

    void CheckExitPosition()
    {
        if(select == false)
        {
            if (Vector3.Distance(GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position, GetComponent<XRGrabInteractable>().colliders[0].gameObject.transform.position) < GameControllerMovilidad.Instance.distanciaCorrectaAgarre)
            {
                //
                //Descomentar luego
                //
                SetDestinyTransform();

                GameControllerMovilidad.Instance.primeraCondicionActual = true;
                GameControllerMovilidad.Instance.CheckConditions();
            }
        }
        else
        {
            if (Vector3.Distance(GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position, GetComponent<XRGrabInteractable>().colliders[0].gameObject.transform.position) < GameControllerMovilidad.Instance.distanciaCorrectaAgarre)
            {

                GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().Crecer(20, 1);

            }
            else
            {
                 GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().DeshacerCrecer(1);
            }
        }
    }
}
