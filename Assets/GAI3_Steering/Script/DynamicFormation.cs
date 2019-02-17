using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFormation : MonoBehaviour
{
    public string formType;
    public float limit;
    private float timeDelay = 0;
    public GameObject unit;
    public GameObject place;
    private GameObject[] agent;
    private GameObject[] pos;
    public int size;
    public float radius;

    private GameObject anchor;
    // Use this for initialization
    void Start()
    {
        //inisialisasi untuk tempat anchor atau titik tengah sebuah formasi
        anchor = this.gameObject;
        //mendisable game object untuk delay
        anchor.GetComponent<SteerSeekArrive>().enabled = false;
        //Melakukan
        changeNumber();
    }

    void changeNumber()
    {
        agent = new GameObject[size];
        this.pos = new GameObject[size];
        //menyamakan tempat spawn dengan pusat formasi
        unit.transform.position = anchor.transform.position;

        //menclone agent dan menclone posisi agen di formasi
        for (int i = 0; i < size; i++)
        {
            agent[i] = Instantiate(unit) as GameObject;
            agent[i].GetComponent<SteerSeekColl>()._target = anchor.transform;
            //memasukkan clone object ke element array
            this.pos[i] = Instantiate(place) as GameObject;
            //memasukkan ke anak gameObject
            this.pos[i].transform.parent = anchor.transform;
        }
        //mengatur jenis formasi
        setFormation();
    }
    // Update is called once per frame
    void Update()
    {
        timeDelay += Time.deltaTime;
        
        if (timeDelay > limit)
        {
            anchor.GetComponent<SteerSeekArrive>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            formType = "circle";
            setFormation();
            Debug.Log("Press C");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            formType = "line";
            setFormation();
            Debug.Log("Press L");
        }

    }

    //setFormasi agent ke formasi yang dibutuhkan
    public void setFormation()
    {
        
        //case untuk pemilihan formasi
        switch (formType)
        {
            case "circle":
                {
                    int degree = 360 / size;
                    for (int i = 0; i < size; i++)
                    {
                        this.pos[i].transform.position = Vector3.zero;
                        //Debug.Log(degree * (i + 1));
                        Vector3 pos = RandomCircle(anchor.transform, radius, degree * (i));
                        this.pos[i].transform.position = pos;
                        //menset target agent
                        agent[i].GetComponent<SteerSeekColl>()._target = this.pos[i].transform;
                        //Debug.Log("Use Circle Formation");
                    }
                    break;
                }
            case "line":
                {
                    for (int i = 0; i < size; i++)
                    {
                        this.pos[i].transform.position = Vector3.zero;
                        //menentukan tempat agent
                        Vector3 pos = RandomLine(anchor.transform, radius, i);
                        this.pos[i].transform.position = pos;
                        //menset target agent
                        agent[i].GetComponent<SteerSeekColl>()._target = this.pos[i].transform;
                        //Debug.Log("Use line Formation");
                    }
                    break;
                }
            case "flying":
                {
                    
                    break;
                }
            default:
                {
                    break;
                }
        };
    }

    //method menentukan tempat sebuah agent di formasi lingkaran(Circle)
    Vector3 RandomCircle(Transform center, float radius, int a)
    {
        float ang = a;
        Vector3 poss;
        poss.x = center.position.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        poss.y = center.position.y;
        poss.z = center.position.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return poss;
    }

    //method menentukan tempat sebuah agent di formasi garis(Line)
    Vector3 RandomLine(Transform center, float radius, int a)
    {
        int ang = -(size/2);
        Vector3 poss;
        poss.x = center.position.x + radius * (ang + a);
        poss.y = center.position.y;
        poss.z = center.position.z;
        return poss;
    }
}