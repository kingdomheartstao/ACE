using UnityEngine;
using System.Collections;

public class Aerodynamic : MonoBehaviour {
    Vector3 speed;
    Vector3 pos;
    Vector3 torque;
    Vector3 enginF;
    Vector3 Force;
    Vector3 a;
    Vector3 lift;
    Vector3 resistance;
    Engin engin;
    AirControl ac;
    float m = 1;
    float g = 9.8f;
    float attackAngle = 0;
    float maxResistance = 1;
    float maxSpeed = 5;
    float maxLift = 10;
    Rigidbody rig;

    void GetStatus()
    {
        enginF = transform.rotation * (Vector3.right * engin.power);
        float xTorque = ac.aileronAngle;
        float zTorque = ac.elevatorAngle;
        float yTorque = ac.rudderAngle;
        if (xTorque > 30)
        {
            xTorque -= 360;
        }
        if (zTorque > 40)
        {
            zTorque -= 360;
        }
        if (yTorque > 30)
        {
            yTorque -= 360;
        }
        torque = new Vector3(xTorque, -yTorque, -zTorque);

        //torque = new Vector3(xTorque, 0, 0);
        //torque = transform.rotation * torque;
        //rig.AddTorque(torque * 0.05f);

        //torque = new Vector3(0, 0, -zTorque);
        //torque = transform.rotation * torque;
        //rig.AddTorque(torque * 0.02f);

        //torque = new Vector3(0, -yTorque, 0);
        //torque = transform.rotation * torque;
        //rig.AddTorque(torque * 0.02f);
    }


    void Calculate()
    {
        float m_lift;
        Vector3 m_speed;
        Quaternion cDrt = new Quaternion(-transform.rotation.x, -transform.rotation.y, -transform.rotation.z, transform.rotation.w);
        m_speed = cDrt * rig.velocity;
        m_lift = Mathf.Log(m_speed.x + 1) * 2;
        Debug.Log(m_lift);
        if (m_lift > maxLift)
            m_lift = maxLift;

        torque = new Vector3(torque.x * 0.5f, torque.y * 0.2f, torque.z * 0.2f);
        torque = transform.rotation * torque;
        rig.AddTorque(torque);
        lift = transform.rotation * Vector3.up * m_lift;
        rig.AddForce(enginF * 5 + lift);
        Debug.Log(rig.velocity + " ms " + m_speed.x);
    }

    void GetAirSpeed()
    {

    }

	// Use this for initialization
	void Start () {
        engin = GameObject.Find("Engin").GetComponent<Engin>();
        ac = GameObject.Find("Body").GetComponent<AirControl>();
        rig = GetComponent<Rigidbody>();
        rig.maxAngularVelocity = 2;
        rig.angularDrag = 5;
        speed = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetStatus();
        Calculate();
	}
}
