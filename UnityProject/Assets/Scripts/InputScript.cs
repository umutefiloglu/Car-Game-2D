using UnityEngine;


public class InputScript : MonoBehaviour
{
    [SerializeField] [Tooltip("Player Object")] protected PlayerController playerController;
    private Touch touch;

    // Update is called once per frame
    void Update()
    {
        OnFingerUp();
        OnFingerDown();
    }

    private void OnFingerUp()
    {
        if (Input.touchCount == 0)
        {
            playerController.Turn = false;
        }
    }

    private void OnFingerDown()
    {
        if (Input.touchCount > 0)
        {
            playerController.Turn = true;
            touch = Input.GetTouch(0);
            Vector3 _screenPos = touch.position;
            if (_screenPos.x < Screen.width / 2) playerController.TurnLeft = true;
            else playerController.TurnLeft = false;
        }
    }
}
