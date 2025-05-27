using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // “G‚É–½’†‚µ‚½‚©”»’è
        if (other.CompareTag("Character"))
        {
            // CharacterBase ‚ğæ“¾‚µ‚Äƒ_ƒ[ƒW‚ğ—^‚¦‚é
            CharacterBase target = other.GetComponent<CharacterBase>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
