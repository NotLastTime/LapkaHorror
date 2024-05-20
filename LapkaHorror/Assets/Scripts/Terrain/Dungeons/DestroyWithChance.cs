using UnityEngine;

public class DestroyWithChance : MonoBehaviour
{
    [Range(0, 1)]
    public float _ChanceOfStay = 0.5f;

    private void Start()
    {
        if (Random.value > _ChanceOfStay)
            Destroy(gameObject);
    }
}
