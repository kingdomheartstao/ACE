using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Engin : MonoBehaviour {
    public float power;
    float maxPower = 20;
    int throttle;
    int maxThrottle = 50;
    bool isStart = false;
    bool turning = false;
    GameObject LJet;
    GameObject RJet;

    IEnumerator StartCount()
    {
        turning = true;
        for (float timer = 0; timer < 1; timer += Time.deltaTime)
        {
            power = timer;
            yield return 0;
        }
        power = 1;
        isStart = true;
        turning = false;
    }

    IEnumerator CloseCount()
    {        turning = true;
        for (float timer = 0; timer < 1; timer += Time.deltaTime)
        {
            power = 1 - timer;
            yield return 0;
        }

        power = 0;
        isStart = false;
        turning = false;
    }

    public void TurnEngin() {
        if (turning)
            return;
        if (isStart)
        {
            StartCoroutine(CloseCount());
        }
        else
        {
            StartCoroutine(StartCount());
        }
    }

    public void ThrottleValeP()
    {
        if(throttle < maxPower)
            throttle++;
    }
    public void ThrottleValeM()
    {
        if (throttle > 1)
            throttle--;
    }

    float EnginWork(int m_throttle)
    {
        float power = 0;
        power = Mathf.Log(m_throttle) + 1;

        if (power <= maxPower)
            return power;
        else
            return maxPower;
    }

	// Use this for initialization
	void Start () {
        throttle = 1;
        power = 0;
        LJet = GameObject.Find("LeftJet");
        RJet = GameObject.Find("RightJet");
	}

    void Update ()
    {
        LJet.GetComponent<Light>().intensity = power;
        RJet.GetComponent<Light>().intensity = power;
    }

	void FixedUpdate () {
        if (isStart)
        {
            power = EnginWork(throttle);
        }
	}
}
