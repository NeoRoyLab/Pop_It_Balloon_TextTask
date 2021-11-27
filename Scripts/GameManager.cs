using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int coins;
    private int diamonds;

    public AudioClip popSound;
    public AudioSource AS, BGs;
    public bool CanPlay = false,
                inMainMenu = true,
                isMusic;
    bool noCoroutine = true;

    [Range(0f, 10f)]
    public float balloonSpeed;
    public float balloonMaxSpeed,
                 frequency;
    

    public int score;
    public int bestRecord;

    public GameObject mainMenu,
                      UI,
                      ResultObj;

    public GameObject BalloonPref;

    private Vector3 SpawnZone;
    public Transform SpawnPoint;

    public Transform balloons;

    public Text Score,
                BestScore,
                Money,
                Diamonds;

    void Update()
    {
        if ((CanPlay||inMainMenu)&&noCoroutine) //если находимся в меню или играем - шарики спавнятся
        {
            
            StartCoroutine(SpawnBaloon());
            if (CanPlay) //если в игре
            {
                balloonSpeed += 0.03f; //увеличиваем постепенно скорость полёта шаров
                frequency = 1 - balloonSpeed/(balloonMaxSpeed+1); //увеличиваем частоту спавна шаров
                
            }
        }
    }
    IEnumerator SpawnBaloon() //функция спавна шариков
    {
        float randomX;
        randomX = Random.Range(-3.1f, 3.1f); // рандомное значение шарика по оси Х
        SpawnZone = new Vector3(randomX, -2.5f, 0); //точка спавна шарика
        GameObject newBalloon = Instantiate(BalloonPref,
                                            SpawnZone,
                                            Quaternion.identity,
                                            balloons) as GameObject; //создание шарика

        noCoroutine = false; //отключаем возможность спавна шарика
        yield return new WaitForSeconds(frequency); //ждем определённое время
        noCoroutine = true; //включаем возможность спавна шарика
    }

    public void PlayBtn() //кнопка Play в меню
    {
        DeleteAllBalloons();
        balloonSpeed = 3f;
        CanPlay = true;
        mainMenu.SetActive(false);
        inMainMenu = false;
        UI.SetActive(true);
        ResultObj.SetActive(false);
        ResetText();

    }

    public void ExitBtn() //выход из игры
    {
        Application.Quit();
    }

    public void SetDefault() //установка значений как при запуске игры
    {
        CanPlay = false;
        inMainMenu = true;
        mainMenu.SetActive(true);
        UI.SetActive(false);
        balloonSpeed = 2f;
        frequency = 1f;
        score = 0;
        ResultObj.SetActive(false);
        ResetText();
        DeleteAllBalloons();
    }
    public void DeleteAllBalloons() //функция удаления всех шаров с экрана
    {
        for(int i = 0; i<balloons.childCount; i++) // перебираем все дочерние обьекты balloons и удаляем их
        Destroy(balloons.GetChild(i).gameObject);
    }

    public void AddCoin(int number) //добавить монеток
    {
        coins += number;
    }
    public void AddDiamond(int number) //добавить алмазы
    {
        diamonds += number;
    }

    public void ResetText()
    {
        Score.text = score.ToString();
        BestScore.text = "Best " + bestRecord.ToString();
        //Money.text = coins.ToString();
        //Diamonds.text = diamonds.ToString();
    }

    public void MusicBtn()
    {
        if (isMusic)
        {
            BGs.Pause();
        }
        else
        {
            BGs.UnPause();
        }
        isMusic = !isMusic;
    }
    public void ShowResult()
    {
        ResultObj.SetActive(true);
        CanPlay = false;
        if (score > bestRecord)
        {
            bestRecord = score;
        }
            ResetText();
        score = 0;
        
    }
}
