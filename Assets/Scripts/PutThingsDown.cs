using UnityEngine;

public class PutThingsDown : MonoBehaviour
{
    [SerializeField] private GameObject _things;
    [SerializeField] private GameObject _thingsDammy;
    private Camera _mainCam;
    private bool _canPutThings = false;
    public static PutThingsDown Instance { get; private set; }

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
    private void Start()
    {
        _mainCam = Camera.main;
    }
    private void Update()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (GameManager.Instance._putState != GameManager.PutState.None && GameManager.Instance._putState != GameManager.PutState.Delete)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
                Vector3 pointerPosInt = new(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y) + 1, Mathf.FloorToInt(hit.point.z));
                _thingsDammy.transform.position = pointerPosInt;
                _thingsDammy.transform.position += new Vector3(0, pointerPosInt.y - _thingsDammy.GetComponent<ThingsDammy>()._bottomVector.transform.position.y, 0);

                if (hit.collider.gameObject.CompareTag("Ground") && GridManager.Instance.CanPlaceObjectAtPosition(pointerPosInt))
                {
                    _thingsDammy.SetActive(true);
                    _canPutThings = true;

                    if(Input.GetKeyDown(KeyCode.R))
                    {
                        _thingsDammy.transform.Rotate(0, 90, 0);
                        if(_thingsDammy.transform.rotation.y >= 360)
                        {
                            _thingsDammy.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                }
                else
                {
                    _thingsDammy.SetActive(false);
                    _canPutThings = false;
                }
            }
        }
        else
        {
            _canPutThings = false;
        }
        OnClickPutThings();
    }

    private void OnClickPutThings()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canPutThings && GameManager.Instance._putState != GameManager.PutState.None)
        {
            GridManager.Instance.RegisterPlacedObject(_thingsDammy.transform.position, Instantiate(_things, _thingsDammy.transform.position, Quaternion.identity));
        }
    }

    public void ChangeThings(GameObject things, GameObject thingsDammy)
    {
        _things = things;
        _thingsDammy = thingsDammy;
        Debug.Log($"Put Things Changed: {_things.name}, {_thingsDammy.name}");
    }
}
