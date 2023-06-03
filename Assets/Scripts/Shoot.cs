using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Slider forceTime;
    public Transform shooter;
    private GameManager gameManager;
    private float rotation;
    private float rotationSpeed = 1f;

    private float minForce = 0f;
    private float maxForce = 10f;
    private float duration = 1f;    // increment force, reduce force duration
    public float currentForce = 1f;

    public bool ischarging = false;
    public bool isShoot = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        forceTime.gameObject.SetActive(false);

        forceTime.minValue = minForce;
        forceTime.maxValue = maxForce;
    }

    void Update()
    {
        if(!GameManager.pause)
        {
            Rotate();
            Forcechargeing();
            _Shoot();
        }
    }

    private void Rotate()   // Control shot angle
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.rotation += rotationSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.rotation -= rotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void _Shoot()
    {
        float playerForce = currentForce * 2f;  // Calculate forces shooting

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, shooter.position, transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.AddForce(transform.up * playerForce, ForceMode2D.Impulse);
                StartCoroutine(HasShoot());
            }
            currentForce = 1f;                  // Turn to default after successfully
            forceTime.gameObject.SetActive(false);
        }
    }

    private void Forcechargeing()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            ischarging = true;
        }

        if (Input.GetKeyUp(KeyCode.Space)){
            ischarging = false;
        }

        if (ischarging){

            forceTime.gameObject.SetActive(true);
            currentForce += duration * (Time.deltaTime * 10f);
            if (currentForce <= minForce || currentForce >= maxForce)      // increment force and runs back to min force
            {
                duration *= -1;
            }
            forceTime.value = currentForce;
        }
    }
    
    IEnumerator HasShoot()
    {
        yield return new WaitForSeconds(2f);
        isShoot = true;
    }
}
