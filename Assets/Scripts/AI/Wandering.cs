using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Wandering : MonoBehaviour
{
    private NavMeshAgent n;

    // Start is called before the first frame update
    void Start()
    {
        n = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((n.remainingDistance <0.2f) && n.isOnNavMesh)
        {
            //n.destination = 
            n.SetDestination(GetRandomLocation());
            n.isStopped = false;
        }
        else
        {
        }
    }
    
    
    
    
    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
     
        int maxIndices = navMeshData.indices.Length - 3;
        // Pick the first indice of a random triangle in the nav mesh
        int firstVertexSelected = Random.Range(0, maxIndices);
        int secondVertexSelected = Random.Range(0, maxIndices);
        //Spawn on Verticies
        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
     
        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];
        //Eliminate points that share a similar X or Z position to stop spawining in square grid line formations
        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x ||
            (int)firstVertexPosition.z == (int)secondVertexPosition.z
        )
        {
            point = GetRandomLocation(); //Re-Roll a position - I'm not happy with this recursion it could be better
        }
        else
        {
            // Select a random point on it
            point = Vector3.Lerp(
                firstVertexPosition,
                secondVertexPosition, //[t + 1]],
                Random.Range(0.05f, 0.95f) // Not using Random.value as clumps form around Verticies 
            );
        }
        //Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value); //Made Obsolete
     
        return point;
    }

    private void OnDrawGizmosSelected()
    {
        if( n != null)
        Gizmos.DrawSphere(n.destination,1);
    }
}
