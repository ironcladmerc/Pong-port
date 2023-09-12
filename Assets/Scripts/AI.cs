using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    float topBound = 560;

    [SerializeField]
    float bottomBound = 40;

    [SerializeField]
    float paddleSpeed = 5;

    private Ball ballPrefab;

    private bool isGameOver = false;

    private void OnEnable()
    {
        GameManager.OnGameOver += this.OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= this.OnGameOver;
    }

    private void OnGameOver()
    {
        isGameOver = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ballPrefab = GameObject.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (ballPrefab == null)
            {
                ballPrefab = GameObject.FindObjectOfType<Ball>();
            }

            if (ballPrefab != null)
            {
                float ballCenterY = ballPrefab.GetComponent<SpriteRenderer>().bounds.center.y;
                float centerY = GetComponent<SpriteRenderer>().bounds.center.y;

                if (centerY < ballCenterY)
                {
                    float newYPos = transform.position.y + (paddleSpeed * Time.deltaTime);
                    transform.position = new Vector2(transform.position.x, newYPos);
                }

                if (centerY > ballCenterY)
                {
                    float newYPos = transform.position.y - (paddleSpeed * Time.deltaTime);
                    transform.position = new Vector2(transform.position.x, newYPos);
                }
                
            }

            if (transform.position.y > topBound)
            {
                transform.position = new Vector2(transform.position.x, topBound);
            }
            else if (transform.position.y < bottomBound)
            {
                transform.position = new Vector2(transform.position.x, bottomBound);
            }
        }
        
    }
}
