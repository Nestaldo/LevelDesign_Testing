using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] float lifePoints;
    [SerializeField] LifeBar lifeBar;
    float takedDmg;

    public void GetDamage(float dmg)
    {
        takedDmg += dmg;
        float currentLife = lifePoints - takedDmg;
        if (currentLife <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            lifeBar.UpdateLifeBar(currentLife, lifePoints);
        }
    }
}
