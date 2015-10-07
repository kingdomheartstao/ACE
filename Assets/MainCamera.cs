using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    public Transform target;
    public float smoothing = 500f;
    Vector3 camPos;
    Vector3 offset;
    Quaternion offsetA;
	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
        offsetA = target.rotation * transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, 1);
        transform.LookAt(target);
	}
}
