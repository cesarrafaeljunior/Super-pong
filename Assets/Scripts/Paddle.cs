using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour
{

    public float speed = 10;
    public KeyCode inputKeyUp = KeyCode.W;
    public KeyCode inputKeyDown = KeyCode.S;

    private void Update()
    {
        float movement = ProcessInput();

        Move(movement);

        ClampPositionToScreen();

        // if (position.y >= maxPositionY - 1)
        //     position.y = maxPositionY - 1;
        // else if (position.y <= minPositionY + 1)
        //     position.y = minPositionY + 1;

    }

    private float ProcessInput()
    {

        float movement = 0f;

        if (Input.GetKey(inputKeyUp))
            movement = 1f;
        else if (Input.GetKey(inputKeyDown))
            movement = -1f;

        return movement;

    }

    private void Move(float movement)
    {
        transform.Translate(
        Vector3.up *
        movement *
        speed *
        Time.deltaTime);
    }

    private void ClampPositionToScreen()
    {

        float maxPositionY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float minPositionY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        Vector3 position = transform.position;

        position.y = Mathf.Clamp(position.y, minPositionY + 1, maxPositionY - 1);

        transform.position = position;
    }
}
