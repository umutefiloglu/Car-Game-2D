using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelStateManager : MonoBehaviour
{
    private static LevelStateManager instance;
    public static LevelStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("LevelStateManager");
                go.AddComponent<LevelStateManager>();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
        if (onGameStateChange == null)
        {
            onGameStateChange = new UnityEvent();
        }
    }

    private GameState currentGameState;
    /// <summary>
    /// Sets current game state; then fires onGameStateChange event.
    /// </summary>
    public GameState CurrentGameState
    {
        get => currentGameState;
        set
        {
            currentGameState = value;
            Debug.LogWarning(currentGameState + ": Fired");
            onGameStateChange.Invoke();
        }
    }

    private UnityEvent onGameStateChange;
    /// <summary>
    /// This event is fired when game state changes.
    /// </summary>
    public UnityEvent OnGameStateChange { get => onGameStateChange; set => onGameStateChange = value; }


    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        CurrentGameState = GameState.s1_Loading;
    }
}
