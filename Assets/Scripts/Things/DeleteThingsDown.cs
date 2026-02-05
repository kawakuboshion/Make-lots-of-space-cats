using UnityEngine;

public class DeleteThingsDown : MonoBehaviour
{
    [SerializeField] private GameObject _deleteDammy;
    private Camera _mainCam;
    private bool _canDeleteThings = false;
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
        if (GameManager.Instance._putState != GameManager.PutState.Delete) { return; }

        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
            Vector3 pointerPosInt = new(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y) + 1, Mathf.FloorToInt(hit.point.z));
            _deleteDammy.transform.position = pointerPosInt;
            if (!GridManager.Instance.CanPlaceObjectAtPosition(pointerPosInt))
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
            GameObject remove = GridManager.Instance.GetObjectAtPosition(_deleteDammy.transform.position);
            GridManager.Instance.UnregisterPlacedObject(_deleteDammy.transform.position);
            Destroy(remove);
        }
    }
}
