using UnityEngine;

public class CanvasGroupUIAnimation : MonoBehaviour
{
    [Header("Referencias")]
    public CanvasGroup canvasGroup;               // Referencia al CanvasGroup
    public RectTransform rectTransform;           // Referencia al RectTransform

    [Header("Animación de Posición")]
    public Vector2 startPosition;                 // Posición inicial (anchoredPosition)
    public Vector2 endPosition;                   // Posición final (anchoredPosition)

    [Header("Parámetros de Tiempo")]
    public float delayBeforeStart = 1f;           // Tiempo antes de comenzar
    public float effectDuration = 2f;             // Duración del efecto

    [Header("Interpolación")]
    public AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1); // Curva de interpolación

    private void Start()
    {
        if (canvasGroup == null || rectTransform == null)
        {
            Debug.LogError("Faltan referencias al CanvasGroup o RectTransform.");
            return;
        }

        // Inicializa al estado inicial
        rectTransform.anchoredPosition = startPosition;
        canvasGroup.alpha = 0f;

        // Inicia la corrutina
        StartCoroutine(AnimateUI());
    }

    private System.Collections.IEnumerator AnimateUI()
    {
        // Espera antes de iniciar
        yield return new WaitForSeconds(delayBeforeStart);

        float elapsedTime = 0f;

        while (elapsedTime < effectDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / effectDuration);
            float curveValue = animationCurve.Evaluate(t);

            // Interpolación de posición y alpha
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, curveValue);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, curveValue);

            yield return null;
        }

        // Asegura estado final
        rectTransform.anchoredPosition = endPosition;
        canvasGroup.alpha = 1f;
    }
}
