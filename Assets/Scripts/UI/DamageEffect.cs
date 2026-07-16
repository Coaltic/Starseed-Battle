using UnityEngine;
using TMPro;

public class DamageEffect : MonoBehaviour
{
    public GameObject damageEffectObject;
    public TMP_Text damageEffectText;
    public float fadeTimer = 1.0f;

    private void Awake()
    {
        damageEffectObject = this.gameObject;
        damageEffectText = this.GetComponent<TMP_Text>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fadeTimer -= Time.deltaTime;
        damageEffectObject.transform.position = new Vector2(damageEffectObject.transform.position.x, damageEffectObject.transform.position.y + 0.005f);

        if (fadeTimer <= 0.0f)
        {
            FadeOut();
        }
    }

    public void SetText(int hpText)
    {
        if (hpText < 0)
        {
            damageEffectText.color = Color.red;
            damageEffectText.text = hpText.ToString();
        }
    }
    void FadeOut()
    {
        damageEffectText.alpha -= 0.025f;
        if (damageEffectText.alpha <= 0.0f) Destroy(this.gameObject);
    }
}
