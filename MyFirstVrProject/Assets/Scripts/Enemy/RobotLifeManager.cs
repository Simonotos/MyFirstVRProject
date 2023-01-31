using UnityEngine;
using UnityEngine.UI;

public class RobotLifeManager : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    [SerializeField]
    private Image healthbarImage;
    public Gradient gradient;
    [SerializeField]
    private GameObject healthCanvas;

    [SerializeField]
    private RayAttack robotAttack;
    [SerializeField]
    private LaserRobotBehavour robotBehavour;
    [SerializeField]
    private EnemyAttackDetector robotDetector;

    [SerializeField]
    private Transform robotHead;

    [SerializeField]
    private AudioSource myAudioSource;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthbarImage.color = gradient.Evaluate(1f);
        healthCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);

            if (!robotAttack.inAttackRange)
                robotBehavour.inRange(true);

            reduceLife();
        }
    }

    private void updateLifebar()
    {
        float value = currentHealth / maxHealth;
        healthbarImage.fillAmount = Mathf.Clamp01(value);
        healthbarImage.color = gradient.Evaluate(value);
    }

    private void reduceLife()
    {
        myAudioSource.Play();
        currentHealth -= 1;
        updateLifebar();

        if (currentHealth == 0)
            killMe();
    }

    public void activateCanvas(bool value) 
    {
        healthCanvas.SetActive(value);
    }

    private void killMe()
    {
        //disable scritps
        robotAttack.setAttack(false);
        robotAttack.enabled = false;
        robotDetector.enabled = false;
        robotBehavour.enabled = false;
        //start animation
        robotHead.Rotate(-45,0,0, Space.Self);
        this.enabled = false;
    }

    public float getCurrentHealth() {
        return currentHealth;
    }
}
