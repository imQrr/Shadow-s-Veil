using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoliceMan : MonoBehaviour
{
    GameObject hiddenDoor;
    bool ShouldMove = false;
    float doorSpeed = 4f;
    [SerializeField] GameObject bossHealth;
    [SerializeField] GameObject[] PlayerHealth;
    [SerializeField] TMP_Text text;
    int totalEnemies = 5;
    int health = 3;
    int enemyKilled = 0;
    AudioSource policeSource;
    [SerializeField] AudioClip bossGrowling;
    [SerializeField] AudioClip takingDamageFromEnemy;
    // Start is called before the first frame update
    void Start()
    {
        hiddenDoor = GameObject.Find("Hidden Door");
        policeSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShouldMove)
        {
            hiddenDoor.transform.position = Vector3.MoveTowards(hiddenDoor.transform.position, new Vector3(13.8129816f, 131.039993f, -138.686996f), doorSpeed * Time.deltaTime);
        }
        else
        {
            hiddenDoor.transform.position = Vector3.MoveTowards(hiddenDoor.transform.position, new Vector3(13.8129816f, 131.039993f - 6.317993f, -138.686996f), doorSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Boss Room Trigger")
        {
            ShouldMove = false;
            other.gameObject.SetActive(false);
            GameObject.Find("Boss").SendMessage("CanAttack");
            GameObject.Find("Boss").SendMessage("CanTakeDamage");
            policeSource.PlayOneShot(bossGrowling);
            bossHealth.SetActive(true);
        }
        if (other.tag == "Spike")
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (health > 1)
        {
            health--;
            PlayerHealth[health].SetActive(false);
            policeSource.PlayOneShot(takingDamageFromEnemy);
        }
        else
        {
            health = 0;
            PlayerHealth[health].SetActive(false);
            SceneManager.LoadScene("GameOver");

        }
    }

    void EnemyKilled()
    {
        enemyKilled++;
        Debug.Log(enemyKilled);
        if (enemyKilled >= totalEnemies)
        {
            ShouldMove = true;
            text.text = "Objective: Eliminate the Master of Shadows";
        }
    }

    void DoorMove()
    {
        ShouldMove = true;
    }
}
