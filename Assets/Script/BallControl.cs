using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text Zaman, Life,Durum;
    private Rigidbody rg;
    public float Hiz=1.5f;
    float TimeCounter = 16;
    int LifeCounter = 4;
    bool oyunDevam = true;
    bool oyunTamam = false;
    // Start is called before the first frame update
    void Start()
    {
        Life.text = LifeCounter + "";
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            TimeCounter -= Time.deltaTime; // TimeCounter=TimeCounter- Time.deltaTime;
            Zaman.text = (int)TimeCounter + "";
        } 
        else if (!oyunTamam)
        {
            Durum.text = "Game Over";
            btn.gameObject.SetActive (true);
        }
        if (TimeCounter < 0)
            oyunDevam = false;
    }
     void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * Hiz);
        } else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }
     void OnCollisionEnter(Collision cls)
    {
        string objIsmi = cls.gameObject.name;
        if(objIsmi.Equals("Finish"))
        {
            //print("You win!");
            oyunTamam = true;
            Durum.text = "You Win!";
            btn.gameObject.SetActive(true);
        }
        else if(!objIsmi.Equals("GameFloor") && !objIsmi.Equals("Floor") )
        {
            LifeCounter -= 1;
            Life.text = LifeCounter + "";
            if (LifeCounter == 0)
                oyunDevam = false;
        }
    }
}
