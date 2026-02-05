using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
                        ChangePutState(PutState.None, null, null);
                        break;
                    case 1:
                        Debug.Log("Deleteモードに変更");
                        ChangePutState(PutState.Delete, null, null);
                        break;
                    default:
                        if (Enum.GetValues(typeof(PutState)).Length > number)
                        {
                            Debug.Log($"{(PutState)Enum.ToObject(typeof(PutState), number)}モードに変更");
                            //押された数字からDelete分を引いたインデックスで取得
                            ChangePutState((PutState)Enum.ToObject(typeof(PutState), number), _thingsPrefabs[number - (int)PutState.Delete * 2], _thingsDammyPrefabs[number - (int)PutState.Delete * 2]);
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

    private void ChangePutState(PutState state, GameObject putThings, GameObject putThingsDummy)
    {
        _putState = state;
        if (putThings != null || putThingsDummy != null)
        {
            PutThingsDown.Instance.ChangeThings(putThings, putThingsDummy);
            return;
        }
    }

    public void AddEnergy(float amount)
    {
        _Energy += amount;
    }
}
