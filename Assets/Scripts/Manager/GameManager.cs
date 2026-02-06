using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Energy_Text;
    [SerializeField] private TextMeshProUGUI _PutState_Text;
    [SerializeField] private TextMeshProUGUI _Money_Text;
    [SerializeField] private float _Energy = 0f;
    [SerializeField] private float _Money = 0f;
    public PutState _putState { get; private set; } = PutState.None;

    public List<GameObject> _thingsPrefabs;
    public List<GameObject> _thingsDammyPrefabs;
    public static GameManager Instance { get; private set; }
    public enum PutState
    {
        None,
        Delete,
        Conveyor,
        Entrance,
        Exit,
        ProvidInfoMachine
    }

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
        UpdatePutStateText("何もしない");
        ChangePutState(PutState.None, null, null, 0);
    }

    private void Update()
    {
        ChangePutThings();
    }

    private void ChangePutThings()
    {
        if (Input.anyKeyDown) // 何らかのキーが押された時
        {
            string keyStr = Input.inputString;

            // 数字かどうかを判定する例
            if (int.TryParse(keyStr, out int number))
            {
                Debug.Log("数字が入力されました: " + number);
                switch (number)
                {
                    case 0:
                        UpdatePutStateText("何もしない");
                        ChangePutState(PutState.None, null, null, 0);
                        break;
                    case 1:
                        UpdatePutStateText("置いたものを消す");
                        ChangePutState(PutState.Delete, null, null, 0);
                        break;
                    default:
                        if (Enum.GetValues(typeof(PutState)).Length > number)
                        {
                            PutState selectedState = (PutState)Enum.ToObject(typeof(PutState), number);
                            int selectedIndex = number - (int)PutState.Delete * 2;//押された数字からDelete分を引いたインデックスで取得
                            float thingsPrice = _thingsPrefabs[selectedIndex].GetComponent<Things>()._Price;
                            ChangePutState(selectedState, _thingsPrefabs[selectedIndex], _thingsDammyPrefabs[selectedIndex], thingsPrice);
                            UpdatePutStateText(_thingsPrefabs[selectedIndex].GetComponent<Things>()._thingName + "を置く");
                        }
                        else
                        {
                            Debug.Log("対応するモードが存在しません");
                        }
                        break;
                }
            }
        }
    }

    private void ChangePutState(PutState state, GameObject putThings, GameObject putThingsDummy, float thingsPrice)
    {
        _putState = state;
        if (putThings != null || putThingsDummy != null)
        {
            PutThingsDown.Instance.ChangeThings(putThings, putThingsDummy, thingsPrice);
            return;
        }
    }

    public void UpdatePutStateText(string text)
    {
        _PutState_Text.text = "現在の行動 : " + text;
    }

    public float GetEnergy()
    {
        return _Energy;
    }

    public float GetMoney()
    {
        return _Money;
    }

    public void AddEnergy(float amount)
    {
        _Energy += amount;
        _Energy_Text.text = "Energy: " + _Energy.ToString("F0");
    }

    public void AddMoney(float amount)
    {
        _Money += amount;
        _Money_Text.text = "Money: " + _Money.ToString("F0");
    }

    public void RemoveEnergy(float amount)
    {
        _Energy -= amount;
        _Energy_Text.text = "Energy: " + _Energy.ToString("F0");
    }

    public void RemoveMoney(float amount)
    {
        _Money -= amount;
        _Money_Text.text = "Money: " + _Money.ToString("F0");
    }
}