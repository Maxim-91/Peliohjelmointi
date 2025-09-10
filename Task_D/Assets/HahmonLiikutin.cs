using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HahmonLiikutin : MonoBehaviour
{
    public float liikeVoima = 5f;
    public float kaantoVoima = 2f;
    private Rigidbody hahmonRB;

    public Transform kannettavaEsine = null; // The object hahmo carry
    public Vector3 paikkaPaalla; // Where hahmo carry the object 

     [Header("UI asetukset")]
    public Image esineKuva; // The image component in the UI to show the carried object's photo
    public TextMeshProUGUI esineTeksti; // The text component in the UI to show the carried object's name    
    public Sprite ilmastointiteippiSprite; 
    public Sprite tyokalupakkiSprite;
    public Sprite sorkkarautaSprite;
    public Sprite taskulamppuSprite;

    void Start()
    {
        hahmonRB = GetComponent<Rigidbody>();
        
        // Starting parameters for the task "20' Kun pelaajalla on kannossa jokin esine, niin näytä UI:lla kuva..."
        if (kannettavaEsine == null)
        {
            esineKuva.enabled = false;
            esineTeksti.enabled = false;
        }
    }


    void Update()
    {
        // Hahmon liike
        // Eteen-/taaksepäin liike
        if (Input.GetKey(KeyCode.W)) hahmonRB.AddRelativeForce(Vector3.forward * liikeVoima);
        if (Input.GetKey(KeyCode.S)) hahmonRB.AddRelativeForce(Vector3.back * liikeVoima);

        // Käänny vasemmalle/oikealle
        if (Input.GetKey(KeyCode.A)) hahmonRB.AddRelativeTorque(Vector3.up * -kaantoVoima);
        if (Input.GetKey(KeyCode.D)) hahmonRB.AddRelativeTorque(Vector3.up * kaantoVoima);

        // Picking up an item/the object
        if (kannettavaEsine != null)
        {
            // carry an object over the head/back
            paikkaPaalla = transform.TransformPoint(Vector3.up * 2f); // Find the point the required point (2 units up)
            kannettavaEsine.position = paikkaPaalla; // Moving the item/object to paikkaPaalla   

            // Drop the item/object by pressing Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 dropPos = transform.TransformPoint(Vector3.forward * 1f);
                kannettavaEsine.position = dropPos;

                kannettavaEsine = null;

                if (esineKuva != null) esineKuva.enabled = false; // Hide object's photo
                if (esineTeksti != null) esineTeksti.enabled = false; // Hide object's name
            }      
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Checking whether an object can be lifted and whether the object is from the list of lifts
        if (kannettavaEsine == null &&
            (collision.gameObject.name.Contains("Ilmastointiteippi") ||
             collision.gameObject.name.Contains("tyokalupakki") ||
             collision.gameObject.name.Contains("sorkkarauta") ||
             collision.gameObject.name.Contains("taskulamppu")))
        {
            Debug.Log(collision.gameObject.name);
            kannettavaEsine = collision.transform;

            // Show the object's photo and name on the UI   
            if (esineKuva != null && esineTeksti != null)
            {
                esineKuva.enabled = true;
                esineTeksti.enabled = true;

                // Set the image size to 40x40
                RectTransform rt = esineKuva.GetComponent<RectTransform>();
                if (rt != null)
                    rt.sizeDelta = new Vector2(40, 40);

                if (collision.gameObject.name.Contains("Ilmastointiteippi"))
                {
                    esineKuva.sprite = ilmastointiteippiSprite;
                    esineTeksti.text = collision.gameObject.name.ToUpper();
                }
                else if (collision.gameObject.name.Contains("tyokalupakki"))
                {
                    esineKuva.sprite = tyokalupakkiSprite;
                    esineTeksti.text = collision.gameObject.name.ToUpper();
                }
                else if (collision.gameObject.name.Contains("sorkkarauta"))
                {
                    esineKuva.sprite = sorkkarautaSprite;
                    esineTeksti.text = collision.gameObject.name.ToUpper();
                }
                else if (collision.gameObject.name.Contains("taskulamppu"))
                {
                    esineKuva.sprite = taskulamppuSprite;
                    esineTeksti.text = collision.gameObject.name.ToUpper();
                }
            }        
        }
    }
}
