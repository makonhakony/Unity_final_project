using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_sound;

    [SerializeField]
    private AudioClip[] footstep_clip;

    private CharacterController character_controller;

    [HideInInspector]
    public float volume_min, volume_max;

    private float accumulated_distance;

    [HideInInspector]
    public float step_distance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        footstep_sound = GetComponent<AudioSource>();

        character_controller = GetComponentInParent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound(){
        if (!character_controller.isGrounded)
            return;

        if(character_controller.velocity.sqrMagnitude > 0){
            accumulated_distance += Time.deltaTime;

            if (accumulated_distance >step_distance){

                footstep_sound.volume = Random.Range(volume_min,volume_max);
                footstep_sound.clip = footstep_clip[Random.Range(0,footstep_clip.Length)];
                footstep_sound.Play();

                accumulated_distance = 0f;
            }
        }
        else {
            accumulated_distance= 0f;
        }
    }
}
