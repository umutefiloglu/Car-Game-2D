using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTrackOfCars : MonoBehaviour
{
    private List<Vector3> carPos;
    private List<Quaternion> carRot;
    private int currentStageIndex = -1;
    private bool keepTrack = false;

    // Start is called before the first frame update
    void Start()
    {
        LevelStateManager.Instance.OnGameStateChange.AddListener(OnStateChange);

        carPos = new List<Vector3>();
        carRot = new List<Quaternion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keepTrack && LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
        {
            KeepTrackOfTransform();
        }
    }

    private void OnStateChange()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s2_NotStarted)
        {
            CheckPlayerObject();
        }
        else if (LevelStateManager.Instance.CurrentGameState == GameState.s4_NextState && keepTrack)
        {
            PassTrackingResults();
        }
        else if (LevelStateManager.Instance.CurrentGameState == GameState.s_Failed && keepTrack)
        {
            //Release memory
            carPos.Clear();
            carRot.Clear();
        }
    }

    private void KeepTrackOfTransform()
    {
        carPos.Add(transform.position);
        carRot.Add(transform.rotation);
    }

    private void PassTrackingResults()
    {
        LevelManager.Instance.Level.cars[currentStageIndex].carPos = null;
        LevelManager.Instance.Level.cars[currentStageIndex].carPos = new Vector3[carPos.Count];

        LevelManager.Instance.Level.cars[currentStageIndex].carRot = null;
        LevelManager.Instance.Level.cars[currentStageIndex].carRot = new Quaternion[carRot.Count];

        carPos.CopyTo(LevelManager.Instance.Level.cars[currentStageIndex].carPos);
        carRot.CopyTo(LevelManager.Instance.Level.cars[currentStageIndex].carRot);

        keepTrack = false;
        //Release memory
        carPos.Clear();
        carRot.Clear();
    }

    private bool CheckPlayerObject()
    {
        currentStageIndex = LevelManager.Instance.LevelStageIndex;
        var _tempCar = LevelManager.Instance.Level.cars[currentStageIndex].carGO;

        if (_tempCar.name == name && currentStageIndex != 7)
        {
            keepTrack = true;
            return true;
        }
        keepTrack = false;
        return false;
    }
}
