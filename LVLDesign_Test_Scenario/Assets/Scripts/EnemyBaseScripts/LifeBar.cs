using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Image lifeBar;
    [SerializeField] TMPro.TextMeshProUGUI txtLife;
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        if(canvas!= null && canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
        }
    }

    public void UpdateLifeBar(float currentLife, float lifePoints)
    {
        float fillAmount = currentLife / lifePoints;
        lifeBar.fillAmount = fillAmount;
        txtLife.text = currentLife.ToString("0");
    }
}
