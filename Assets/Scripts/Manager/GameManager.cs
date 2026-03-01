using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _result_Canvas;
    [SerializeField] private TextMeshProUGUI _resultTitle_Text;
    [SerializeField] private TextMeshProUGUI _resultScore_Text;
    [SerializeField] private TextMeshProUGUI _resultTotalScore_Text;
    [SerializeField] private TextMeshProUGUI _result_Text;
    [SerializeField] private TextMeshProUGUI _money_Text;
    [SerializeField] private List<Image> _life_Images;
    [SerializeField] private TextMeshProUGUI _energyProductionPerSecond_Text;
    [SerializeField] private TextMeshProUGUI _factorySurvivalTime_Text;
    [SerializeField] private TextMeshProUGUI _targetProductionAmount_Text;
    [SerializeField] private TextMeshProUGUI _intervalCountDown_Text;
    [SerializeField] private TextMeshProUGUI _spaceCatCounter_Text;
    [SerializeField] private TextMeshProUGUI _log_Text;
    [SerializeField] private CameraMove _cameraMove;
    [SerializeField] private Ranking _ranking;
    [SerializeField] private Color _defaultLogColor;
    [SerializeField] private Color _errorLogColor;
    [SerializeField] private float _energy = 0f;
    [SerializeField] private float _money = 0f;
    [SerializeField] private float _reductionInAnger = 5f;
    [SerializeField] private int _initialTargetProductionAmount = 10;
    [SerializeField] private int _initialCheckEnergyInterval = 60;
    [SerializeField] private int _targetProductionIncreaseAmount = 10;
    [SerializeField] private int _checkEnergyIntervalMax = 30;
    [SerializeField] private int _checkEnergyIntervalMin = 5;
    private int _life = 3;
    private float _energyProductionPerSecond = 0f;
    private float _targetProductionAmount = 10f;
    private float _lastEnergy = 0f;
    private float _factorySurvivalTime = 0f;
    private int _checkEnergyInterval;
    private int _spaceCatsCounter = 0;
    private int _logCount = 0;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AddEnergy(0f);
        AddMoney(0f);
    }

    private void Start()
    {
        StartCoroutine(PrintProductionPerSecond());
        StartCoroutine(UpdateFactorySurvivalTime());
        SetNewTargetProductionAmountIncrease(_initialTargetProductionAmount);
        SetNewInterval(_initialCheckEnergyInterval);
        StartCoroutine(CheckEnergyByNeeds());
        _result_Canvas.gameObject.SetActive(false);
        AudioManager.Instance.PlayBGM(AudioManager.BGM.Stage);
    }

    private IEnumerator CheckEnergyByNeeds()
    {
        int countdown = _checkEnergyInterval;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            countdown--;
            _intervalCountDown_Text.text = "次のチェックまで : " + countdown.ToString("F0") + "秒";
            if(countdown <= 0)
            {
                float requiredEnergy = _targetProductionAmount;
                if (_energyProductionPerSecond >= requiredEnergy)
                {
                    SetLogText("エネルギー生産量が目標を達成しています。");
                    AudioManager.Instance.PlaySE(AudioManager.SE.Happy);
                }
                else
                {
                    SetLogText("エネルギー生産量が目標を下回っています。", true);
                    _cameraMove.ShakeCamera();
                    AudioManager.Instance.PlaySE(AudioManager.SE.Anger);
                    _life--;
                    _life_Images[_life].enabled = false;// ライフが減る
                    if (_life <= 0)
                    {
                        ShowResult("エネルギーが足りなくて市民がおこった。");
                        yield break;
                    }
                }
                SetNewTargetProductionAmountIncrease(_targetProductionIncreaseAmount);
                SetNewInterval(UnityEngine.Random.Range(_checkEnergyIntervalMin, _checkEnergyIntervalMax));
                countdown = _checkEnergyInterval;
            }
        }
    }

    private void ShowResult(string cause)
    {
        Time.timeScale = 0f; // ゲームを停止
        AudioManager.Instance.PlayBGM(AudioManager.BGM.Result);

        _resultTitle_Text.text = "工場がこわされました！\n" +
                                 "原因 : " + cause;

        _result_Text.text = "最終目標生産量 : " + _targetProductionAmount.ToString("F0") + "/s\n" +
                            "最終エネルギー生産量 : " + _energyProductionPerSecond.ToString("F0") + "/s\n" +
                            "最終お金所持量 : " + _money.ToString("F0") + "\n" +
                            "工場生存時間 : " + _factorySurvivalTime.ToString("F0") + "秒\n" +
                            "宇宙ネコの数 : " + _spaceCatsCounter;

        _resultScore_Text.text = _targetProductionAmount * 10 + "\n" +
                                 _energyProductionPerSecond * 10 + "\n" +
                                 _money * 5 + "\n" +
                                 _factorySurvivalTime * 100 + "\n" +
                                 _spaceCatsCounter * 100 + "\n";

        float totalScore = CalculateScore();

        _resultTotalScore_Text.text = "総合スコア : " + totalScore.ToString("F0");

        _result_Canvas.gameObject.SetActive(true);
        _ranking.AddScore(totalScore);
    }

    private float CalculateScore()
    {
        return _targetProductionAmount * 10 + 
               _energyProductionPerSecond * 10 + 
               _energy * 5 + 
               _money * 5 + 
               _factorySurvivalTime * 100 + 
               _spaceCatsCounter * 100;
    }
    private void SetNewTargetProductionAmountIncrease(float amount)
    {
        _targetProductionAmount += amount;
        _targetProductionAmount_Text.text = "目標生産量 : " + _targetProductionAmount.ToString("F0") + "/s";
    }

    private void SetNewInterval(int interval)
    {
        _checkEnergyInterval = interval;
    }
    private IEnumerator PrintProductionPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _energyProductionPerSecond = _energy - _lastEnergy;
            _energyProductionPerSecond_Text.text = "エネルギーの生産量 : " + (_energyProductionPerSecond).ToString("F0") + "/s";
            _lastEnergy = _energy;
            AddMoney(_energyProductionPerSecond); // 生産量に応じてお金を増やす
        }
    }
    private IEnumerator UpdateFactorySurvivalTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _factorySurvivalTime += 1f;
            _factorySurvivalTime_Text.text = "工場生存時間 : " + _factorySurvivalTime.ToString("F0") + "秒";
        }
    }

    public void SetLogText(string message, bool isError = false)
    {
        _log_Text.text += message + "\n";
        _log_Text.color = isError ? _errorLogColor : _defaultLogColor;
        _logCount++;
        if (_logCount > 5)
        {
            _log_Text.text = _log_Text.text.Remove(0, _log_Text.text.IndexOf('\n') + 1);// 古いログを削除して最新の5件のみを表示
            _logCount--;
        }
    }
    public float GetEnergy()
    {
        return _energy;
    }

    public float GetMoney()
    {
        return _money;
    }

    public void AddEnergy(float amount)
    {
        _energy += amount;
    }

    public void AddMoney(float amount)
    {
        _money += amount;
        _money_Text.text = "Money: " + _money.ToString("F0");
    }
    public void AddSpaceCatCounter(int amount)
    {
        _spaceCatsCounter += amount;
        _spaceCatCounter_Text.text = "宇宙ネコの数 : " + _spaceCatsCounter.ToString();
    }

    public void RemoveEnergy(float amount)
    {
        _energy -= amount;
    }

    public void RemoveMoney(float amount)
    {
        _money -= amount;
        _money_Text.text = "Money: " + _money.ToString("F0");
    }
}