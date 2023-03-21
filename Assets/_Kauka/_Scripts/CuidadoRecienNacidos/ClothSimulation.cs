using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSimulation : MonoBehaviour
{
    public Vector2 dimension = new Vector3(10, 10);

    [SerializeReference] int numberRay;
    [SerializeReference] float density;

    public MeshCollider meshCollider;
    public float distanceRaycast;
    [SerializeReference] int column;

    public LayerMask obstacleLayer;

    Vector3[] vertex;

    public Transform[] trackerArray;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        Initialize();
    }
    private void Update()
    {
        if (transform.position.y < minaltura)
        {
            transform.position = new Vector3(transform.position.x, minaltura, transform.position.z);
        }
     
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(trackerArray[i].position, Vector3.down, distanceRaycast, obstacleLayer))
            {
                Debug.DrawRay(trackerArray[i].position, Vector3.down * distanceRaycast, Color.red);
                minaltura = transform.position.y;
            }
        }
    }

    void Initialize()
    {
        vertex = GetVertex();
    }

    float minaltura;
    void RaycastController()
    {
      
      
    }
    Vector3[] GetVertex()
    {
        Vector3 ver1 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x + (dimension.x / 2), transform.position.y, transform.position.z + (dimension.y / 2)));
        Vector3 ver2 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x + (dimension.x / 2), transform.position.y, transform.position.z - (dimension.y / 2)));
        Vector3 ver3 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x - (dimension.x / 2), transform.position.y, transform.position.z + (dimension.y / 2)));
        Vector3 ver4 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x - (dimension.x / 2), transform.position.y, transform.position.z - (dimension.y / 2)));

        trackerArray[0].position = ver1;
        trackerArray[1].position = ver2;
        trackerArray[2].position = ver3;
        trackerArray[3].position = ver4;


        dimension = new Vector2(Vector2.Distance(ver1, ver3), Vector2.Distance(ver2, ver4));
        Vector3[] array = { ver1, ver2, ver3, ver4 };
        return array;
   

    }
    private void OnValidate()
    {
    }

    private void OnDrawGizmos()
    {
        //Vector3 newPos1 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x + (dimension.x / 2), transform.position.y, transform.position.z + (dimension.y / 2)));
        //Vector3 newPos2 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x + (dimension.x / 2), transform.position.y, transform.position.z - (dimension.y / 2)));
        //Vector3 newPos3 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x - (dimension.x / 2), transform.position.y, transform.position.z + (dimension.y / 2)));
        //Vector3 newPos4 = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x - (dimension.x / 2), transform.position.y, transform.position.z - (dimension.y / 2)));
       

        //for (int i = 0; i < dimension.x; i++)
        //{
        //   Vector3 vertex = meshCollider.ClosestPointOnBounds(new Vector3(transform.position.x + i, transform.position.y, transform.position.z + (dimension.y / 2)));
        //   Gizmos.DrawRay(vertex, Vector3.down);
        //}
        //Gizmos.DrawRay(newPos1, Vector3.down);
        //Gizmos.DrawRay(newPos2, Vector3.down);
        //Gizmos.DrawRay(newPos3, Vector3.down);
        //Gizmos.DrawRay(newPos4, Vector3.down);
    }
}
