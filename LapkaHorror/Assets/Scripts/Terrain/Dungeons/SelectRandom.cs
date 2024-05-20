using UnityEngine;

public class SelectRandom : MonoBehaviour
{
    /* �������� ��� �������� �������� � ��������� � ��������� ������� 
     * �� ������ ��������� �������, ������� ��� ����
     * � _countToLeave ��������� ���������� ���������, ������� ������ ��������
     */

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

/* � ��� ���� �� �������, ������ ��� ��� �����-�� ����, �� � �������� �� ������� ������, ������ ���:
 * ������ ����� � �������!!!
 * 1. ��� �������� ��� �������� ������� � ������, �������� ����������� ���� ��������
 * 2. ����� �� ����������� ������� �������� � ��������: ��. ��� �� �� ������ ���� ������ �� �������, ��� �� ������
 * 
 * ����� ����������� ������� ���� ����� ����� ���������, �� �����
 */

//public class SelectRandom : MonoBehaviour
//{
//    public int _countToLeave = 2;

//    private void Start()
//    {
//        // ������� ������ ������
//        Transform[] _doors = new Transform[transform.childCount];
//        for(int i=0; i<transform.childCount; i++)
//        {
//            _doors[i] = transform.GetChild(i);
//        }

//        // ��������� �����
//        while(_doors.Length > _countToLeave)
//        {
//            // �������� ��������� ����� �� �������
//            int _index = Random.Range(0, _doors.Length);
//            Transform _door = _doors[_index];

//            // ������� ������ ��������������� ����� � �������
//            int _oppositeIndex = (_index + 2) % _doors.Length;

//            // ������� ��������������� ����� �� �������
//            Transform _oppositeDoor = _doors[_oppositeIndex];
//            _doors = System.Array.FindAll(_doors, d => d != _oppositeDoor);
//            DestroyImmediate(_oppositeDoor.gameObject);

//            // ������� ��������� ����� �� ������� � �����
//            _doors = System.Array.FindAll(_doors, d => d != _door);
//            DestroyImmediate(_door.gameObject);
//        }
//    }
//}
