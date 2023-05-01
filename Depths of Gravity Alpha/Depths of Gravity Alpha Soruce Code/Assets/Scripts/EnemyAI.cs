using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //world variables
    public GameObject player;
    public GameObject projectilePrefab;
    public AudioSource spitSound;

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
    Quaternion initRotation;


    // Start is called before the first frame update
    void Start()
    {
        shootTimer = 0;
        bufferTime = Random.Range(0.5f, 1f);
        initToFin = true;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        distance = transform.position - player.transform.position;

        if (distance.magnitude < 7.5)
        {
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

            if (shootTimer == bufferTime)
            {
                spitSound.Play();
                enemyProjectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
                enemyProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
                bufferTime = Random.Range(3f, 5f);
                shootTimer = 0;
                //spitSound.Stop();
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initRotation, 1 * Time.deltaTime);
        }

        shootTimer += Time.deltaTime;

        if (shootTimer > bufferTime)
        {
            shootTimer = bufferTime;
        }

        MoveBetweenPositions();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player Projectile"))
            Destroy(gameObject);
    }

    void MoveBetweenPositions()
    {
        if (lerpTimer < oneWayTime)
        {
            if (initToFin)
            {
                float newX = Mathf.SmoothStep(initialPos.x, finalPos.x, lerpTimer / oneWayTime);
                float newY = Mathf.SmoothStep(initialPos.y, finalPos.y, lerpTimer / oneWayTime);
                float newZ = Mathf.SmoothStep(initialPos.z, finalPos.z, lerpTimer / oneWayTime);
                transform.position = new Vector3(newX, newY, newZ);
            }
            else
            {
                float newX = Mathf.SmoothStep(finalPos.x, initialPos.x, lerpTimer / oneWayTime);
                float newY = Mathf.SmoothStep(finalPos.y, initialPos.y, lerpTimer / oneWayTime);
                float newZ = Mathf.SmoothStep(finalPos.z, initialPos.z, lerpTimer / oneWayTime);
                transform.position = new Vector3(newX, newY, newZ);
            }

            lerpTimer += Time.deltaTime;
        }
        else
        {
            lerpTimer = 0f;
            if (initToFin)
            {
                initToFin = false;
            }
            else
            {
                initToFin = true;
            }
        }
    }
}


