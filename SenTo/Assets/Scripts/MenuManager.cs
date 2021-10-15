using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject circles;
    public GameObject circle_left;
    public GameObject circle_right;
    public float offset_circles_y;
    public float offset_circles_x;

    public GameObject[] objects;

    private GamepadController gamepadController;


    private int selector = 0;
    private int objects_len = 0;

    private void Start()
    {
        gamepadController = GetComponent<GamepadController>();
        objects_len = objects.GetLength(0);

        moveCircles();
    }

    // Update is called once per frame
    void Update()
    {
        moveSelector();
    }

    void moveSelector()
    {
        float vertical = gamepadController.GetDirectionPressed("Vertical");

        if (vertical > 0) {
            moveUp();
        } else if (vertical < 0) {
            moveDown();
        }

    }

    void moveDown()
    {
        if (selector + 1 >= objects_len) {
            return;
        }

        selector++;
        moveCircles();
    }

    void moveUp()
    {
        if (selector <= 0) {
            return;
        }

        selector--;
        moveCircles();
    }

    void moveCircles()
    {
        Transform selected_ts = objects[selector].GetComponent<Transform>();
        Bounds selected_bounds = objects[selector].GetComponent<SpriteRenderer>().bounds;
        Transform circles_ts = circles.GetComponent<Transform>();
        Transform circle_left_ts = circle_left.GetComponent<Transform>();
        Transform circle_right_ts = circle_right.GetComponent<Transform>();


        circles_ts.position = new Vector2(circles_ts.position.x, selected_ts.position.y + offset_circles_y);
        circle_right_ts.position = new Vector2(selected_bounds.extents.x + offset_circles_x, circle_right_ts.position.y);
        circle_left_ts.position = new Vector2(selected_bounds.extents.x - offset_circles_x - selected_bounds.size.x, circle_left_ts.position.y);
    }

}
