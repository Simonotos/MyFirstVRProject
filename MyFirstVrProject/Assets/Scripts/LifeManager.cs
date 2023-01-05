using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    [SerializeField]
    private Image healthbarImage;
    public Gradient gradient;
    [SerializeField]
    private GameObject healthCanvas;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthbarImage.color = gradient.Evaluate(1f);
        activateCanvas(false);
    }

    public void onCollisionDetect()
    {
        activateCanvas(true);
        reduceLife();
    }

    private void updateLifebar()
    {
        float value = currentHealth / maxHealth;
        healthbarImage.fillAmount = Mathf.Clamp01(value);
        healthbarImage.color = gradient.Evaluate(value);
    }

    private void reduceLife()
    {
        currentHealth -= 1;
        updateLifebar();

        if (currentHealth == 0)
            killMe();
    }

    public void activateCanvas(bool value)
    {
        healthCanvas.SetActive(value);
    }

    public void killMe()
    {
        Destroy(this.gameObject);
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
}
