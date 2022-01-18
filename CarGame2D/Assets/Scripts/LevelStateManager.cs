using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*

- CUSTOM DATA TYPES
enum
struct
constructor
serializable


- LEVEL STATE MANAGER
    s1_Loading,                 -> Player pref'leri check ediyor. Ve kayýtlý oyuncu verilerini yüklüyor.
    s2_NotStarted,              -> Yükleme bitti ve oyun oynanmaya hazýr.
    s3_Playing,                 -> oyuncu oyuna baþladý. Oyun devam ediyor.
    s4_CalculatePlayerStats,    -> toplanan puanlarýn hesaplanmasý. yýldýz sayýsý vs.
    s5_SaveGame,                -> playerpref'lere save iþlemi yapýlýyor. topladýðýmýz puanlar, hangi levelda olduðumuz vs.
    s6_Finished,                -> oyun bütün hesaplamalarýyla bitti ve yeni level yüklenmeye hazýr.
                                    Bunun ardýndan s1_Loading state'ine dönülür. Çünkü yeni level yüklenir.
    s_Failed                    -> Oyuncu yandý. caný bitti, trap'e deðdi vs.
                                    Bu durumun ardýndan s2_NotStarted state'ine dönülür. Çünkü level zaten yüklüdür ve oyunun yeniden baþlanmasý saðlanýr.

singleton design pattern    ->      Tek bir object, her class'dan eriþim.
static keyword              ->      Temelde bir class'ýn instance'ýný yaratmadan; o class'ýn elemanýna eriþmeyi saðlar.

Getter ve Setter fonksiyonlar
private float boy;
    public float GetBoy()
        { return boy; }
    public void SetBoy(float b)
        { boy = b; }

Order of execution for event functions
https://docs.unity3d.com/Manual/ExecutionOrder.html
*/
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
