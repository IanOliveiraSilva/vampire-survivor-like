using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    private float lifetime = 1f;
    private float elapsedTime = 0f;


    public void Setup(float damageAmount, Color color)
    {
        damageText.text = damageAmount.ToString("F0");
        damageText.color = color;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
