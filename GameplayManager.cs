using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    [System.Serializable]
    public class PlayerInfo
    {
        public string name;
        public string country;
    }

    [System.Serializable]
    public class PlayerInfoList
    {
        public PlayerInfo[] infoList;
    }

    public GameObject[] randomPoints;

    public List<GameObject> civilCars;
    public List<GameObject> players;
    public TextAsset namesJsonFile;

    public GameObject GameOverPanel;
    public TextMeshProUGUI winnerNameText;

    public GameObject player;

    //For UI
    public GameObject PlayerListPanel;
    public List<GameObject> PlayerBoardList;

    public bool gameOver;
    public float timer;
    public TextMeshProUGUI timerText;

    public GameObject tutorialPanel;

    private void Awake()
    {
        instance = this;

        randomPoints = GameObject.FindGameObjectsWithTag("area");
        civilCars = new List<GameObject>();
        players = new List<GameObject>();
        PlayerBoardList = new List<GameObject>();

    }

    // Start is called bsefore the first frame update
    void Start()
    {
        foreach(GameObject car in GameObject.FindGameObjectsWithTag("civil_car"))
        {
            civilCars.Add(car);
        }
        foreach(GameObject car in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(car);
        }
        foreach (Transform tag in PlayerListPanel.transform)
        {
            PlayerBoardList.Add(tag.gameObject);
        }

        PlayerInfoList PlayerDetailList = JsonUtility.FromJson<PlayerInfoList>(namesJsonFile.text);
        foreach (GameObject car in players)
        {
            car.GetComponent<PlayerData>().SetName(PlayerDetailList.infoList[Random.Range(0, PlayerDetailList.infoList.Length)].name);
        }

        SortPlayers();
        timer = 179f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GameOverPanel.SetActive(true);
            winnerNameText.text = players[0].GetComponent<PlayerData>().playerName;
            gameOver = true;
            timerText.gameObject.SetActive(false);
            Time.timeScale = 0;

        
        }
        if (Mathf.RoundToInt(timer % 60)<10)
        {
            timerText.text = Mathf.Floor(timer / 60).ToString() + " : 0" + Mathf.RoundToInt(timer % 60).ToString();
        }
        else
        {
            timerText.text = Mathf.Floor(timer / 60).ToString() + " : " + Mathf.RoundToInt(timer % 60).ToString();
        }
    }

    public void SpawnCivilCar(GameObject destroyedCar)
    {
        destroyedCar.transform.position = randomPoints[Random.Range(0, GameplayManager.instance.randomPoints.Length)].transform.position;
        destroyedCar.GetComponent<RandomMovementArea1>().ResetCar();
    }

    public void SortPlayers()
    {
        players.Sort((IComparer<GameObject>)new sortByScore());
        players.Reverse();

        bool abovePlayer = false;
        for (int i = 0; i <4; i++)
        {
            PlayerBoardList[i].GetComponent<PlayerUIData>().NameText.text = (i+1).ToString() + " " + players[i].GetComponent<PlayerData>().playerName;
            PlayerBoardList[i].GetComponent<PlayerUIData>().ScoreText.text = players[i].GetComponent<PlayerData>().score.ToString();
            PlayerBoardList[i].GetComponent<PlayerUIData>().PlayerTag.color = players[i].GetComponent<PlayerData>().playerColor;

            if (abovePlayer)
            {
                players[i].GetComponent<PlayerData>().DangerAura.Stop();
            }
            else
            {
                players[i].GetComponent<PlayerData>().DangerAura.Play();
            }
            if (players[i].tag == "Player")
            {
                abovePlayer = true;
            }
        }

    }

    private class sortByScore : IComparer<GameObject>
    {
        int IComparer<GameObject>.Compare(GameObject _objA, GameObject _objB)
        {
            int t1 = _objA.GetComponent<PlayerData>().score;
            int t2 = _objB.GetComponent<PlayerData>().score;
            return t1.CompareTo(t2);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Invoke("HideControls",2);

      
    }

    private void HideControls()
    {
        tutorialPanel.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("city");
    }
}
