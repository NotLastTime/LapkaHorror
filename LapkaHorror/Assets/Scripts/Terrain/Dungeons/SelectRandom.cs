using UnityEngine;

public class SelectRandom : MonoBehaviour
{
    public int _countToLeave = 1;

    private void Start()
    {
        while (transform.childCount > _countToLeave) 
        {
            Transform _childToDestroy = transform.GetChild(Random.Range(0, transform.childCount));
            DestroyImmediate(_childToDestroy.gameObject);
        }
    }
}
