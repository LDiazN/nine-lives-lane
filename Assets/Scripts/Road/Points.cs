using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint.position, 1);
        Gizmos.DrawWireSphere(endPoint.position, 1);
    }
}
