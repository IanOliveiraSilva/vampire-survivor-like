// AuraVisuals.cs
using System.Collections.Generic;
using UnityEngine;

public class AuraVisuals : MonoBehaviour
{
    [Header("Configura��es do Visual")]
    [Tooltip("O prefab do c�rculo individual que ser� instanciado.")]
    public GameObject circlePrefab;

    [Tooltip("Quantos c�rculos devem girar ao redor.")]
    public int numberOfCircles = 3;

    [Tooltip("A dist�ncia dos c�rculos em rela��o ao centro.")]
    public float orbitDistance = 0.8f;

    [Tooltip("A velocidade com que os c�rculos giram.")]
    public float rotationSpeed = 100f;

    // Lista para guardar a refer�ncia de cada c�rculo criado
    private List<Transform> circles = new List<Transform>();
    private float currentAngle = 0f;

    void Start()
    {
        // Garante que temos um prefab antes de continuar
        if (circlePrefab == null)
        {
            Debug.LogError("O prefab do c�rculo n�o foi atribu�do no AuraVisuals!");
            return;
        }

        // Cria os c�rculos iniciais
        InstantiateCircles();
    }

    void Update()
    {
        // Anima a rota��o dos c�rculos
        RotateCircles();
    }

    private void InstantiateCircles()
    {
        // Cria um c�rculo para cada n�mero definido em numberOfCircles
        for (int i = 0; i < numberOfCircles; i++)
        {
            // Instancia o prefab como um "filho" deste objeto, para que ele se mova junto com o jogador
            GameObject circleInstance = Instantiate(circlePrefab, transform);
            circles.Add(circleInstance.transform);
        }
    }

    private void RotateCircles()
    {
        // Atualiza o �ngulo de rota��o com base na velocidade e no tempo
        currentAngle += rotationSpeed * Time.deltaTime;

        // Calcula o �ngulo de espa�amento entre cada c�rculo
        float angleStep = 360f / numberOfCircles;

        for (int i = 0; i < circles.Count; i++)
        {
            // Calcula o �ngulo espec�fico para este c�rculo
            float angle = (currentAngle + (i * angleStep)) * Mathf.Deg2Rad; // Converte para radianos

            // Calcula a posi��o usando trigonometria (c�rculo trigonom�trico)
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitDistance;

            // Define a posi��o local do c�rculo em rela��o ao seu "pai" (o objeto da aura)
            circles[i].localPosition = offset;
        }
    }
}