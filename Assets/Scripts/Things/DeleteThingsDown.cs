using UnityEngine;

public class DeleteThingsDown : MonoBehaviour
{
    [SerializeField] private GameObject _deleteDammy;
    private Camera _mainCam;
    private bool _canDeleteThings = false;
    private GameManager _gameManager = GameManager.Instance;
    private GridManager _gridManager = GridManager.Instance;
    public static DeleteThingsDown Instance { get; private set; }

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
        if (_gameManager._putState != GameManager.PutState.Delete) { return; }

        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
            Vector3 pointerPosInt = new(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y) + 1, Mathf.FloorToInt(hit.point.z));
            _deleteDammy.transform.position = pointerPosInt;
            if (!_gridManager.CanPlaceObjectAtPosition(pointerPosInt))
            {
                _deleteDammy.SetActive(true);
                _canDeleteThings = true;
            }
            else
            {
                _deleteDammy.SetActive(false);
                _canDeleteThings = false;
            }
        }
        OnClickDeleteThings();
    }

    private void OnClickDeleteThings()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canDeleteThings)
        {
            GameObject remove = _gridManager.GetObjectAtPosition(_deleteDammy.transform.position);
            _gridManager.UnregisterPlacedObject(_deleteDammy.transform.position);
            _gameManager.AddMoney(remove.GetComponent<Things>()._Price);
            Destroy(remove.GetComponent<Things>()._cat);
            Destroy(remove);
        }
    }
}
