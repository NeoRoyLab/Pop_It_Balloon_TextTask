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
        if ((CanPlay||inMainMenu)&&noCoroutine) //���� ��������� � ���� ��� ������ - ������ ���������
        {
            
            StartCoroutine(SpawnBaloon());
            if (CanPlay) //���� � ����
            {
                balloonSpeed += 0.03f; //����������� ���������� �������� ����� �����
                frequency = 1 - balloonSpeed/(balloonMaxSpeed+1); //����������� ������� ������ �����
                
            }
        }
    }
    IEnumerator SpawnBaloon() //������� ������ �������
    {
        float randomX;
        randomX = Random.Range(-3.1f, 3.1f); // ��������� �������� ������ �� ��� �
        SpawnZone = new Vector3(randomX, -2.5f, 0); //����� ������ ������
        GameObject newBalloon = Instantiate(BalloonPref,
                                            SpawnZone,
                                            Quaternion.identity,
                                            balloons) as GameObject; //�������� ������

        noCoroutine = false; //��������� ����������� ������ ������
        yield return new WaitForSeconds(frequency); //���� ����������� �����
        noCoroutine = true; //�������� ����������� ������ ������
    }

    public void PlayBtn() //������ Play � ����
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

    public void ExitBtn() //����� �� ����
    {
        Application.Quit();
    }

    public void SetDefault() //��������� �������� ��� ��� ������� ����
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
    public void DeleteAllBalloons() //������� �������� ���� ����� � ������
    {
        for(int i = 0; i<balloons.childCount; i++) // ���������� ��� �������� ������� balloons � ������� ��
        Destroy(balloons.GetChild(i).gameObject);
    }

    public void AddCoin(int number) //�������� �������
    {
        coins += number;
    }
    public void AddDiamond(int number) //�������� ������
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
