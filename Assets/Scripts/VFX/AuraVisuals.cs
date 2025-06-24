// AuraVisuals.cs
using System.Collections.Generic;
using UnityEngine;

public class AuraVisuals : MonoBehaviour
{
    [Header("Configurações do Visual")]
    [Tooltip("O prefab do círculo individual que será instanciado.")]
    public GameObject circlePrefab;

    [Tooltip("Quantos círculos devem girar ao redor.")]
    public int numberOfCircles = 3;

    [Tooltip("A distância dos círculos em relação ao centro.")]
    public float orbitDistance = 0.8f;

    [Tooltip("A velocidade com que os círculos giram.")]
    public float rotationSpeed = 100f;

    // Lista para guardar a referência de cada círculo criado
    private List<Transform> circles = new List<Transform>();
    private float currentAngle = 0f;

    void Start()
    {
        // Garante que temos um prefab antes de continuar
        if (circlePrefab == null)
        {
            Debug.LogError("O prefab do círculo não foi atribuído no AuraVisuals!");
            return;
        }

        // Cria os círculos iniciais
        InstantiateCircles();
    }

    void Update()
    {
        // Anima a rotação dos círculos
        RotateCircles();
    }

    private void InstantiateCircles()
    {
        // Cria um círculo para cada número definido em numberOfCircles
        for (int i = 0; i < numberOfCircles; i++)
        {
            // Instancia o prefab como um "filho" deste objeto, para que ele se mova junto com o jogador
            GameObject circleInstance = Instantiate(circlePrefab, transform);
            circles.Add(circleInstance.transform);
        }
    }

    private void RotateCircles()
    {
        // Atualiza o ângulo de rotação com base na velocidade e no tempo
        currentAngle += rotationSpeed * Time.deltaTime;

        // Calcula o ângulo de espaçamento entre cada círculo
        float angleStep = 360f / numberOfCircles;

        for (int i = 0; i < circles.Count; i++)
        {
            // Calcula o ângulo específico para este círculo
            float angle = (currentAngle + (i * angleStep)) * Mathf.Deg2Rad; // Converte para radianos

            // Calcula a posição usando trigonometria (círculo trigonométrico)
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitDistance;

            // Define a posição local do círculo em relação ao seu "pai" (o objeto da aura)
            circles[i].localPosition = offset;
        }
    }
}