using System.Linq;
using UnityEngine;

public class SimpleSnakeGameOnGUI : MonoBehaviour {
    #region Fields

    [Header("Game")]

    [SerializeField]
    protected SimpleSnakeGameManager gameManager;

    [Header("GUI")]

    [Tooltip("Size of a (square) cell in pixels.")]
    [Range(1 << 0, 1 << 8)]
    public int CellSize;

    [Header("Misc.")]

    private float timePast = 0f;
    private float updateInterval = float.PositiveInfinity;

    #endregion

    #region Properties

    private SimpleSnakeGameManager.States lastState = SimpleSnakeGameManager.States.None;

    #endregion

    #region MonoBehaviour methods

    void Start() {
        gameManager.GameConfiguration.PlayfieldRect = new RectInt(new Vector2Int(0, 0), new Vector2Int(Screen.width / CellSize, Screen.height / CellSize));
        gameManager.Start();
    }

    void FixedUpdate() {
        RecomputeUpdateInterval();
    }

    void Update() {
        gameManager?.InputDirection(MovingDirection.FromKey(InputManager.FilterKeys(SimpleSnakeGameManager.Configuration.RecognizableKeyCodes, Input.GetKeyDown).FirstOrDefault()).InvertY());

        timePast += Time.deltaTime;

        while (timePast >= updateInterval) {
            lastState = gameManager.Update();
            timePast -= updateInterval;

            if ((lastState & SimpleSnakeGameManager.States.Loss) != SimpleSnakeGameManager.States.None)
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game Over", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    void OnGUI() {
        DrawSnakeInfo();
        DrawFood();
        DrawSnakeBody();
    }

    #endregion

    #region GUI-related

    private void DrawSnakeInfo() {
        string textScore = $"Score: {gameManager?.SnakeManager?.Score}";
        GUI.TextField(new Rect(10, 10, textScore.Length * 12, 32), textScore);
    }

    private void DrawSnakeBody() {
        foreach (var cell in gameManager?.SnakeManager.Snake.Body)
            GUI.DrawTextureWithTexCoords(new Rect(cell.Position.x * CellSize, cell.Position.y * CellSize, CellSize, CellSize), cell.Sprite.texture, cell.Sprite.UV());
    }

    private void DrawFood() {
        var food = gameManager.FoodManager.Food;
        GUI.DrawTextureWithTexCoords(new Rect(food.Position.x * CellSize, food.Position.y * CellSize, CellSize, CellSize), food.Sprite.texture, food.Sprite.UV());
    }

    #endregion

    /// <summary>
    /// Calculate move interval based on the snake's speed.
    /// </summary>
    public void RecomputeUpdateInterval() {
        updateInterval = 1f / gameManager.SnakeManager.Snake.Speed;
    }
}