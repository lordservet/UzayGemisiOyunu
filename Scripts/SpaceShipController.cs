using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    private int points = 0,puanSpeed;
    private Rigidbody2D fizik;

    public Text ppintsText;

    private bool gameOver = false;
    private GameController gameController;

    private AudioSource sound;
    public AudioClip fly,destroy,point;
    private void Start()
    {puanSpeed=0;
        points = PlayerPrefs.GetInt("puan");
        ppintsText.text = "Puan = " + points;
        fizik = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        sound = this.GetComponent<AudioSource>();
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            sound.clip = fly;
            sound.Play();
            fizik.velocity = Vector2.zero;
            fizik.AddForce(new Vector2(0, 350));

        }
        if (fizik.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(56, 45, -50);
        }
        else
        {
            transform.eulerAngles = new Vector3(56, -45, -130);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            gameOver = true;
            sound.clip = destroy;
            sound.Play();
            gameController.gameOver();
            this.GetComponent<EdgeCollider2D>().enabled = false;

            PlayerPrefs.SetInt("puan", points);

            if (points > PlayerPrefs.GetInt("maxPuan")) PlayerPrefs.SetInt("maxPuan", points);
            Invoke("returnMainMenu", 2);
        }
        if (col.gameObject.tag == "point")
        {
			puanSpeed++;
			gameController.enemySpeedUp(-((float)puanSpeed/500));
		gameController.gameSpeedUp(-((float)puanSpeed/500));
            points++;
            ppintsText.text = "Puan = " + points;
            sound.clip = point;
            sound.Play();
        }
    }

    private void returnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
