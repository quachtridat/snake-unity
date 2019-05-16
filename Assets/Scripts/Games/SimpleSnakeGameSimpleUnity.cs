using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleSnakeGameSimpleUnity : MonoBehaviour {
    #region Fields

    [Header("Components")]
    [SerializeField]
    protected new Camera camera;

    [Header("Game")]

    [SerializeField]
    protected SimpleSnakeGameManager gameManager;

    [Header("Misc.")]

    private float timePast = 0f;
    private float updateInterval = float.PositiveInfinity;

    private GameObject _snakePrefab;
    private GameObject _foodPrefab;

    private List<GameObject> _body;
    GameObject _food;

    #endregion

    #region Properties

    private SimpleSnakeGameManager.States lastState = SimpleSnakeGameManager.States.None;

    #endregion

    private void Awake() {
        _body = new List<GameObject>();

        _snakePrefab = new GameObject("Snake Cell Prefab", typeof(SpriteRenderer));
        _snakePrefab.SetActive(false);

        SpriteRenderer sr = _snakePrefab.GetComponent<SpriteRenderer>();
        sr.sprite = gameManager.SnakeManager.SnakeConfiguration.Sprite;

        _foodPrefab = new GameObject("Food Prefab", typeof(SpriteRenderer), typeof(BoxCollider2D));
        _foodPrefab.SetActive(false);

        sr = _foodPrefab.GetComponent<SpriteRenderer>();
        sr.sprite = gameManager.FoodManager.FoodConfiguration.Sprite;
        
    }

    // Start is called before the first frame update
    void Start() {
        var camSize = camera.orthographicSize;
        var aspectRatio = Screen.width / Screen.height;
        var h = camSize;
        var w = h * aspectRatio;
        var pos = camera.transform.position;

        gameManager.GameConfiguration.PlayfieldRect = new RectInt(new Vector2Int(Mathf.CeilToInt(pos.x - w), Mathf.CeilToInt(pos.y - h)), new Vector2Int(Mathf.FloorToInt(w * 2), Mathf.FloorToInt(h * 2)));
        gameManager.Start();

        foreach (var cell in gameManager.SnakeManager.Snake.Body) {
            var obj = Instantiate(_snakePrefab, new Vector3(cell.Position.x, cell.Position.y, this.transform.position.z), Quaternion.identity, this.transform);
            obj.SetActive(true);
            obj.name = $"Snake Cell {_body.Count}";
            _body.Add(obj);
        }

        {
            var food = gameManager.FoodManager.Food;
            var obj = Instantiate(_foodPrefab, new Vector3(food.Position.x, food.Position.y, this.transform.position.z), Quaternion.identity, this.transform);
            obj.SetActive(true);
            obj.name = $"Food Cell";
            _food = obj;
        }
    }

    void FixedUpdate() {
        RecomputeUpdateInterval();
    }

    // Update is called once per frame
    void Update() {
        gameManager?.InputDirection(MovingDirection.FromKey(InputManager.FilterKeys(SimpleSnakeGameManager.Configuration.RecognizableKeyCodes, Input.GetKeyDown).FirstOrDefault()));

        timePast += Time.deltaTime;

        while (timePast >= updateInterval) {
            lastState = gameManager.Update();
            timePast -= updateInterval;

            if ((lastState & SimpleSnakeGameManager.States.Loss) != SimpleSnakeGameManager.States.None)
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game Over", UnityEngine.SceneManagement.LoadSceneMode.Single);

            if ((lastState & SimpleSnakeGameManager.States.Grown) != SimpleSnakeGameManager.States.None) {
                var cell = gameManager.SnakeManager.Snake.Head;
                var obj = Instantiate(_snakePrefab, new Vector3(cell.Position.x, cell.Position.y, this.transform.position.z), Quaternion.identity, this.transform);
                obj.SetActive(true);
                obj.name = $"Snake Cell {_body.Count}";
                _body.Add(obj);
            }
        }
    }

    void LateUpdate() {
        {
            var food = gameManager.FoodManager.Food;
            _food.transform.position = new Vector3(food.Position.x, food.Position.y, _food.transform.position.z);
        }
        var e1 = gameManager.SnakeManager.Snake.Body.GetEnumerator();
        var e2 = _body.GetEnumerator();
        while (e1.MoveNext() | e2.MoveNext()) {
            UnityEngine.Assertions.Assert.IsNotNull(e1.Current);
            UnityEngine.Assertions.Assert.IsNotNull(e2.Current);
            var pos = e1.Current.Position;
            e2.Current.transform.position = new Vector3(pos.x, pos.y, e2.Current.transform.position.z);
        }
    }

    /// <summary>
    /// Calculate move interval based on the snake's speed.
    /// </summary>
    public void RecomputeUpdateInterval() {
        updateInterval = 1f / gameManager.SnakeManager.Snake.Speed;
    }
}