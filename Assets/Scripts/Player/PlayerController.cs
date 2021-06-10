using MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    [SerializeField] private FloatingJoystickCustom _joystick;

    public float moveHorizontal;
    [Header("Dev mode")]
    [SerializeField] private KeyCode _goFinish;
    private void Awake()
    {
        _player = GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) // jump
        //{
        //    _player.OnJump?.Invoke();
        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A)) //left
        //{
        //    _player.OnChangeLane?.Invoke(1);
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D)) //right
        //{
        //    _player.OnChangeLane?.Invoke(-1);

        //}
        if (Input.GetKeyDown(_goFinish))
        {
            this.gameObject.transform.Translate(Vector3.forward * 250f);
            this.GetComponent<PlayerMovement>().Test();
        }

        moveHorizontal = -_joystick.Horizontal; // left right

    }

}
