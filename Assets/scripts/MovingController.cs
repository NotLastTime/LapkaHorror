using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class MovingController : MonoBehaviour
{
    public float sensitivity = 200f;
    private Vector2 turn;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        if (turn.x > 89)
        {
            turn.x = 90f;
        }
        else if(turn.x < -89)
        {
            turn.x = -90f;
        }

        if (turn.y > 89)
        {
            turn.y = 90f;
        }
        else if(turn.y < -89)
        {
            turn.y = -90f;
        }
        transform.localRotation = UnityEngine.Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
