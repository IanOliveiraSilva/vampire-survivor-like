using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusItemUI : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image[] levelSquares; // Arraste os quadrados de n�vel aqui no Inspector do prefab

    [Header("Sprites de N�vel")]
    [SerializeField] private Sprite filledSquareSprite;
    [SerializeField] private Sprite emptySquareSprite;

    /// <summary>
    /// Configura a apar�ncia deste item de status com base nos dados fornecidos.
    /// </summary>
    /// <param name="icon">O �cone a ser exibido.</param>
    /// <param name="itemName">O nome do item (arma ou status).</param>
    /// <param name="currentLevel">O n�vel atual do item.</param>
    /// <param name="maxLevel">O n�vel m�ximo que o item pode atingir.</param>
    public void Setup(Sprite icon, string itemName, int currentLevel, int maxLevel)
    {
        iconImage.sprite = icon;
        nameText.text = itemName;

        // Itera por todos os quadrados de n�vel para configur�-los
        for (int i = 0; i < levelSquares.Length; i++)
        {
            if (i < maxLevel)
            {
                // Este quadrado faz parte dos n�veis poss�veis
                levelSquares[i].gameObject.SetActive(true);

                // Define se est� preenchido ou vazio
                if (i < currentLevel)
                {
                    levelSquares[i].sprite = filledSquareSprite;
                }
                else
                {
                    levelSquares[i].sprite = emptySquareSprite;
                }
            }
            else
            {
                // Esconde quadrados que excedem o n�vel m�ximo do item
                levelSquares[i].gameObject.SetActive(false);
            }
        }
    }
}