using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject bg1, bg2;
    private float uzunluk;
    private Rigidbody2D f1, f2;

    private float enemyTimeCount, bgSpeed;

    public GameObject enemy;
    public int enemyCount = 5;
    private GameObject[] enemys;
    private bool ilk = true;
    private float dTime = 0;
    private int syc = 0;

    private void Start()
    {
        enemyTimeCount = 5f;
        bgSpeed = -5f;
        f1 = bg1.GetComponent<Rigidbody2D>();
        f2 = bg2.GetComponent<Rigidbody2D>();
        f1.velocity = new Vector2(bgSpeed, 0);
        f2.velocity = new Vector2(bgSpeed, 0);
        uzunluk = f1.GetComponent<BoxCollider2D>().size.y;

        enemys = new GameObject[enemyCount];
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i] = Instantiate(enemy, new Vector2(-20, -20), Quaternion.identity);
            enemys[i].GetComponent<Rigidbody2D>().velocity = new Vector2(bgSpeed, 0);
        }
        Invoke("bannerGizle", 2);

    }

    private void bannerGizle()
    {
        ReklamScript.banner = false;
    }

    private void Update()
    {
        if (bg1.transform.position.x <= -uzunluk) bg1.transform.position += new Vector3(uzunluk * 2, 0);
        if (bg2.transform.position.x <= -uzunluk) bg2.transform.position += new Vector3(uzunluk * 2, 0);
        //
        if (ilk)
        {
            ilk = false;
            enemys[syc].transform.position = new Vector3(52, Random.Range(4, 16));
            syc++;
        }
        dTime += Time.deltaTime;
        if (dTime >= enemyTimeCount)
        {
            dTime = 0;
            enemys[syc].transform.position = new Vector3(52, Random.Range(4, 16));
            syc++;
            if (syc == enemys.Length) syc = 0;
        }

    }

    public void enemySpeedUp(float a)
    {
        if (enemyTimeCount > 1) enemyTimeCount += a;
    }

    public void gameSpeedUp(float a)
    {
        if (bgSpeed > -15) bgSpeed += a;
    }

    public void gameOver()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        f1.velocity = f2.velocity = Vector2.zero;

    }
}
