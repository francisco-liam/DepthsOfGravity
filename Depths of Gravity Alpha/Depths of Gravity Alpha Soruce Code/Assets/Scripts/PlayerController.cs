using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    public int health;
    public float damageTimer;
    public float iTime;
    public GameObject heart2;
    public GameObject heart3;
    public int ammo;
    public GameObject coal;
    public AudioSource gravSound;
    public AudioSource hurtSound;
    public TextMeshProUGUI ammoText;
    public int rayHitRange;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        iTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, velocity * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, 0, velocity * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
        }

        float gravY = Physics.gravity.y;
        //Debug.Log(gravY);
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Physics.gravity = new Vector3(1, -gravY, 1);
            gravSound.Play();
        }

        Quaternion targetRotation;
        if (gravY == 9.81f)
            targetRotation = Quaternion.Euler(0, 0, 180);
        else
            targetRotation = Quaternion.Euler(0, 0, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0)
        {
            Vector3 lookPos;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, rayHitRange))
            {
                lookPos = hit.point;
                Debug.Log("hit");
            }
            else
            {
                lookPos = Input.mousePosition;
                lookPos.z = rayHitRange;
                lookPos = Camera.main.ScreenToWorldPoint(lookPos);
                Debug.Log("didn't");
                Debug.Log(lookPos);
            }

            GameObject proj = Instantiate(coal, transform.position + new Vector3(0,0,1), Quaternion.identity);
            Physics.IgnoreCollision(proj.GetComponent<Collider>(), GetComponent<Collider>());


            Rigidbody rb = proj.GetComponent<Rigidbody>();
            Vector3 direction = lookPos - transform.position;

            direction.Normalize();

            rb.AddForce(direction * 1000f);
            ammo--;
            ammoText.text = ammo + "/10";
        }

        if (damageTimer < iTime)
            damageTimer += Time.deltaTime;

        if (damageTimer > iTime)
            damageTimer = iTime;

        health = Mathf.Clamp(health, 0, 3);

        //healthText.text = "Health: " + health + "/3";

        if(health <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Damaging")) 
        {
            hurtSound.Play();
            
            if(damageTimer == iTime)
            {
                health--;
                damageTimer = 0;
            }
            if(health == 2)
            {
                heart3.SetActive(false);
            }
            if (health == 1)
            {
                heart2.SetActive(false);
            }
        }
        if (collision.collider.CompareTag("Gem")) 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("LevelSelect");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            if(ammo<10)
                ammo++;
            ammoText.text = ammo + "/10";
        }
    }

    public void load()
    {
        StartCoroutine(loadLevelSelect());
    }

    public IEnumerator loadLevelSelect()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("LevelSelect");
    }
}
