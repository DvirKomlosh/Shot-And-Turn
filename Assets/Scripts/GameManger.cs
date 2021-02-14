using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;


public class GameManger : MonoBehaviour
{
    public GameObject onGameUI;
    public GameObject GameOverScreen;
    public GameObject shopScreen;
    public TextMeshProUGUI textScore;
    public Text bestScore;
    public Text endScore;
    public TextMeshProUGUI gemsOnPlay;
    public Text gemsOnMenu;
    public int Score = 0;
    public int gems;
    public int highScore;
    public static Data data;
    bool isGameOver;

    //SHOP
    int[] bulletNumPrice = new[] { 100, 200, 400, 800, 1600, 3200, 6400, 12800, 10000000};
    int[] bulletSpeedPrice = new[] { 20, 50, 100, 200, 400, 800, 1600, 3200, 10000000 };
    int[] movementSpeedPrice = new[] { 20, 50, 100, 200, 400, 800, 1600, 3200, 10000000 };
    int[] turningSpeedPrice = new[] { 20, 50, 100, 200, 400, 800, 1600, 3200, 10000000 };

    public int[] bulletNumAmaount = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    public int[] bulletSpeedAmaount = new[] { 50, 70, 90, 110, 130, 150, 200, 250 };
    public int[] movementSpeedAmaount = new[] { 500, 480, 450, 420, 390, 360, 330, 300};
    public int[] turningSpeedAmaount = new[] { 50, 60, 70, 80, 90, 120, 150, 200 };
    

    public GameObject bulletNumUI;
    public GameObject bulletSpeedUI;
    public GameObject movementSpeedUI;
    public GameObject turningSpeedUI;
    
    public GameObject maxedError;
    public GameObject insufficientGemsError;
    //




    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerCollision>().OnPlayerDeath += OnGameOver;
        data = SaveSystem.LoadData();
        if (data != null)
        {
           
            FindObjectOfType<Gun>().SetBulletNum(data.bulletNum, data.bulletSpeed);
            FindObjectOfType<PlayerMovment>().SetPlayerMovement(data.turnSpeed, data.movementSpeed);
            

            highScore = data.highScore;
            gems = data.gems;
        }
        else 
        {
            Debug.Log("NO DATA!");
            data = new Data(0, 0, 0, 0, 0, 0);
            SaveSystem.SaveData(0, 0, 0, 0, 0, 0);
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {

        }
        else 
        {
            OnGameUI();
        }

    }

    public void RestartGame() 
    {
        if (isGameOver)
        {
            SceneManager.LoadScene(0);
        }
    }
    
    /*[MenuItem("MyMenu/Reset Data")]
    public static void ResetData() 
    {
        data = new Data(0, 100000 , 0, 0, 0, 0);
        SaveSystem.SaveData(0, 100000, 0, 0, 0, 0);
    }

    [MenuItem("MyMenu/add 10000 gems")]
    public static void AddGems()
    {
        data.gems += 10000;
        SaveSystem.SaveData(data);
    }*/


    void OnGameUI() 
    {
        textScore.text = Score.ToString();
        gemsOnPlay.text = gems.ToString();
    }

    public void addScore(int score) 
    {
        Score += score;
        gems += score * 10;
    }

    void OnGameOver()
    {
        onGameUI.SetActive(false);
        GameOverScreen.SetActive(true);
        shopScreen.SetActive(false);
        if (Score > data.highScore) 
        {
            data.highScore = Score;
            highScore = Score;
        }

        endScore.text ="Score \n" + Score.ToString();
        bestScore.text = "Highscore \n" +highScore.ToString();
        gemsOnMenu.text = gems.ToString() + "Gems";
        Score = 0;
        isGameOver = true;

        data.gems = gems;

        SaveSystem.SaveData(data);

    }

    public void mainMenu() 
    {
        onGameUI.SetActive(false);
        GameOverScreen.SetActive(true);
        shopScreen.SetActive(false);

        gemsOnMenu.text = gems.ToString() + "Gems";
    }

    public void openShop() 
    {
        GameOverScreen.SetActive(false);
        shopScreen.SetActive(true);

        TextMeshProUGUI gemsAmaount = shopScreen.GetComponentInChildren<TextMeshProUGUI>();
        gemsAmaount.text = gems.ToString();

        TextMeshProUGUI bulletNumText = bulletNumUI.GetComponentInChildren<TextMeshProUGUI>();
        bulletNumText.text = bulletNumPrice[data.bulletNum].ToString();

        TextMeshProUGUI bulletSppedText = bulletSpeedUI.GetComponentInChildren<TextMeshProUGUI>();
        bulletSppedText.text = bulletSpeedPrice[data.bulletSpeed].ToString();

        TextMeshProUGUI movementSpeedText = movementSpeedUI.GetComponentInChildren<TextMeshProUGUI>();
        movementSpeedText.text = movementSpeedPrice[data.movementSpeed].ToString();

        TextMeshProUGUI turningSpeedText = turningSpeedUI.GetComponentInChildren<TextMeshProUGUI>();
        turningSpeedText.text = turningSpeedPrice[data.turnSpeed].ToString();
    }

    public void buyBulletNum()
    {

        if (data.bulletNum >= 7)
        {
            StartCoroutine(displayError(maxedError));
        }
        else if (data.gems >= bulletNumPrice[data.bulletNum])
        {
            data.gems -= bulletNumPrice[data.bulletNum];
            gems -= bulletNumPrice[data.bulletNum];
            data.bulletNum++;
            SaveSystem.SaveData(data);

            openShop();
        }
        else
        {
            StartCoroutine(displayError(insufficientGemsError));
        }
    }

    public void buyBulletSpeed()
    {

        if (data.bulletSpeed >= 7)
        {
            StartCoroutine(displayError(maxedError));
        }
        else if (data.gems >= bulletSpeedPrice[data.bulletSpeed])
        {
            data.gems -= bulletSpeedPrice[data.bulletSpeed];
            gems -= bulletSpeedPrice[data.bulletSpeed];
            data.bulletSpeed++;
            SaveSystem.SaveData(data);

            openShop();
        }
        else
        {
            StartCoroutine(displayError(insufficientGemsError));
        }
    }

    public void buyMovementSpeed()
    {

        if (data.movementSpeed >= 7)
        {
            StartCoroutine(displayError(maxedError));
        }
        else if (data.gems >= movementSpeedPrice[data.movementSpeed])
        {
            data.gems -= movementSpeedPrice[data.movementSpeed];
            gems -= movementSpeedPrice[data.movementSpeed];
            data.movementSpeed++;
            SaveSystem.SaveData(data);

            openShop();
        }
        else
        {
            StartCoroutine(displayError(insufficientGemsError));
        }
    }

    public void buyTurningSpeed()
    {

        if (data.turnSpeed >= 7)
        {
            StartCoroutine(displayError(maxedError));
        }
        else if (data.gems >= turningSpeedPrice[data.turnSpeed])
        {
            data.gems -= turningSpeedPrice[data.turnSpeed];
            gems -= turningSpeedPrice[data.turnSpeed];
            data.turnSpeed++;
            SaveSystem.SaveData(data);

            openShop();
        }
        else
        {
            StartCoroutine(displayError(insufficientGemsError));
        }
    }

    IEnumerator displayError(GameObject textObject) 
    {
        textObject.SetActive(true);

        yield return new WaitForSeconds(3);

        textObject.SetActive(false);
    }

}