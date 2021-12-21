using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            LevelManager.Instance.Level.cars[LevelManager.Instance.LevelStageIndex].carGO.name == collision.gameObject.name &&
            LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
        {
            var _state = LevelStateManager.Instance.CurrentGameState;
            var _levelIndex = LevelManager.Instance.CurrentLevelIndex;
            var _stageIndex = LevelManager.Instance.LevelStageIndex;

            if (_state == GameState.s3_Playing)
            {
                if (_stageIndex != 7) LevelStateManager.Instance.CurrentGameState = GameState.s4_NextState;
                else LevelStateManager.Instance.CurrentGameState = GameState.s5_CalculatePlayerStats;

                //Reset ghost cars
                LevelManager.Instance.StopCoroutineOfGhosts();
            }
        }
    }
}
