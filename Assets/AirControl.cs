using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AirControl : MonoBehaviour {
    public float aileronAngle;
    public float rudderAngle;
    public float elevatorAngle;
    GameObject elevator;
    GameObject aileron;
    GameObject rudder;
    GameObject leftWing;
    GameObject rightWing;
    GameObject engin;
    Dictionary<string, bool> flag = new Dictionary<string,bool>();
	// Use this for initialization
	void Start () {
        elevator = GameObject.Find("Elevator");
        aileron = GameObject.Find("Aileron");
        rudder = GameObject.Find("Rudder");
        leftWing = GameObject.Find("LeftWing");
        rightWing = GameObject.Find("RightWing");
        engin = GameObject.Find("Engin");
        flag["Elevator"] = false;
        flag["Aileron"] = false;
        flag["Rudder"] = false;
	}



    void InputHandle()
    {
        //Elevator
        if (Input.GetKey(KeyCode.UpArrow))
        {
            flag["Elevator"] = true;
            if (elevator.transform.localEulerAngles.z < 30 || elevator.transform.localEulerAngles.z > 300)
                elevator.transform.Rotate(Vector3.forward, 2, Space.Self);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            flag["Elevator"] = true;
            if (elevator.transform.localEulerAngles.z > 330 || elevator.transform.localEulerAngles.z < 60)
                elevator.transform.Rotate(Vector3.forward, -2, Space.Self);
        }
        //Aileron
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            flag["Aileron"] = true;
            if (leftWing.transform.localEulerAngles.z < 20 || leftWing.transform.localEulerAngles.z > 320)
            {
                leftWing.transform.Rotate(Vector3.forward, 2, Space.Self);

                rightWing.transform.Rotate(Vector3.forward, -2, Space.Self);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            flag["Aileron"] = true;
            if (leftWing.transform.localEulerAngles.z > 340 || leftWing.transform.localEulerAngles.z < 40)
            {
                leftWing.transform.Rotate(Vector3.forward, -2, Space.Self);

                rightWing.transform.Rotate(Vector3.forward, 2, Space.Self);
            }
        }
        //Rudder
        if (Input.GetKey(KeyCode.Q))
        {
            flag["Rudder"] = true;
            if (rudder.transform.localEulerAngles.y < 20 || rudder.transform.localEulerAngles.y > 320)
                rudder.transform.Rotate(Vector3.up, 2, Space.Self);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            flag["Rudder"] = true;
            if (rudder.transform.localEulerAngles.y > 340 || rudder.transform.localEulerAngles.y < 40)
                rudder.transform.Rotate(Vector3.up, -2, Space.Self);
        }
        //Engin
        if (Input.GetKey(KeyCode.W))
        {
            engin.GetComponent<Engin>().ThrottleValeP();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            engin.GetComponent<Engin>().ThrottleValeM();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            engin.GetComponent<Engin>().TurnEngin();
        }
    }

    void ResetCF()
    {
        //Reset Elevator
        if (!flag["Elevator"] && Mathf.Abs(elevator.transform.localEulerAngles.z) >= 1)
        {
            if(elevator.transform.localEulerAngles.z < 60){
                elevator.transform.Rotate(Vector3.forward, -2, Space.Self);
            }
            else if (elevator.transform.localEulerAngles.z > 300)
            {
                elevator.transform.Rotate(Vector3.forward, 2, Space.Self);
            }
        }
        //Reset Aileron
        if (!flag["Aileron"] && Mathf.Abs(leftWing.transform.localEulerAngles.z) >= 1)
        {
            if (leftWing.transform.localEulerAngles.z < 40)
            {
                leftWing.transform.Rotate(Vector3.forward, -2, Space.Self);
                rightWing.transform.Rotate(Vector3.forward, 2, Space.Self);
            }
            else if (leftWing.transform.localEulerAngles.z > 320)
            {
                leftWing.transform.Rotate(Vector3.forward, 2, Space.Self);
                rightWing.transform.Rotate(Vector3.forward, -2, Space.Self);
            }
        }
        //Reset Rudder
        if (!flag["Rudder"] && Mathf.Abs(rudder.transform.localEulerAngles.y) >= 1)
        {
            if (rudder.transform.localEulerAngles.y < 40)
            {
                rudder.transform.Rotate(Vector3.up, -2, Space.Self);
            }
            else if (rudder.transform.localEulerAngles.y > 320)
            {
                rudder.transform.Rotate(Vector3.up, 2, Space.Self);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        elevatorAngle = elevator.transform.localEulerAngles.z;
        rudderAngle = rudder.transform.localEulerAngles.y;
        aileronAngle = leftWing.transform.localEulerAngles.z;
	}
    void FixedUpdate()
    {
        InputHandle();
        ResetCF();

        flag["Elevator"] = false;
        flag["Aileron"] = false;
        flag["Rudder"] = false;
    }
}
