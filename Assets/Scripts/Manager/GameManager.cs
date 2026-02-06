using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _Energy_Text;
    [SerializeField] TextMeshProUGUI _PutState_Text;
    [SerializeField] TextMeshProUGUI _Money_Text;
    public PutState _putState { get; private set; } = PutState.None;
    public enum PutState
    {
        None,
        Delete,
        Conveyor,
        Entrance,
        Exit
    }

    public List<GameObject> _thingsPrefabs;
    public List<GameObject> _thingsDammyPrefabs;
    private float _Energy = 0f;
    private float _Money = 0f;
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
                        Debug.Log("Noneモードに変更");
                        ChangePutState(PutState.None, null, null, 0);
                        break;
                    case 1:
                        Debug.Log("Deleteモードに変更");
                        ChangePutState(PutState.Delete, null, null, 0);
                        break;
                    default:
                        if (Enum.GetValues(typeof(PutState)).Length > number)
                        {
                            PutState selectedState = (PutState)Enum.ToObject(typeof(PutState), number);
                            int selectedIndex = number - (int)PutState.Delete * 2;//押された数字からDelete分を引いたインデックスで取得
                            float thingsPrice = _thingsPrefabs[selectedIndex].GetComponent<Things>()._Price;
                            ChangePutState(selectedState, _thingsPrefabs[selectedIndex], _thingsDammyPrefabs[selectedIndex], thingsPrice);
                            Debug.Log($"{selectedState}モードに変更");
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
        _Energy_Text.text = "Energy: " + _Energy.ToString("F1");
    }

    public void AddMoney(float amount)
    {
        _Money += amount;
        _Money_Text.text = "Money: " + _Money.ToString("F1");
    }

    public void RemoveEnergy(float amount)
    {
        _Energy -= amount;
        _Energy_Text.text = "Energy: " + _Energy.ToString("F1");
    }

    public void RemoveMoney(float amount)
    {
        _Money -= amount;
        _Money_Text.text = "Money: " + _Money.ToString("F1");
    }
}