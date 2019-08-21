using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    private bool is_Dead;

    private EnemyAudio enemyAudio;

    // private PlayerStats player_Stats;

    void Awake()
    {
        // player_Stats = GetComponent<PlayerStats>();
    }

    public void ApplyDamage(float damage)
    {

        // if we died don't execute the rest of the code
        if (is_Dead)
            return;

        health -= damage;
    
        // show the stats(display the health UI value)
        // player_Stats.Display_HealthStats(health);
    
        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }

    } // apply damage

    void PlayerDied()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().enabled = false;
        }

        // call enemy manager to stop spawning enemis
        EnemySpawn.instance.StopSpawning();

        // GetComponent<PlayerMovement>().enabled = false;
        // GetComponent<PlayerAttack>().enabled = false;
        // GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        Invoke("RestartGame", 3f);
    } // player died

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }

} // class
