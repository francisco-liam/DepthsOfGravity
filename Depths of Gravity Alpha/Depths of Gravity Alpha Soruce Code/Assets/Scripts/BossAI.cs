using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    //world variables
    public GameObject player;
    public GameObject projectilePrefab;
    public GameObject firePrefab;
    public AudioSource spitSound;
    public BossHealthBar healthBar;

    //fire
    float fireY;
    float fireTimer;
    public float fireBuffer;
    int fireCount;

    //health
    int health;
    public int maxHealth;

    //phase variables
    public int phaseNum;
    int phaseTwo;
    int phaseThree;
    

    //projectile variables
    public int projectileSpeed;
    GameObject enemyProjectile;
    Vector3 distance;
    float shootTimer;
    float bufferTime;

    //movement variables
    float lerpTimer = 0.0f;
    public float oneWayTime;
    public Vector3 initialPos;
    public Vector3 finalPos;
    bool initToFin;


    // Start is called before the first frame update
    void Start()
    {
        shootTimer = 0;
        bufferTime = Random.Range(0.5f, 1f);
        initToFin = true;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        fireY = 2;
        phaseTwo = maxHealth * 2 / 3;
        phaseThree = maxHealth * 1 / 3;
        initialPos = transform.position;
        finalPos = transform.position + new Vector3(10, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFire();

        if (phaseNum>1)
            PhaseTwo();
        if (phaseNum==3)
            MoveBetweenPositions();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player Projectile")) 
        { 
            health--;
            healthBar.SetHealth(health);

            if (health < phaseThree)
                phaseNum = 3;
            else if (health < phaseTwo)
                phaseNum = 2;
           

            if(health <= 0)
            {
                Destroy(gameObject);
                player.GetComponent<PlayerController>().load();
            }
                
        }
    }

    private void SpawnFire()
    {

        if (fireTimer > fireBuffer)
        {
            for (int i = -4; i <= 24; i += 2)
            {
                Instantiate(firePrefab, new Vector3(i, fireY, -3), Quaternion.identity);
            }

            fireTimer = 0;
            fireCount++;
        }

        if (fireCount > 2)
        {
            fireCount = 0;
            if (fireY == 2)
                fireY = 12;
            else
                fireY = 2;
        }
            

        fireTimer += Time.deltaTime;
    }

    private void PhaseTwo()
    {
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        if (shootTimer >= bufferTime)
        {
            spitSound.Play();
            enemyProjectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
            Physics.IgnoreCollision(enemyProjectile.GetComponent<Collider>(), GetComponent<Collider>());
            enemyProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
            bufferTime = Random.Range(0.5f, 1.5f);
            shootTimer = 0;
        }

        shootTimer += Time.deltaTime;
    }

    void MoveBetweenPositions()
    {
        if (lerpTimer < oneWayTime)
        {
            float newX = Mathf.SmoothStep(initialPos.x, finalPos.x, lerpTimer / oneWayTime);
            float newY = Mathf.SmoothStep(initialPos.y, finalPos.y, lerpTimer / oneWayTime);
            float newZ = Mathf.SmoothStep(initialPos.z, finalPos.z, lerpTimer / oneWayTime);
            transform.position = new Vector3(newX, newY, newZ);

            lerpTimer += Time.deltaTime;
        }
        else
        {
            lerpTimer = 0f;
            if (initToFin)
            {
                initialPos = finalPos;
                finalPos -= new Vector3(20, 0, 0);
                initToFin = false;
            }
            else
            {
                initialPos = finalPos;
                finalPos += new Vector3(20, 0, 0);
                initToFin = true;
            }
        }
    }
}


