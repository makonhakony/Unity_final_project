using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float sprint_speed = 10f;
    public float move_speed = 5f;
    public float crouch_speed = 2f;

    private Transform look_root;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;

    private bool isCrouching;

    private PlayerFootsteps player_footstep;

    private float sprint_volume = 1f;
    
    private float crouch_volume = 0.1f;

    private float walk_volume_min = 0.2f , walk_volume_max =0.6f;

    private float walk_step_distance = 0.4f;

    private float sprint_step_distance = 0.25f;

    private float crouch_step_distance = 0.5f;


    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_root = transform.GetChild(0);

        player_footstep = GetComponentInChildren<PlayerFootsteps>();
    }

    void Start()
    {
        player_footstep.volume_max = walk_volume_max;
        player_footstep.volume_min = walk_volume_min;
        player_footstep.step_distance = walk_step_distance;

    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint(){
        if (Input.GetKeyDown(KeyCode.LeftShift)&& ! isCrouching){
            playerMovement.speed = sprint_speed;
            player_footstep.step_distance = sprint_step_distance;;
            player_footstep.volume_max = sprint_volume;
            player_footstep.volume_min = sprint_volume;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)&& !isCrouching){
            playerMovement.speed = move_speed;
            player_footstep.volume_max = walk_volume_max;
            player_footstep.volume_min = walk_volume_min;
            player_footstep.step_distance = walk_step_distance;
        }
    }

    void Crouch(){
        if (Input.GetKeyDown(KeyCode.C)){
            if (isCrouching){
                look_root.localPosition = new Vector3(0f,stand_height,0f);
                playerMovement.speed = move_speed;

                player_footstep.volume_max = walk_volume_max;
                player_footstep.volume_min = walk_volume_min;
                player_footstep.step_distance = walk_step_distance;

                isCrouching = false;
            }
            else {
                look_root.localPosition = new Vector3(0f,crouch_height,0f);
                playerMovement.speed = crouch_speed;

                player_footstep.volume_max = crouch_volume;
                player_footstep.volume_min = crouch_volume;
                player_footstep.step_distance = crouch_step_distance;

                isCrouching = true;
            }
        }
        
    }
}
