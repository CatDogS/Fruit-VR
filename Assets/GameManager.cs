using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : UnitySingleton<GameManager>
{
    //文本分数框
    public Text scoreTxt;
    //时间文本框
    public Text timeTxt;
    //重新开始模块预制体
    public GameObject RestartModulePrefab;
    //游戏是否结束
    public bool isGameOver;
    //游戏结束UI显示位置
    public Transform RestartModulePos;
    //当前分数
    private int totalScore;
    //剩余时间
    private float timeRemain;
    //游戏时间
    private float countTime = 60;
    //游戏开始时间
    private float startTime;

    private SpawnPoint spawnPoint;

    // Start is called beSfore the first frame update
    void Start()
    {
        spawnPoint = GetComponent<SpawnPoint>();
        ini();
    }

    //初始化游戏
    private void ini() { 
        totalScore = 0;
        scoreTxt.text = "0";
        timeTxt.text = "00:00";
        isGameOver = false;
        startTime = Time.time;

        spawnPoint.ini();
    }

    //分数增加
    public void AddScore(int score) {
        totalScore += score;
        scoreTxt.text = totalScore.ToString();
    }

    public void FixedUpdate() {
        if (isGameOver) {
            return;
        }
        timeRemain = countTime - (Time.time - startTime);
        timeTxt.text = formateTime(timeRemain);
        if (timeRemain <= 0) {
            onGameOver();
        }
    }

    //格式化要显示的时间
    private string formateTime(float time) {
        string timeStr;
        int timeFormated = (int)time;
        timeStr = time > 10 ? "00:" + timeFormated.ToString() : "00:0" + timeFormated.ToString();
        return timeStr;
    }

    //游戏结束处理函数
    private void onGameOver() {
        isGameOver = true;
        //生成游戏结束模块的实例
        GameObject restartUI = Instantiate(RestartModulePrefab);
        restartUI.transform.position = RestartModulePos.position;
        restartUI.transform.localScale = Vector3.zero;
        restartUI.transform.DOScale(Vector3.one * 20, 0.3f);
        //停止生成水果
        spawnPoint.OnGameOver();
    }

    //重新开始游戏
    public void RestartGame() {
        //延时调用初始化函数
        Invoke("ini", 1);
    }

}
