using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;
    public float speed;
    public float waitTime;
    public float turnSpeed;

    void OnDrawGizmos() {
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
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < pathHolder.childCount; i++) {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        StartCoroutine(FollowPath(waypoints));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath(Vector3[] waypoints) {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint) {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(angle, transform.eulerAngles.y)) > .05f) {
            float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, angle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * newAngle;
            yield return null;
        }
    }
}
