using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    public Transform target;
    public bool useAdvancedCamera = false;

    [Header("Basic Camera")]
    public float maxDistance = 15f;

    [Header("Advanced Camera")]
    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10.0f;

    void Update()
    {
        if(useAdvancedCamera)
        {
            Vector3 wantedPosition;
            if (followBehind)
                wantedPosition = target.TransformPoint(0, height, -distance);
            else
                wantedPosition = target.TransformPoint(0, height, distance);

            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else transform.LookAt(target, target.up);
        }
        else
        {
            transform.position = target.position;
            Quaternion targetRotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);
            transform.rotation = targetRotation;
            transform.Translate(new Vector3(0, 6, -maxDistance));

            RaycastHit hit;
            var camVector = transform.position - target.position;
            Ray ray = new Ray(target.position, camVector);
            if (Physics.Raycast(ray, out hit, maxDistance + 0.5f))
            {
                transform.position = hit.point + hit.normal;
            }

            var rot = transform.rotation.eulerAngles;
            rot.x = Vector3.Angle(target.position - transform.position, transform.forward);
            transform.rotation = Quaternion.Euler(rot);
            transform.Translate(Vector3.forward * 0.5f);
        }

    }
}
