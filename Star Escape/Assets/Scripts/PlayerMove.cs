using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    Ray RayX = new Ray();
    Ray RayNX = new Ray();
    Ray RayZ = new Ray();
    Ray RayNZ = new Ray();
    RaycastHit hitX;
    RaycastHit hitNX;
    RaycastHit hitZ;
    RaycastHit hitNZ;
    float time, timeDeathZone;
    public GameObject model1, model2;
    int health;
    public Text healthText;
    public GameObject deathScreen;
    bool dead = false, firstHit = true;
    public GameObject EvilSmile, StarDust;

    void Start()
    {
        health = 3; // по дефолту, убрать надо потом
    }
    IEnumerator TimeDeathZoneCounter()
    {
        while (true)
        {
            timeDeathZone += Time.deltaTime; yield return null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Moon")
        {
            StartCoroutine(TimeDeathZoneCounter());
            if ((health != 0 && timeDeathZone > 30) || firstHit)
            {
                health--;
                timeDeathZone = 0;
                firstHit = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Moon")
        {
            timeDeathZone = 0;
            firstHit = true;
            StopAllCoroutines();
        }
    }

    void Update()
    {
        Debug.Log(timeDeathZone);
        healthText.text = health.ToString();
        if (health == 0)
        {
            deathScreen.SetActive(true);
            EvilSmile.SetActive(true);
            EvilSmile.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            StopAllCoroutines();
            dead = true;
            StarDust.SetActive(false);
        }
        ////// set Position
        RayX.origin = transform.position;
        RayNX.origin = transform.position;
        RayZ.origin = transform.position;
        RayNZ.origin = transform.position;

        ////// set direction
        RayX.direction = new Vector3(1, 0, 0);
        RayNX.direction = new Vector3(-1, 0, 0);
        RayZ.direction = new Vector3(0, 0, 1);
        RayNZ.direction = new Vector3(0, 0, -1);

        ////// set Size
        Physics.Raycast(RayX, out hitX, 1);
        Physics.Raycast(RayNX, out hitNX, 1);
        Physics.Raycast(RayZ, out hitZ, 1);
        Physics.Raycast(RayNZ, out hitNZ, 1);


        Debug.DrawLine(RayZ.origin, hitZ.point);
        Debug.DrawLine(RayNZ.origin, hitNZ.point);
        Debug.DrawLine(RayX.origin, hitX.point);
        Debug.DrawLine(RayNX.origin, hitNX.point);

        if (!dead)
        {
            if (hitNX.distance == 0 && Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            if (hitZ.distance == 0 && Input.GetKeyDown(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, 1);
            }
            if (hitX.distance == 0 && Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0);
            }
            if (hitNZ.distance == 0 && Input.GetKeyDown(KeyCode.S))
            {
                transform.position += new Vector3(0, 0, -1);
            }
        }

        time += Time.deltaTime;
        float y = 360.0f / 2.0f * time;
        transform.rotation = Quaternion.Euler(0, y, 0);
        if (time > 2)
            time = 0;
    }
    public void ChangeLifes(Text textBox)
    {
        health = int.Parse(textBox.text);
    }

    public void ChangeColor(string color)
    {
        switch (color)
        {
            case "Red":
                model1.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                model2.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                break;
            case "Blue":
                model1.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                model2.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                break;
            case "Green":
                model1.GetComponent<MeshRenderer>().materials[0].color = Color.green;
                model2.GetComponent<MeshRenderer>().materials[0].color = Color.green;
                break;
            case "Black":
                model1.GetComponent<MeshRenderer>().materials[0].color = Color.black;
                model2.GetComponent<MeshRenderer>().materials[0].color = Color.black;
                break;
            case "Yellow":
                model1.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                model2.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                break;
        }
    }
}
