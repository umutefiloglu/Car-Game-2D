using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Management
    private int currentLevelIndex;
    private int levelStageIndex = 0;
    public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
    public int LevelStageIndex { get => levelStageIndex; set => levelStageIndex = value; }
    #endregion
    #region Level Building
    [SerializeField][Tooltip("Unique for each level.")]private LevelInfo level = new LevelInfo(8);
    public LevelInfo Level { get => level; set => level = value; }
    #endregion

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("LevelManager");
                go.AddComponent<LevelManager>();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
        levelStageIndex = 0;
    }

    private void Start()
    {
        LevelStateManager.Instance.OnGameStateChange.AddListener(OnStateChange);
    }

    private void Update()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s_Failed)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                LevelStateManager.Instance.CurrentGameState = GameState.s1_Loading;
            }
        }
    }

    public void ConstructLevel()
    {
        ActivateCurrentStage(levelStageIndex);
        AdjustPlayer(ref level.cars[levelStageIndex].carGO, level.cars[levelStageIndex].startGO);
    }

    private void OnStateChange()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
        {
            StartPreviousPlayers(levelStageIndex);
        }
        else if (LevelStateManager.Instance.CurrentGameState == GameState.s4_NextState)
        {
            levelStageIndex++;
            Debug.Log("Level Stage: " + levelStageIndex);
            LevelStateManager.Instance.CurrentGameState = GameState.s1_Loading;
        }
        else if (LevelStateManager.Instance.CurrentGameState == GameState.s_Failed)
        {
            StopCoroutineOfGhosts();
        }
        else if (LevelStateManager.Instance.CurrentGameState == GameState.s5_CalculatePlayerStats)
        {
            LevelStateManager.Instance.CurrentGameState = GameState.s6_SaveGame;
        }
    }

    

    private void ActivateCurrentStage(int index)
    {
        for (int i = 0; i < level.stagesGO.Length; i++)
        {
            if (i == index) level.stagesGO[i].SetActive(true);
            else level.stagesGO[i].SetActive(false);
        }
    }
    private void AdjustPlayer(ref GameObject player, GameObject startGO)
    {
        player.SetActive(true);
        player.transform.position = startGO.transform.position;
        player.transform.rotation = startGO.transform.localRotation;
    }
    private void StartPreviousPlayers(int currentIndex)
    {
        if (currentIndex == 0) return;

        for (int i = currentIndex - 1; i >= 0; i--)
        {
            level.cars[i].carGO.SetActive(true);
            level.cars[i].carGO.transform.position = level.cars[i].startGO.transform.position;
            level.cars[i].carGO.transform.rotation = level.cars[i].startGO.transform.localRotation;

            StartCoroutine(MovePlayer(i));
        }
    }
    private IEnumerator MovePlayer(int index)
    {
        for (int i = 0; i < level.cars[index].carPos.Length; i++)
        {
            level.cars[index].carGO.transform.position = level.cars[index].carPos[i];
            level.cars[index].carGO.transform.rotation = level.cars[index].carRot[i];
            yield return new WaitForEndOfFrame();
        }

        if (LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
        {
            //Disable ghosts arrived their destination before current stage finishes
            level.cars[index].carGO.transform.GetChild(0).gameObject.SetActive(false);
            level.cars[index].carGO.GetComponent<Rigidbody2D>().isKinematic = true;
            level.cars[index].carGO.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void StopCoroutineOfGhosts()
    {
        StopAllCoroutines();
    }
}
