using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunteroLinea : MonoBehaviour
{
    Vector3 pos1;
    Vector3 pos2;
    bool activo;
    // Start is called before the first frame update
    void Start()
    {
        activo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activo)
        {
            GetComponent<LineRenderer>().SetPosition(0, pos1);
            GetComponent<LineRenderer>().SetPosition(1, pos2);
        }
    }

    public void SetPositions(Vector3 p1, Vector3 p2)
    {
        pos1 = p1;
        pos2 = p2;
        activo = true;
    }
    public void Disable()
    {
        activo = false;
        this.gameObject.SetActive(false);
    }
}
