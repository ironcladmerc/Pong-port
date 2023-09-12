using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public delegate void ScoreAction(string side);
    public static event ScoreAction OnScored;

    [SerializeField]
    private float speedX = 0;

    [SerializeField]
    private float speedY = 0;

    [SerializeField]
    private float yForceRange = 100;

    private Rigidbody2D playerRb;

    [SerializeField]
    float leftBound = 0;

    [SerializeField]
    float rightBound = 800;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        Vector2 force = new Vector2(speedX, speedY);

        playerRb.AddForce(force);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound)
        {
            if (OnScored != null)
            {
                OnScored("left");
            }

            Destroy(this.gameObject);
        } else if (transform.position.x > rightBound)
        {
            if (OnScored != null)
            {
                OnScored("right");
            }

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float yForce = Random.Range(-yForceRange, yForceRange);
        Vector2 force = new Vector2(0, yForce);
        playerRb.AddForce(force);
        audioSource.Play();
    }
}
