using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   
    public Text puan, maxPuan,baslaText,video,reklamGizle;
    public Button bDevam;
    private bool devam = false,rklm=false,bnnr=true;
    private float bntTime = 0;
    private float reklamZamani;
    
    public void Basla()
    {
        if(!devam)
        PlayerPrefs.SetInt("puan",0);

        SceneManager.LoadScene(1);
    }

    public void cikis()
    {
      Application.Quit();
    }

    public void Devam()
    {
     ReklamScript.RewardedReklamGoster(this.reklamDevam);

    }

    void reklamDevam(GoogleMobileAds.Api.Reward odul)
    {
        
         if (odul.Amount>0)
        {
            ReklamScript.reklamTime = 0;
            devam = true;
            baslaText.text = "Recovery!";
        }
    }

    private void Start()
    {

        reklamZamani = (60 * 10);
        bDevam.enabled = false;
        devam = false;

        ReklamScript.BannerGoster();

        puan.text = "Your Last Points = " + PlayerPrefs.GetInt("puan");
        maxPuan.text = "Highest Points= " + PlayerPrefs.GetInt("maxPuan");
    }

    public void reklamlariGizle()
    {
        if (GameObject.FindGameObjectWithTag("reklam").GetComponent<ReklamScript>().enabled)
        {
            GameObject.FindGameObjectWithTag("reklam").GetComponent<ReklamScript>().enabled = false;
            ReklamScript.BannerGizle();
           reklamGizle.text = "Göster";

        }
        else
        {
            GameObject.FindGameObjectWithTag("reklam").GetComponent<ReklamScript>().enabled = true;
            ReklamScript.BannerGoster();
            reklamGizle.text = "Gizle";
            
        }
    }
    void Update()
    {
        if (ReklamScript.reklamTime <= reklamZamani) video.text = "Watch Video\n" + zaman(ReklamScript.reklamTime, reklamZamani);
        else bDevam.enabled = true;
    }

    string zaman(float a,float b)
    {
        float s = b - a;
        if (b != a) return (((int)s / 60) + " : " + ((int)s % 60));
        else return "00:00";

    }
}
