using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private AudioSource playderSoundEffect; 
    private float minSpeed = 12;
    private float maxSpeed = 15;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    public AudioClip breakBox;
    public ParticleSystem explosionPartical;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        playderSoundEffect = GetComponent<AudioSource>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPosition();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (!gameManager.isGameOver)
        {
            playderSoundEffect.PlayOneShot(breakBox, 1.0f);
            Destroy(gameObject);
            Instantiate(explosionPartical, transform.position, explosionPartical.transform.rotation);           
            gameManager.UpdateScore(pointValue);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good") && gameManager.isGameOver == false)
        {
            gameManager.UpdateScore(-10);
        }
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
