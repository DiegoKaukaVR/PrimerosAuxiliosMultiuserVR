using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPerso : MonoBehaviour
{
    public Color colorFuera;
    public Color colorNormal;
    public Color colorCorrecto;
    public float dimensiones = 10;
    public float sphereSize = 0.2f;
    GridLayout gridComp;
    public Collider colliderGeneral;

    public GameObject puntoVisual;

    public List<GameObject> puntosInstanciadosGrid = new List<GameObject>();

    public float distanciaAPuntoCorrecto = 0.2f;
    public List<GameObject> puntosCorrectos = new List<GameObject>();


    public float tiempoAnimacionPunto = 0.7f;
    public float alfaMin = 0f;
    public float alfaMax = 0.5f;
    public float relacionDistancia = 0.4f;
    public float maxSize;
    public float maxSizeCorrecto;
    Camera mainCamera;

    bool changeAlfaAux;


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

    GameObject puntoGuardado;
    GameObject puntoActual;

    Vector3 initSize;
    //public Vector3 GetNearestPointOnGrid(Vector3 position)
    //{
    //    position -= transform.position;

    //    int xCount = Mathf.RoundToInt(position.x / size);
    //    int yCount = Mathf.RoundToInt(position.y / size);
    //    int zCount = Mathf.RoundToInt(position.z / size);

    //    Vector3 result = new Vector3 (
    //        (float) xCount * size,
    //        (float) xCount * size,
    //        (float) xCount * size);

    //    result += transform.position;

    //    return result;

    //}

    void Awake()
    {
        gridComp = GetComponent<GridLayout>();
        mainCamera = Camera.main;
        
        if (gridComp.cellSize.x > 0 && gridComp.cellSize.y > 0 && gridComp.cellSize.z > 0)
        {
            for (float x = 0; x < dimensiones; x += gridComp.cellSize.x)
            {
                for (float z = 0; z < dimensiones; z += gridComp.cellSize.z)
                {
                    for (float y = 0; y < dimensiones; y += gridComp.cellSize.y)
                    {
                        Vector3 point = GetNearestPointOnGrid(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z), colliderGeneral.GetComponent<Collider>());

                        if (colliderGeneral.bounds.Contains(point))
                        {
                            GameObject puntoV = Instantiate(puntoVisual, point, Quaternion.identity, gridComp.transform);
                            puntosInstanciadosGrid.Add(puntoV);
                            Color colorAux = puntoV.GetComponent<SpriteRenderer>().color;
                            colorAux.a = alfaMin;
                            puntoV.GetComponent<SpriteRenderer>().color = colorAux;
                            
                        }
                        
                    }
                }
            }
        }
        initSize = puntosInstanciadosGrid[0].transform.localScale;
    }

   

    public Vector3 GetNearestPointOnGrid(Vector3 position, Collider colliderAux)
    {
        Vector3Int cellPos = gridComp.WorldToCell(position);
        Vector3Int cellPos2 = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, 0));
        Vector3Int cellPos3 = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, 0));

        Vector3Int cellPos4 = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, 0));
        Vector3Int cellPos5 = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, 0));

        Vector3Int cellPos6 = gridComp.WorldToCell(position + new Vector3(0, 0, gridComp.cellSize.z));
        Vector3Int cellPos7 = gridComp.WorldToCell(position + new Vector3(0, 0, -gridComp.cellSize.z));

        //Vector3 posFinal = gridCC.CellToWorld(cellPos);

        pos1 = gridComp.CellToWorld(cellPos);
        pos2 = gridComp.CellToWorld(cellPos2);
        pos3 = gridComp.CellToWorld(cellPos3);
        pos4 = gridComp.CellToWorld(cellPos4);
        pos5 = gridComp.CellToWorld(cellPos5);
        pos6 = gridComp.CellToWorld(cellPos6);
        pos7 = gridComp.CellToWorld(cellPos7);

        //Calculo de X
        if (Vector3.Distance(position, pos1) < Vector3.Distance(position, pos2) && Vector3.Distance(position, pos1) < Vector3.Distance(position, pos3))
        {
            xNegativo = false; xPositivo = false;
        }
        if (Vector3.Distance(position, pos2) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos2) < Vector3.Distance(position, pos3))
        {
            xNegativo = false; xPositivo = true;
        }
        if (Vector3.Distance(position, pos3) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos3) < Vector3.Distance(position, pos2))
        {
            xNegativo = true; xPositivo = false;
        }

        //Calculo de Y
        if (Vector3.Distance(position, pos1) < Vector3.Distance(position, pos4) && Vector3.Distance(position, pos1) < Vector3.Distance(position, pos5))
        {
            yNegativo = false; yPositivo = false;
        }
        if (Vector3.Distance(position, pos4) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos4) < Vector3.Distance(position, pos5))
        {
            yNegativo = false; yPositivo = true;
        }
        if (Vector3.Distance(position, pos5) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos5) < Vector3.Distance(position, pos4))
        {
            yNegativo = true; yPositivo = false;
        }

        //Calculo de Z
        if (Vector3.Distance(position, pos1) < Vector3.Distance(position, pos6) && Vector3.Distance(position, pos1) < Vector3.Distance(position, pos7))
        {
            zNegativo = false; zPositivo = false;
        }
        if (Vector3.Distance(position, pos6) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos6) < Vector3.Distance(position, pos7))
        {
            zNegativo = false; zPositivo = true;
        }
        if (Vector3.Distance(position, pos7) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos7) < Vector3.Distance(position, pos6))
        {
            zNegativo = true; zPositivo = false;
        }


        Vector3Int cellPosFinal = Vector3Int.zero;

        if (!xNegativo && !xPositivo)
        {
            if (!yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position);
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, 0, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, 0, gridComp.cellSize.z));
                }
            }
            else if (yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
            else if (!yNegativo && yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
        }
        else if (xNegativo && !xPositivo)
        {
            if (!yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, gridComp.cellSize.z));
                }
            }
            else if (yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, -gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, -gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, -gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
            else if (!yNegativo && yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
        }
        else if (!xNegativo && xPositivo)
        {
            if (!yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, gridComp.cellSize.z));
                }
            }
            else if (yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, -gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, -gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, -gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
            else if (!yNegativo && yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
        }

        Vector3 posFinal = gridComp.CellToWorld(cellPosFinal);
        if (colliderGeneral.bounds.Contains(posFinal) && colliderAux.bounds.Contains(posFinal))
        {
            
            return posFinal;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void GetNearestVisualPointOnGrid(Vector3 position, Collider colliderAux)
    {

        //GridLayout gridlay = GetComponent<GridLayout>();
        //Vector3Int cellPos = gridlay.WorldToCell(position);
        //Vector3 posFinal = gridlay.CellToWorld(cellPos);

        Vector3Int cellPos = gridComp.WorldToCell(position);
        Vector3Int cellPos2 = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, 0));
        Vector3Int cellPos3 = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, 0));

        Vector3Int cellPos4 = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, 0));
        Vector3Int cellPos5 = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, 0));

        Vector3Int cellPos6 = gridComp.WorldToCell(position + new Vector3(0, 0, gridComp.cellSize.z));
        Vector3Int cellPos7 = gridComp.WorldToCell(position + new Vector3(0, 0, -gridComp.cellSize.z));

        //Vector3 posFinal = gridCC.CellToWorld(cellPos);

        pos1 = gridComp.CellToWorld(cellPos);
        pos2 = gridComp.CellToWorld(cellPos2);
        pos3 = gridComp.CellToWorld(cellPos3);
        pos4 = gridComp.CellToWorld(cellPos4);
        pos5 = gridComp.CellToWorld(cellPos5);
        pos6 = gridComp.CellToWorld(cellPos6);
        pos7 = gridComp.CellToWorld(cellPos7);

        //Calculo de X
        if (Vector3.Distance(position, pos1) < Vector3.Distance(position, pos2) && Vector3.Distance(position, pos1) < Vector3.Distance(position, pos3))
        {
            xNegativo = false; xPositivo = false;
        }
        if (Vector3.Distance(position, pos2) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos2) < Vector3.Distance(position, pos3))
        {
            xNegativo = false; xPositivo = true;
        }
        if (Vector3.Distance(position, pos3) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos3) < Vector3.Distance(position, pos2))
        {
            xNegativo = true; xPositivo = false;
        }

        //Calculo de Y
        if (Vector3.Distance(position, pos1) < Vector3.Distance(position, pos4) && Vector3.Distance(position, pos1) < Vector3.Distance(position, pos5))
        {
            yNegativo = false; yPositivo = false;
        }
        if (Vector3.Distance(position, pos4) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos4) < Vector3.Distance(position, pos5))
        {
            yNegativo = false; yPositivo = true;
        }
        if (Vector3.Distance(position, pos5) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos5) < Vector3.Distance(position, pos4))
        {
            yNegativo = true; yPositivo = false;
        }

        //Calculo de Z
        if (Vector3.Distance(position, pos1) < Vector3.Distance(position, pos6) && Vector3.Distance(position, pos1) < Vector3.Distance(position, pos7))
        {
            zNegativo = false; zPositivo = false;
        }
        if (Vector3.Distance(position, pos6) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos6) < Vector3.Distance(position, pos7))
        {
            zNegativo = false; zPositivo = true;
        }
        if (Vector3.Distance(position, pos7) < Vector3.Distance(position, pos1) && Vector3.Distance(position, pos7) < Vector3.Distance(position, pos6))
        {
            zNegativo = true; zPositivo = false;
        }


        Vector3Int cellPosFinal = Vector3Int.zero;

        if (!xNegativo && !xPositivo)
        {
            if (!yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position);
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, 0, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, 0, gridComp.cellSize.z));
                }
            }
            else if (yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, -gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
            else if (!yNegativo && yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(0, gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
        }
        else if (xNegativo && !xPositivo)
        {
            if (!yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, 0, gridComp.cellSize.z));
                }
            }
            else if (yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, -gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, -gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, -gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
            else if (!yNegativo && yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(-gridComp.cellSize.x, gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
        }
        else if (!xNegativo && xPositivo)
        {
            if (!yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, 0, gridComp.cellSize.z));
                }
            }
            else if (yNegativo && !yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, -gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, -gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, -gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
            else if (!yNegativo && yPositivo)
            {
                if (!zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, gridComp.cellSize.y, 0));
                }
                else if (zNegativo && !zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, gridComp.cellSize.y, -gridComp.cellSize.z));
                }
                else if (!zNegativo && zPositivo)
                {
                    cellPosFinal = gridComp.WorldToCell(position + new Vector3(gridComp.cellSize.x, gridComp.cellSize.y, gridComp.cellSize.z));
                }
            }
        }

        Vector3 posFinal = gridComp.CellToWorld(cellPosFinal);

        if (colliderGeneral.bounds.Contains(posFinal) && colliderAux.bounds.Contains(posFinal))
        {

            for (int i = 0; i < puntosInstanciadosGrid.Count; i++)
            {
                if (puntoActual != null && Vector3.Distance(posFinal, puntosInstanciadosGrid[i].transform.position) < Vector3.Distance(posFinal, puntoActual.transform.position))
                {
                    puntoActual = puntosInstanciadosGrid[i];
                }

                if (puntoActual == null)
                {
                    puntoActual = puntosInstanciadosGrid[i];
                }
            }

            //StartCoroutine(ResaltarPuntoActual(puntoActual));
            puntoActual.GetComponent<PuntoGridMovilidad>().enabled = true;
            puntoActual.GetComponent<PuntoGridMovilidad>().Crecer(maxSize, tiempoAnimacionPunto/2);
            
            if (puntoGuardado != null && puntoGuardado != puntoActual)
            {
                puntoGuardado.GetComponent<PuntoGridMovilidad>().enabled = true;
                puntoGuardado.GetComponent<PuntoGridMovilidad>().DeshacerCrecer(tiempoAnimacionPunto/2);
            }
            puntoGuardado = puntoActual;
        }

        //else
        //{
        //    StartCoroutine(ResaltarPuntoActual(puntoGuardado));
        //    puntoGuardado = null;
        //}
    }

    public void BorrarPActual()
    {
        puntoActual = null;
    }
    public IEnumerator EncenderPuntoVisual(Collider colliderSecundario, Transform posicionAgarre)
    {
        float t = 0;
        Color colorAux = puntosInstanciadosGrid[0].GetComponent<SpriteRenderer>().color;

        while (t < tiempoAnimacionPunto)
        {
            t += Time.deltaTime;
            colorAux.a = Mathf.Lerp(alfaMin, alfaMax, t/tiempoAnimacionPunto);

            foreach(GameObject go in puntosInstanciadosGrid)
            {
                float dist = Vector3.Distance(new Vector3(posicionAgarre.transform.position.x, posicionAgarre.transform.position.y, posicionAgarre.transform.position.z), go.transform.position);
                Color colorFinal = Color.white;
                
                if (colliderSecundario.bounds.Contains(go.transform.position))
                {
                    if (puntosCorrectos.Contains(go))
                    {
                        float distC = Vector3.Distance(GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position, go.transform.position);

                        colorFinal = Color.Lerp(colorCorrecto, colorNormal, distC / (distanciaAPuntoCorrecto));
                        //go.GetComponent<SpriteRenderer>().color = colorFinal;
                    }
                    else
                    {
                        colorFinal = colorNormal * new Color(1, 1, 1, colorAux.a);
                    }
                }
                else
                {
                    colorFinal = colorFuera * new Color(1, 1, 1, colorAux.a);
                }
                
                if (!puntosCorrectos.Contains(go))
                {
                    if (relacionDistancia / dist > 0.35f)
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, relacionDistancia / dist);

                    }
                    else
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, 0);
                    }
                }
                else if (puntosCorrectos.Contains(go))
                {
                    if (relacionDistancia / dist > 0.35f)
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, 1);

                    }
                    else
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, 0);
                    }
                }
            }
            yield return null;
        }
        changeAlfaAux = true;
    }

    public IEnumerator ApagarPuntoVisual()
    {
        float t = 0;
        Color colorAux = puntosInstanciadosGrid[0].GetComponent<SpriteRenderer>().color;
        
        while (t < tiempoAnimacionPunto)
        {
            t += Time.deltaTime;
            
            foreach (GameObject go in puntosInstanciadosGrid)
            {
                colorAux.a = Mathf.Lerp(go.GetComponent<SpriteRenderer>().color.a, alfaMin, t / tiempoAnimacionPunto);
                go.GetComponent<SpriteRenderer>().color = new Color(go.GetComponent<SpriteRenderer>().color.r, go.GetComponent<SpriteRenderer>().color.g, go.GetComponent<SpriteRenderer>().color.b, colorAux.a);
                if (!puntosCorrectos.Contains(go))
                {
                    go.transform.localScale = initSize;
                }
            }
            yield return null;

        }
        puntoGuardado = null;
        changeAlfaAux = false;
    }

    public void CambiarAlfa(Collider colliderSecundario, Transform posicionAgarre)
    {
        if (changeAlfaAux)
        {
            Color colorAux = puntosInstanciadosGrid[0].GetComponent<SpriteRenderer>().color;

            colorAux.a = alfaMax;
            foreach (GameObject go in puntosInstanciadosGrid)
            {
                float dist = Vector3.Distance(new Vector3(posicionAgarre.position.x, posicionAgarre.position.y, posicionAgarre.position.z), go.transform.position);
                Color colorFinal = Color.white;
                if (colliderSecundario.bounds.Contains(go.transform.position))
                {
                    if (puntosCorrectos.Contains(go))
                    {
                        float distC = Vector3.Distance(GameControllerMovilidad.Instance.puntoCorrectoActual.transform.position, go.transform.position);

                        colorFinal = Color.Lerp(colorCorrecto, colorNormal, distC/(distanciaAPuntoCorrecto));
                        //go.GetComponent<SpriteRenderer>().color = colorFinal;
                    }
                    else
                    {
                        colorFinal = colorNormal * new Color(1, 1, 1, colorAux.a);
                    }
                }
                else
                {
                    colorFinal = colorFuera * new Color(1, 1, 1, colorAux.a*2);
                }
                if(go== puntoActual)
                {
                    go.GetComponent<SpriteRenderer>().color = new Color(colorFinal.r, colorFinal.g, colorFinal.b, 1);
                }
                else if(!puntosCorrectos.Contains(go))
                {
                    if (relacionDistancia / dist > 0.35f)
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, relacionDistancia / dist);

                    }
                    else
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, 0);
                    }
                }
                else if (puntosCorrectos.Contains(go))
                {
                    if (relacionDistancia / dist > 0.35f)
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, 1);

                    }
                    else
                    {
                        go.GetComponent<SpriteRenderer>().color = colorFinal * new Color(colorFinal.r, colorFinal.g, colorFinal.b, 0);
                    }
                }
            }
        }
    }

    public void GetDesiredPoints(Transform puntoObj)
    {
        ClearDesiredPoints();

        foreach (GameObject go in puntosInstanciadosGrid)
        {
            float dist = Vector3.Distance(puntoObj.position, go.transform.position);
            
            if(dist<= distanciaAPuntoCorrecto)
            {
                puntosCorrectos.Add(go);
            }
        }

        foreach(GameObject goc in puntosCorrectos)
        {
            float dist = Vector3.Distance(puntoObj.position, goc.transform.position);

            if (dist < 0.05) { dist = 0; }

            goc.transform.localScale = Vector3.Lerp(goc.transform.localScale*maxSizeCorrecto, goc.transform.localScale, dist/distanciaAPuntoCorrecto);
            goc.GetComponent<PuntoGridMovilidad>().enabled = true;
            goc.GetComponent<PuntoGridMovilidad>().SetSecondInitialScale();
        }

    }

    public void ClearDesiredPoints()
    {
        if (puntosCorrectos.Count > 0)
        {
            foreach (GameObject go in puntosCorrectos)
            {
                go.GetComponent<PuntoGridMovilidad>().enabled = true;
                go.GetComponent<PuntoGridMovilidad>().VolverOrigen();
            }
            puntosCorrectos.Clear();
        }
    }

    public void CheckValidPoint(Vector3 point, int i, GameObject actual)
    {
        if(i == GameControllerMovilidad.Instance.puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().indiceAgarreObjetivo)
        {
            foreach (GameObject go in puntosCorrectos)
            {
                float dist = Vector3.Distance(point, go.transform.position);

                if (dist <= distanciaAPuntoCorrecto)
                {
                    //StartCoroutine(actual.GetComponent<GridObject>().SetRotation());

                    GameControllerMovilidad.Instance.primeraCondicionActual = true;
                    GameControllerMovilidad.Instance.CheckConditions();
                    return;
                }
            }
        }
    }
}
