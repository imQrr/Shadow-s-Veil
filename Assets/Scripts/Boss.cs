using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    float health = 100f;
    float totalHealth;
    bool isDamagable = false;
    bool canAttack = false;
    bool trackPolice = false;
    [SerializeField] float DamagePerSecond = 5f;
    [SerializeField] GameObject Spike;
    [SerializeField] GameObject Police;
    [SerializeField] GameObject Hand;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject HealthParentObject;
    float width, height;
    GameObject newSpike;
    Animator animator;
    AudioSource bossSource;
    [SerializeField] AudioClip bossDying;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        width = HealthBar.GetComponent<Image>().rectTransform.rect.width;
        height = HealthBar.GetComponent<Image>().rectTransform.rect.height;
        totalHealth = health;
        bossSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && isDamagable)
        {
            health = 0;
            isDamagable = false;
            canAttack = false;
            trackPolice = false;
            gameObject.tag = "DeadEnemy";
            animator.SetTrigger("HasDied");
            bossSource.PlayOneShot(bossDying);
            Police.SendMessage("DoorMove");
            StopAllCoroutines();
            StartCoroutine(DeathCleanup());
        }

        if (canAttack)
        {
            Attack(); // Call Attack when boss can attack
        }

        if (trackPolice)
        {
            Vector3 direction = Police.transform.position - transform.position;
            direction.y = 0;

            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    void TakeDamage()
    {
        if (isDamagable)
        {
            health -= DamagePerSecond * Time.deltaTime;
            width = health;
            HealthBar.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(width, height);
        }
    }

    void CanTakeDamage()
    {
        isDamagable = true;
    }

    void CanAttack()
    {
        canAttack = true;
        trackPolice = true;
    }

    void Attack()
    {
        if (canAttack)
        {
            animator.SetTrigger("Throw");
            StartCoroutine(AdjustThrow());
            canAttack = false; // Prevent further attacks until the current one completes
        }
    }

    IEnumerator AdjustThrow()
    {
        yield return new WaitForSeconds(0.7f); // Wait until the boss charges his throw

        Spike.SetActive(true);  // Show the spike when attack is charged

        yield return new WaitForSeconds(0.3f); // Wait until he does the throwing movement, then instantiate a moving spike.
  
        newSpike = GameObject.Instantiate(Spike, Hand.transform.position, gameObject.transform.rotation);
        newSpike.GetComponent<Rigidbody>().isKinematic = false;
        newSpike.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        newSpike.transform.localScale = new Vector3(8, 8, 200);
        Spike.SetActive(false);
        newSpike.transform.LookAt(Police.transform.position);
        newSpike.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1) * 700f);

        Destroy(newSpike, 3.0f);
        yield return new WaitForSeconds(2f);
        canAttack = true; // Allow the boss to attack again, only happens when is damagable.
    }

    IEnumerator DeathCleanup()
    {
        yield return new WaitForSeconds(6f);
        HealthParentObject.SetActive(false);
        Destroy(gameObject);
        SceneManager.LoadScene("Credits");
    }

}