using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour {

    public GameObject atasA;
    public GameObject bawahA;
    public GameObject kiriA;
    public GameObject kananA;
    public Transform atas;
    public Transform bawah;
    public Transform kiri;
    public Transform kanan;
    public string formType;
    public float limit = 5;
    private float timeDelay = 0;

    public GameObject awal;
    // Use this for initialization
    void Start () {
        awal.GetComponent<SteerSeekArrive>().enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        timeDelay += Time.deltaTime;
        setFormation();
        if (timeDelay > limit)
        {
            awal.GetComponent<SteerSeekArrive>().enabled = true;
        }
    }

    //setFormasi agent ke formasi yang dibutuhkan
    public void setFormation()
    {
        switch (formType)
        {
            case "v":
                {

                    break;
                }
            case "o":
                {

                    break;
                }
            case "k":
                {
                    atasA.GetComponent<SteerSeekColl>()._target = atas;
                    bawahA.GetComponent<SteerSeekColl>()._target = bawah;
                    kiriA.GetComponent<SteerSeekColl>()._target = kiri;
                    kananA.GetComponent<SteerSeekColl>()._target = kanan;
                    break;
                }
            default:
                {
                    break;
                }
        };
    }
}
