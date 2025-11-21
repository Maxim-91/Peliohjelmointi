using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PallonliikutusKoodi : MonoBehaviour
{
    // Paikalliset muuttujat ovat luokan muuttujia
    GameObject jalkapallo;
    GameObject kohde1;
    GameObject kohde2;

    void Start()
    {
        // Etsi scriptin "Start"-metorissa kaikki GameObjektit  paikallisiksi muuttujiksi "PallonliikutusKoodi"-scriptiin
        jalkapallo = GameObject.Find("Jalkapallo");
        kohde1 = GameObject.Find("Maali1");
        kohde2 = GameObject.Find("Maali2");
    }    
        void Update()
    {
        // Laita pallo liikkumaan maali1 ja maali2 väliä käyttäen Vector3.Lerp() metodia
        float t = Mathf.PingPong(Time.time, 1f);
        jalkapallo.transform.position = Vector3.Lerp(kohde1.transform.position, kohde2.transform.position, t);
    }
}

