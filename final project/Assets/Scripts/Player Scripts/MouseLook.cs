using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot , lookRoot;
    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private int smooth_step = 10;

    [SerializeField]
    private float smooth_weight = 0.4f;
    [SerializeField]
    private float roll_angle = 10f;
    [SerializeField]
    private float roll_speed = 5f; // extend feature

    [SerializeField]
    private Vector2 default_Look_limits = new Vector2 (-70f, 80f);

    private Vector2 look_angle;

    private Vector2 current_mouse_look;
    private Vector2 smooth_move;

    private float current_roll_angle;

    private int last_look_frame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked){
            LookAround();
        }
    }

    void LockAndUnlockCursor(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (Cursor.lockState == CursorLockMode.Locked){
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible =false;
            }
        }
    }

    void LookAround(){
        current_mouse_look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X)
        );
        
        look_angle.x += current_mouse_look.x * sensivity * (invert ? 1f : -1f); // change mouse look, invert it 
        look_angle.y += current_mouse_look.y * sensivity;
        
        

        look_angle.x = Mathf.Clamp(look_angle.x, default_Look_limits.x, default_Look_limits.y);

        current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw(MouseAxis.MOUSE_X)*roll_angle/5, Time.deltaTime  *roll_speed);

        lookRoot.localRotation = Quaternion.Euler(look_angle.x , 0f, current_roll_angle);
        playerRoot.localRotation= Quaternion.Euler(0f, look_angle.y , 0f);
    }
}
