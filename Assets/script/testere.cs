using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class testere : MonoBehaviour
{
    public int resim;
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayacı=0;
    bool ilerimiGerimi = true;
 
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
      for(int i=0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }  
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        transform.Rotate(0, 0, 5);
        noktalaraGit();
    }
void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayacı].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayacı].transform.position);
        transform.position+=aradakiMesafe*Time.deltaTime*10;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true; ;
            if (aradakiMesafeSayacı == gidilecekNoktalar.Length - 1)
            {
                ilerimiGerimi = false;
            }
            else if (aradakiMesafeSayacı == 0)
            {
                ilerimiGerimi = true;
            }
            if (ilerimiGerimi)
            {
                aradakiMesafeSayacı++;
              
            }
            else
            {
                aradakiMesafeSayacı--;
            }
           
        }
      
        
        
    }


#if UNITY_EDITOR
   void OnDrawGizmos()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);
        }


    }

#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]
class testereEditor :Editor 
{
    public override void OnInspectorGUI()
    {
        testere script = (testere)target;
       if (GUILayout.Button("ÜRET",GUILayout.MinHeight(100),GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();

        }
    }
  
}
#endif
