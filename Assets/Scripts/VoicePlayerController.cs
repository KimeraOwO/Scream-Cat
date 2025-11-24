using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class VoicePlayerController : MonoBehaviour
{
    [Header("Configuración de Voz")]
    public float sensitivity = 100f;
    [Range(0f, 1f)]
    public float loudnessThreshold = 0.1f;

    [Header("Físicas")]
    public float maxVelocity = 10f;

    [Header("Gráficos (Sprites)")]
    [Tooltip("Arrastra aquí la imagen del gato normal.")]
    public Sprite normalSprite;
    [Tooltip("Arrastra aquí la imagen del gato gritando/saltando.")]
    public Sprite screamingSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float currentLoudness;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (normalSprite != null) spriteRenderer.sprite = normalSprite;
    }

    void Update()
    {
        currentLoudness = MicManager.Instance.GetLoudnessFromMic();

        UpdateSpriteState();
    }

    void FixedUpdate()
    {
        if (currentLoudness > loudnessThreshold)
        {
            rb.AddForce(Vector2.up * (currentLoudness * sensitivity));
        }

        if (rb.linearVelocity.y > maxVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxVelocity);
        }
    }

    void UpdateSpriteState()
    {
        if (normalSprite == null || screamingSprite == null) return;

        if (currentLoudness > loudnessThreshold)
        {
            spriteRenderer.sprite = screamingSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}