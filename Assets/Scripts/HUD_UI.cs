using UnityEngine;
using UnityEngine.UI;

public class HUD_UI : MonoBehaviour
{
    [Header("Player")]
    public Health playerHealth;
    public Slider playerSlider;
    public Image playerFill;
    public Gradient healthGradient;

    [Header("Nemici")]
    public Health[] enemies;
    public Slider[] enemySliders;

    void Update()
    {
        // Barra player con cambio colore
        if (playerHealth != null && playerSlider != null)
        {
            float pct = playerHealth.currentHealth / playerHealth.maxHealth;
            playerSlider.value = pct;
            if (playerFill != null)
                playerFill.color = healthGradient.Evaluate(pct);
        }

        // Barre nemici — si nascondono quando il nemico muore
        for (int i = 0; i < enemies.Length && i < enemySliders.Length; i++)
        {
            if (enemySliders[i] == null) continue;

            if (enemies[i] != null)
            {
                enemySliders[i].gameObject.SetActive(true);
                enemySliders[i].value = enemies[i].currentHealth / enemies[i].maxHealth;
            }
            else
            {
                enemySliders[i].gameObject.SetActive(false);
            }
        }
    }
}