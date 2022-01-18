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
    s1_Loading,                 -> Player pref'leri check ediyor. Ve kay�tl� oyuncu verilerini y�kl�yor.
    s2_NotStarted,              -> Y�kleme bitti ve oyun oynanmaya haz�r.
    s3_Playing,                 -> oyuncu oyuna ba�lad�. Oyun devam ediyor.
    s4_CalculatePlayerStats,    -> toplanan puanlar�n hesaplanmas�. y�ld�z say�s� vs.
    s5_SaveGame,                -> playerpref'lere save i�lemi yap�l�yor. toplad���m�z puanlar, hangi levelda oldu�umuz vs.
    s6_Finished,                -> oyun b�t�n hesaplamalar�yla bitti ve yeni level y�klenmeye haz�r.
                                    Bunun ard�ndan s1_Loading state'ine d�n�l�r. ��nk� yeni level y�klenir.
    s_Failed                    -> Oyuncu yand�. can� bitti, trap'e de�di vs.
                                    Bu durumun ard�ndan s2_NotStarted state'ine d�n�l�r. ��nk� level zaten y�kl�d�r ve oyunun yeniden ba�lanmas� sa�lan�r.

singleton design pattern    ->      Tek bir object, her class'dan eri�im.
static keyword              ->      Temelde bir class'�n instance'�n� yaratmadan; o class'�n eleman�na eri�meyi sa�lar.

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
