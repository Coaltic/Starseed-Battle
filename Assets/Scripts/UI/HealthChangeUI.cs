using UnityEngine;
using TMPro;

public class HealthChangeUI : MonoBehaviour
{
    public GameObject healthChangeObject;
    public TMP_Text healthChangeText;
    public float fadeTimer = 1.0f;

    private void Awake()
    {
        healthChangeObject = this.gameObject;
        healthChangeText = this.GetComponent<TMP_Text>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fadeTimer -= Time.deltaTime;
        healthChangeObject.transform.position = new Vector2(healthChangeObject.transform.position.x, healthChangeObject.transform.position.y + 0.005f);

        if (fadeTimer <= 0.0f)
        {
            FadeOut();
        }
    }

    public void SetText(int hpText)
    {
        if (hpText < 0)
        {
            healthChangeText.color = Color.red;
            healthChangeText.text = "-" + hpText;
        }
    }
    void FadeOut()
    {
        healthChangeText.alpha -= 0.025f;
        if (healthChangeText.alpha <= 0.0f) Destroy(this.gameObject);
    }
}
