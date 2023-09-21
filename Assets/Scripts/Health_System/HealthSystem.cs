using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private float health;
    private float lerpTimer;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float chipSpeed = 2f;

    [SerializeField] private Slider frontHealthSlider;
    [SerializeField] private Slider backHealthSlider;

    [SerializeField] private Image backHealthImage;

    [SerializeField] private bool canDie = false;

    #region Delegates

    public delegate void OnDeathEvent();
    public OnDeathEvent onDeath;

    #endregion

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (frontHealthSlider || backHealthSlider || backHealthImage)
            UpdateHealthUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageOutput")
        {
            TakeDamage(Random.Range(5, 10));
        }

        if (other.gameObject.tag == "HealingObject")
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        //Debug.Log($"Current Health: {health}");

        float healthFront = frontHealthSlider.value;
        float healthBack = backHealthSlider.value;

        float hFraction = health / maxHealth;

        if (healthBack > hFraction)
        {
            frontHealthSlider.value = hFraction;
            lerpTimer += Time.deltaTime;
            backHealthImage.color = new Color32(149, 25, 27, 255);
            float percentComplete = lerpTimer / chipSpeed;
            backHealthSlider.value = Mathf.Lerp(healthBack, hFraction, percentComplete);
        }

        if (healthFront < hFraction)
        {
            backHealthSlider.value = hFraction;
            lerpTimer += Time.deltaTime;
            backHealthImage.color = new Color32(25, 149, 119, 255);
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthSlider.value = Mathf.Lerp(healthFront, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    { 
        health -= damage;
        lerpTimer = 0f;

        Debug.Log($"{this.name}'s health left: {health}");

        if (health <= 0)
        {
            if (canDie)
                onDeath?.Invoke();
            else
                Destroy(transform.gameObject);
        }
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
}
