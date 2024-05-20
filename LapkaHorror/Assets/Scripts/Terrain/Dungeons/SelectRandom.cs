using UnityEngine;

public class SelectRandom : MonoBehaviour
{
    /* Выбираем все дочерние элементы и выключаем в случайном порядке 
     * На выходе останется столько, сколько нам надо
     * В _countToLeave указываем количество элементов, которые должны остаться
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

/* В код ниже не смотрим, потому как там какая-то лажа, да и работать по индексу нельзя, потому что:
 * СТОИТЬ ЗНАТЬ И ПОМНИТЬ!!!
 * 1. Нет гарантии что элементы попадут в массив, согласно выстроенной нами иерархии
 * 2. Юнити не гарантирует порядок объектов в иерархии: то. что он по списку идет первый не значить, что он первый
 * 
 * Стоит попробовать сделать тоже самое через коллекции, но потом
 */

//public class SelectRandom : MonoBehaviour
//{
//    public int _countToLeave = 2;

//    private void Start()
//    {
//        // Созадем массив дверей
//        Transform[] _doors = new Transform[transform.childCount];
//        for(int i=0; i<transform.childCount; i++)
//        {
//            _doors[i] = transform.GetChild(i);
//        }

//        // Отключаем двери
//        while(_doors.Length > _countToLeave)
//        {
//            // Выбираем случайную дверь из массива
//            int _index = Random.Range(0, _doors.Length);
//            Transform _door = _doors[_index];

//            // Находим индекс противоположной двери в массиве
//            int _oppositeIndex = (_index + 2) % _doors.Length;

//            // Удаляем противоположную дверь из массива
//            Transform _oppositeDoor = _doors[_oppositeIndex];
//            _doors = System.Array.FindAll(_doors, d => d != _oppositeDoor);
//            DestroyImmediate(_oppositeDoor.gameObject);

//            // Удаляем выбранную дверь из массива и сцены
//            _doors = System.Array.FindAll(_doors, d => d != _door);
//            DestroyImmediate(_door.gameObject);
//        }
//    }
//}
