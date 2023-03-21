using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridtest : MonoBehaviour
{
    public Grid gridCC;

    Vector3 pos1;
    Vector3 pos2;
    Vector3 pos3;
    Vector3 pos4;
    Vector3 pos5;
    Vector3 pos6;
    Vector3 pos7;

    bool xNegativo;
    bool xPositivo;
    bool yNegativo;
    bool yPositivo;
    bool zNegativo;
    bool zPositivo;

    Vector3 initSize;
    Vector3 posIni;
    bool escalado;
    float t = 0;
    public float tiempoCrecer;
   void Start()
   {
        initSize = transform.localScale;
        posIni = transform.position;

   }
    IEnumerator Escalar()
    {
        float t = Time.time;

        while (Time.time < t + tiempoCrecer)
        {
            transform.localScale = Vector3.Lerp(initSize, initSize * 2, (Time.time-t)/tiempoCrecer);
            //transform.position = Vector3.Lerp(posIni, posIni + new Vector3(1, 0, 0), 1);
            //puntoObj.transform.localScale = Vector3.MoveTowards(initSize, initSize*maxSize, t);
        }
        yield return null;
    }
    IEnumerator VolverEscala()
    {
        float t = 0;
        Vector3 sizeActual = transform.localScale;
        while (t < tiempoCrecer)
        {
            t += Time.deltaTime;

            transform.localScale = Vector3.Lerp(sizeActual, initSize, t / tiempoCrecer);
            //transform.position = Vector3.Lerp(posActual, posIni, t / 2);
            //puntoObj.transform.localScale = Vector3.MoveTowards(initSize, initSize*maxSize, t);
        }
        yield return null;
    }
    void Update()
    {
       
        //    Vector3Int cellPos = gridCC.WorldToCell(transform.position);
        //    Vector3Int cellPos2 = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x,0,0));
        //    Vector3Int cellPos3 = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, 0, 0));

        //    Vector3Int cellPos4 = gridCC.WorldToCell(transform.position + new Vector3(0,gridCC.cellSize.y, 0));
        //    Vector3Int cellPos5 = gridCC.WorldToCell(transform.position + new Vector3(0,-gridCC.cellSize.y, 0));

        //    Vector3Int cellPos6 = gridCC.WorldToCell(transform.position + new Vector3(0,0,gridCC.cellSize.z));
        //    Vector3Int cellPos7 = gridCC.WorldToCell(transform.position + new Vector3(0,0,-gridCC.cellSize.z));

        //    //Vector3 posFinal = gridCC.CellToWorld(cellPos);

        //    pos1 = gridCC.CellToWorld(cellPos);
        //    pos2 = gridCC.CellToWorld(cellPos2);
        //    pos3 = gridCC.CellToWorld(cellPos3);
        //    pos4 = gridCC.CellToWorld(cellPos4);
        //    pos5 = gridCC.CellToWorld(cellPos5);
        //    pos6 = gridCC.CellToWorld(cellPos6);
        //    pos7 = gridCC.CellToWorld(cellPos7);


        //    //Calculo de X
        //    if(Vector3.Distance(transform.position, pos1) < Vector3.Distance(transform.position, pos2) && Vector3.Distance(transform.position, pos1) < Vector3.Distance(transform.position, pos3))
        //    {
        //        xNegativo = false; xPositivo = false;
        //    }
        //    if (Vector3.Distance(transform.position, pos2) < Vector3.Distance(transform.position, pos1) && Vector3.Distance(transform.position, pos2) < Vector3.Distance(transform.position, pos3))
        //    {
        //        xNegativo = false; xPositivo = true;
        //    }
        //    if (Vector3.Distance(transform.position, pos3) < Vector3.Distance(transform.position, pos1) && Vector3.Distance(transform.position, pos3) < Vector3.Distance(transform.position, pos2))
        //    {
        //        xNegativo = true; xPositivo = false;
        //    }

        //    //Calculo de Y
        //    if (Vector3.Distance(transform.position, pos1) < Vector3.Distance(transform.position, pos4) && Vector3.Distance(transform.position, pos1) < Vector3.Distance(transform.position, pos5))
        //    {
        //        yNegativo = false; yPositivo = false;
        //    }
        //    if (Vector3.Distance(transform.position, pos4) < Vector3.Distance(transform.position, pos1) && Vector3.Distance(transform.position, pos4) < Vector3.Distance(transform.position, pos5))
        //    {
        //        yNegativo = false; yPositivo = true;
        //    }
        //    if (Vector3.Distance(transform.position, pos5) < Vector3.Distance(transform.position, pos1) && Vector3.Distance(transform.position, pos5) < Vector3.Distance(transform.position, pos4))
        //    {
        //        yNegativo = true; yPositivo = false;
        //    }

        //    //Calculo de Z
        //    if (Vector3.Distance(transform.position, pos1) < Vector3.Distance(transform.position, pos6) && Vector3.Distance(transform.position, pos1) < Vector3.Distance(transform.position, pos7))
        //    {
        //        zNegativo = false; zPositivo = false;
        //    }
        //    if (Vector3.Distance(transform.position, pos6) < Vector3.Distance(transform.position, pos1) && Vector3.Distance(transform.position, pos6) < Vector3.Distance(transform.position, pos7))
        //    {
        //        zNegativo = false; zPositivo = true;
        //    }
        //    if (Vector3.Distance(transform.position, pos7) < Vector3.Distance(transform.position, pos1) && Vector3.Distance(transform.position, pos7) < Vector3.Distance(transform.position, pos6))
        //    {
        //        zNegativo = true; zPositivo = false;
        //    }


        //    Vector3Int cellPosFinal = Vector3Int.zero;

        //    if(!xNegativo && !xPositivo)
        //    {
        //        if (!yNegativo && !yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position);
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, 0, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, 0, gridCC.cellSize.z));
        //            }
        //        }
        //        else if (yNegativo && !yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, -gridCC.cellSize.y, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, -gridCC.cellSize.y, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, -gridCC.cellSize.y, gridCC.cellSize.z));
        //            }
        //        }
        //        else if(!yNegativo && yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, gridCC.cellSize.y, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, gridCC.cellSize.y, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(0, gridCC.cellSize.y, gridCC.cellSize.z));
        //            }
        //        }
        //    }
        //    else if(xNegativo && !xPositivo)
        //    {
        //        if (!yNegativo && !yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                 cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, 0, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, 0, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, 0, gridCC.cellSize.z));
        //            }
        //        }
        //        else if (yNegativo && !yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, -gridCC.cellSize.y, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, -gridCC.cellSize.y, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, -gridCC.cellSize.y, gridCC.cellSize.z));
        //            }
        //        }
        //        else if (!yNegativo && yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, gridCC.cellSize.y, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, gridCC.cellSize.y, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(-gridCC.cellSize.x, gridCC.cellSize.y, gridCC.cellSize.z));
        //            }
        //        }
        //    }
        //    else if(!xNegativo && xPositivo)
        //    {
        //        if (!yNegativo && !yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, 0, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, 0, gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, 0, -gridCC.cellSize.z));
        //            }
        //        }
        //        else if (yNegativo && !yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, -gridCC.cellSize.y, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, -gridCC.cellSize.y, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, -gridCC.cellSize.y, gridCC.cellSize.z));
        //            }
        //        }
        //        else if (!yNegativo && yPositivo)
        //        {
        //            if (!zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, gridCC.cellSize.y, 0));
        //            }
        //            else if (zNegativo && !zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, gridCC.cellSize.y, -gridCC.cellSize.z));
        //            }
        //            else if (!zNegativo && zPositivo)
        //            {
        //                cellPosFinal = gridCC.WorldToCell(transform.position + new Vector3(gridCC.cellSize.x, gridCC.cellSize.y, gridCC.cellSize.z));
        //            }
        //        }
        //    }

        //    transform.position = gridCC.CellToWorld(cellPosFinal);
        //}
    }
}
