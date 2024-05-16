using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    [SerializeField] public GameObject[] _walls; // 0 - Up 1 - Down 2 - Right 3 - Left
    [SerializeField] public GameObject[] _doors; 

    public void UpdateRoom(bool[] status)
    {
        for (int i  = 0;  i < status.Length; i++)
        {
            _doors[i].SetActive(status[i]);
            _walls[i].SetActive(!status[i]);
        }
    }
}
