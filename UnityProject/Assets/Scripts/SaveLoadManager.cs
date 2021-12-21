using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private bool alreadySaved = false;

    private static SaveLoadManager instance;
    public static SaveLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SaveLoadManager");
                go.AddComponent<SaveLoadManager>();
            }

            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelStateManager.Instance.OnGameStateChange.AddListener(OnStateChange);
    }
    void OnStateChange()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s1_Loading)
        {
            //Load level index
            if (PlayerPrefs.HasKey("currentLevelIndex"))
            {
                LevelManager.Instance.CurrentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex");
                if (LevelManager.Instance.CurrentLevelIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    if (LevelManager.Instance.CurrentLevelIndex != SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(LevelManager.Instance.CurrentLevelIndex);
                    }
                }
            }
            else
            {
                LevelManager.Instance.CurrentLevelIndex = 0;
            }

            alreadySaved = false;
            LevelManager.Instance.ConstructLevel();
            LevelStateManager.Instance.CurrentGameState = GameState.s2_NotStarted;
        }
        else if (LevelStateManager.Instance.CurrentGameState == GameState.s6_SaveGame && !alreadySaved)
        {
            alreadySaved = true;
            Debug.Log("I am saving the game.");
            if (LevelManager.Instance.CurrentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                PlayerPrefs.SetInt("currentLevelIndex", LevelManager.Instance.CurrentLevelIndex + 1);
            }
            PlayerPrefs.Save();
            LevelStateManager.Instance.CurrentGameState = GameState.s7_Finished;
        }
    }
}
