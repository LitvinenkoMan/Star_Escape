using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotate : MonoBehaviour
{
    float dayTime;
    IEnumerator TimeCounter()
    {
        while (true)
        {
            dayTime += Time.deltaTime;
            if (dayTime > 1440f)
            {
                dayTime = 0.1f;
            }

            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeCounter());
    }

    // Update is called once per frame
    void Update()
    {
        float y = 360.0f / 1440.0f * dayTime;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
