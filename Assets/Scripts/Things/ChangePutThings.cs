using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class ChangePutThings : MonoBehaviour
{
    public List<GameObject> _ThingsPrefabs;
    public List<GameObject> _ThingsDammyPrefabs;
    [SerializeField] private TextMeshProUGUI _putState_Text;
    [SerializeField] private PutThingsDown _putThingsDown;
    [SerializeField] private DeleteThingsDown _deleteThingsDown;
    public PutState _PutState { get; private set; } = PutState.None;
    public enum PutState
    {
        None,
        Delete,
        Conveyor,
        Entrance,
        Exit,
        ProvideInfoBox,
        ProvideInfoMushroom,
        ProvideInfoMatatabi,
        ProvidInfoMachine,
        ProvideInfoWater
    }

    void Start()
    {
        UpdatePutStateText("何もしない");
        ChangePutState(PutState.None, null, null, 0);
    }

    void Update()
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
                        _putThingsDown.ChangePutState(PutState.None);
                        _deleteThingsDown.ChangePutState(PutState.None);
                        break;
                    case 1:
                        UpdatePutStateText("置いたものを消す");
                        _putThingsDown.ChangePutState(PutState.Delete);
                        _deleteThingsDown.ChangePutState(PutState.Delete);
                        break;
                    default:
                        if (Enum.GetValues(typeof(PutState)).Length > number)
                        {
                            PutState selectedState = (PutState)Enum.ToObject(typeof(PutState), number);
                            int selectedIndex = number - (int)PutState.Delete * 2;//押された数字からDelete分を引いたインデックスで取得
                            float thingsPrice = _ThingsPrefabs[selectedIndex].GetComponent<Things>()._Price;
                            ChangePutState(selectedState, _ThingsPrefabs[selectedIndex], _ThingsDammyPrefabs[selectedIndex], thingsPrice);
                            _deleteThingsDown.ChangePutState(selectedState);
                            UpdatePutStateText(_ThingsPrefabs[selectedIndex].GetComponent<Things>()._thingName + "を置く");
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
        _PutState = state;
        if(putThings != null && putThingsDummy != null)
        {
            _putThingsDown.ChangeThings(putThings, putThingsDummy, thingsPrice);
        }
        _putThingsDown.ChangePutState(state);
    }

    public void UpdatePutStateText(string text)
    {
        _putState_Text.text = "現在の行動 : " + text;
    }
}
