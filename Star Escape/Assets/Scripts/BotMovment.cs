using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotMovment : MonoBehaviour
{
    Ray RayZ = new Ray();
    Ray RayNZ = new Ray();
    RaycastHit hitZ;
    RaycastHit hitNZ;
    float time = 0;
    float positionSubstraction = 0;
    public string BotSpeed;
    public GameObject Player;
    int health;
    public Text healthText;
    public GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        health = 3; // временно!
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();

        RayZ.origin = transform.position;
        RayNZ.origin = transform.position;

        RayZ.direction = new Vector3(0, 0, 1);
        RayNZ.direction = new Vector3(0, 0, -1);

        Physics.Raycast(RayZ, out hitZ, 1);
        Physics.Raycast(RayNZ, out hitNZ, 1);

        Debug.DrawLine(RayZ.origin, hitZ.point);
        Debug.DrawLine(RayNZ.origin, hitNZ.point);

        positionSubstraction = Player.transform.position.z - transform.position.z;
        time += Time.deltaTime;
        float y = 360.0f / float.Parse(BotSpeed) * time;
        transform.rotation = Quaternion.Euler(0, y, 0);

        if (hitZ.distance == 0 && time > float.Parse(BotSpeed) && positionSubstraction > 0)
        {
            transform.position += new Vector3(0, 0, 1);
        }

        if (hitNZ.distance == 0 && time > float.Parse(BotSpeed) && positionSubstraction < 0)
        {
            transform.position += new Vector3(0, 0, -1);
        }

        if (time > float.Parse(BotSpeed))
        {
            time = 0;
        }
        if (health == 0)
        {
            deathScreen.SetActive(true);
        }
    }
    public void ChangeSpeed(Text textBox)
    {
        BotSpeed = textBox.text.ToString();
    }
    public void ChangeLifes(Text textBox)
    {
        health = int.Parse(textBox.text);
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("asdad");
    //    if (other.tag == "Player")
    //    {
    //        if (health != 0)
    //        {
    //            health--;
    //        }
    //    }
    //}
}
