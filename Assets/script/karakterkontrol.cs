using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class karakterkontrol : MonoBehaviour
{
    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Text canText;
    public Image SiyahArkaPlan;
    public Text altinText;
    int can = 20;
    SpriteRenderer spriteRenderere;
    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int altinSayaci = 0;
    Rigidbody2D fizik;
    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;
    float horizontal = 0;
    float beklemeAnimZaman=0;
    float yurumeAnimZaman = 0;
    float siyahArkaPlanSayaci = 0;
    bool birKereZipla=true;
    float anaMenuyeDonZaman = 0;
    GameObject kamera;
    void Start()
    {
        Time.timeScale = 1;
        SiyahArkaPlan.gameObject.SetActive(false);
        spriteRenderere = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacincilevel"))       
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }
       
        kameraIlkPos = kamera.transform.position - transform.position;
     
        canText.text = "CAN  " + can;
        altinText.text = " ALTIN   10 - "+altinSayaci;
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (birKereZipla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZipla = false;
            }
            
        }
       
    }
    void FixedUpdate()
    {
        karakterHareket();
        Animasyon();
        if (can<=0)
        {
            Time.timeScale = 0.5f;
            canText.enabled = false;

            siyahArkaPlanSayaci += 0.03f;
            SiyahArkaPlan.gameObject.SetActive(true);
            SiyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman>1)

            {
                SceneManager.LoadScene("anamenu"); 
            }
        }
    }
     void LateUpdate()
    {
        kameraKontrol(); 
    }

    void karakterHareket()
    {
        horizontal =CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal *8, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        birKereZipla = true;  

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag=="kursun")
        {
            can--;
            canText.text = "CAN  " + can;
        }
        if (col.gameObject.tag == "dusman")
        {
            can-=10;
            canText.text = "CAN  " + can;
        }
        if (col.gameObject.tag == "testere")
        {
            can-= 10;
            canText.text = "CAN  " + can;
        }
        if (col.gameObject.tag == "testereiki")
        {
            can=0;
           
        }
        if (col.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

        }

            if (col.gameObject.tag == "canver")
            {

                can += 10;
                canText.text = "CAN  " + can;
                col.GetComponent<BoxCollider2D>().enabled = false;
                col.GetComponent<canver>().enabled = true;
                Destroy(col.gameObject, 1.5f);


            }
        
        if (col.gameObject.tag == "altin")
        {
            
            altinSayaci++;
            altinText.text = "ALTIN   10-  "+altinSayaci;
            Debug.Log(altinSayaci);
            Destroy(col.gameObject);

        }
        if (col.gameObject.tag=="su")
        {
            can = 0;
        }
        

    }
    void kameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.08f);
    }
    void Animasyon()
    {
        if (birKereZipla)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.04f)
                {
                    spriteRenderere.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;
                }
            }


            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.1f)
                {
                    spriteRenderere.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.1f)
                {
                    spriteRenderere.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (fizik.velocity.y > 0)
            {
                spriteRenderere.sprite = ziplamaAnim[0];
            }
            else
            {
                spriteRenderere.sprite = ziplamaAnim[1];

            }

            if (horizontal>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
       
        }
    }
        

