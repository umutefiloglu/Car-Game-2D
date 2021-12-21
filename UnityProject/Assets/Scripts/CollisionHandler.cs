using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Actual Player collided with hazard
        if ((collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Player")) &&
            LevelManager.Instance.Level.cars[LevelManager.Instance.LevelStageIndex].carGO.name == name &&
            LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Stop ghost car movement
                LevelManager.Instance.StopCoroutineOfGhosts();
            }

            LevelStateManager.Instance.CurrentGameState = GameState.s_Failed;
        }
    }


}
