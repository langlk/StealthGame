using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    Transform guard;

    void OnDrawGizmos() {
        Transform pathHolder = transform.GetChild(1);
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 prevPosition = startPosition;
        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawLine(prevPosition, waypoint.position);
            prevPosition = waypoint.position;
            Gizmos.DrawSphere(waypoint.position, .3f);
        }
        Gizmos.DrawLine(prevPosition, startPosition);
    }
    // Start is called before the first frame update
    void Start()
    {
        Transform guard = transform.GetChild(0);
        guard.gameObject.GetComponent<Guard>().pathHolder = transform.GetChild(1);
    }
}
