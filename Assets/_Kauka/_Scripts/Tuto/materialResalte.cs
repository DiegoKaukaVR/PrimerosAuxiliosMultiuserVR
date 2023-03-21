using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialResalte : MonoBehaviour
{
    Material myMat;
    float a;
    bool cambio;
    bool stahp;
    Coroutine comenzarResaMatCor;
    
    //string moduloCaso;
    //int moduloElegido;
    //int casoElegido;

    private void Start()
    {
        myMat = GetComponent<MeshRenderer>().materials[1];

    }

    #region dosNumerosPorValorDeInt
    //public void moduloYCaso(int i)
    //{
    //    moduloCaso = i.ToString();

    //    if (moduloCaso.Length == 1)
    //    {
    //        moduloElegido = 0;
    //        casoElegido = int.Parse(moduloCaso[0].ToString());
    //    }
    //    else
    //    {
    //        moduloElegido = int.Parse(moduloCaso[0].ToString());
    //        casoElegido = int.Parse(moduloCaso[1].ToString());
    //    }
    //    Debug.Log("Modulo elegido: " + moduloElegido + " " + "Caso elegido: " + casoElegido);
    //}
    #endregion dosNumerosPorValorDeInt

    public void comenzarResalteMat()
    {
        if (comenzarResaMatCor != null)
        {
            StopAllCoroutines();
            comenzarResaMatCor = null;
        }
        stahp = false;
        comenzarResaMatCor = StartCoroutine(comenzarResalteMaterial());
    }
    IEnumerator comenzarResalteMaterial()
    {
        while (!stahp)
        {
            //Debug.Log("sigo dentro de cor " + gameObject.name);
            if (a <= 1 && !cambio)
            {
                a += Time.deltaTime * 1;
                if (a >= 1)
                {
                    cambio = true;
                }
            }
            else if (a >= 0 && cambio)
            {
                a -= Time.deltaTime * 1;
                if (a <= 0)
                {
                    cambio = false;
                }
            }
            myMat.SetFloat("Vector1_244f9c48e1504b879d36c4958b660da7", a);
            yield return new WaitForEndOfFrame();
        }
    }
    public void pararMaterialResalte()
    {
        myMat.SetFloat("Vector1_244f9c48e1504b879d36c4958b660da7", 0);
        stahp = true;
        if (comenzarResaMatCor != null)
        {
            StopCoroutine(comenzarResalteMaterial());
            comenzarResaMatCor = null;
        }
    }
}