using UnityEngine;

public class KentanAsetin : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject ilmastointiteippi = GameObject.Find("Ilmastointiteippi");
        GameObject tyokalupakki = GameObject.Find("tyokalupakki");
        GameObject sorkkarauta = GameObject.Find("sorkkarauta");
        GameObject taskulamppu = GameObject.Find("taskulamppu");

        ilmastointiteippi.transform.position = new Vector3(-7, 0, 7);
        tyokalupakki.transform.position = new Vector3(6, 0, -7);
        sorkkarauta.transform.position = new Vector3(-7, 1, -7);
        taskulamppu.transform.position = new Vector3(7, 1, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
