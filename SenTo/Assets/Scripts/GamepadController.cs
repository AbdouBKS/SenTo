using UnityEngine;

public class GamepadController : MonoBehaviour
{
    public bool stickDownLast = false;
    public bool stickUpLast = false;

    private void Start() {
        stickDownLast = false;
        stickUpLast = false;
    }

    public float GetDirectionPressed(string direction)
    {
        float dir = Input.GetAxisRaw(direction);
        float result = 0;

        if(dir < 0) {
            if(!stickDownLast) {
                result = -1;
            }
            stickDownLast = true;
        }
        else {
            stickDownLast = false;
        }

        if(dir > 0) {
            if(!stickUpLast) {
                result = 1;
            }
            stickUpLast = true;
        }
        else {
            stickUpLast = false;
        }

        return result;
    }
}
