using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool TurnLeft { get => turnLeft; set => turnLeft = value; }
    private bool turnLeft = false;
    public bool Turn { get => turn; set => turn = value; }
    private bool turn = false;

    private void Update()
    {
        if (LevelManager.Instance.Level.cars[LevelManager.Instance.LevelStageIndex].carGO.name == name)
        {
            if (LevelStateManager.Instance.CurrentGameState == GameState.s2_NotStarted)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    LevelStateManager.Instance.CurrentGameState = GameState.s3_Playing;
                }
            }
            else if (LevelStateManager.Instance.CurrentGameState == GameState.s3_Playing)
            {
                if (turn)
                {
                    Rotate(90f, turnLeft);
                }
                Move(3f);
            }
        }
    }

    /// <summary>
    /// Must be called in Update
    /// </summary>
    /// <param name="ySpeed">Forward speed</param>
    private void Move(float ySpeed = 1f)
    {
        transform.localPosition += transform.up * ySpeed * Time.deltaTime;
    }

    /// <summary>
    /// Must be called in Update
    /// </summary>
    /// <param name="rotateSpeed">Between 0 and 1</param>
    /// <param name="rotateLeft">Rotation to left or not</param>
    private void Rotate(float rotateSpeed = 1f, bool rotateLeft = false)
    {
        if (rotateLeft)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime, Space.World);
        }
    }
}
