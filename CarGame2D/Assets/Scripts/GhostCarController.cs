using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCarController : MonoBehaviour
{
    bool amIGhost = false;
    [SerializeField] [Tooltip("Object to deactivate")] GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        LevelStateManager.Instance.OnGameStateChange.AddListener(OnStateChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStateChange()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s2_NotStarted)
        {
            amIGhost = GhostChecker();
            if (amIGhost)
            {
                DisableThis();
            }
            else
            {
                EnableThis();
            }
        }
        if (amIGhost)
        {
            if (LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
            {
                EnableThis();
            }
            else if (LevelStateManager.Instance.CurrentGameState == GameState.s2_NotStarted)
            {
                DisableThis();
            }
        }
    }

    private bool GhostChecker()
    {
        if (LevelManager.Instance.Level.cars[LevelManager.Instance.LevelStageIndex].carGO.name == name)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Makes object's rigidbody kinematic and disables box collider2d of it.
    /// </summary>
    private void DisableThis()
    {
        mesh.SetActive(false);
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// Makes object's rigidbody dynamic and enables box collider2d of it.
    /// </summary>
    private void EnableThis()
    {
        mesh.SetActive(true);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
