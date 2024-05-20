using UnityEngine;

public class FloorFiller : MonoBehaviour
{
    public GameObject floorPrefab; // Сюда кладем префаб пола
    public float spacing = 1f; // Расстояние между префабами

    void Start()
    {
        Terrain _terrain = GetComponent<Terrain>();
        Vector3 _terrainSize = _terrain.terrainData.size;
        
        for (float x = 0; x < _terrainSize.x; x+=spacing)
        {
            for (float z = 0; z < _terrainSize.z; z+=spacing)
            {
                Vector3 position = new Vector3(x, _terrain.SampleHeight(new Vector3(x, 0, z)) + 0.01f, z);
                Instantiate(floorPrefab, position, Quaternion.identity);
            }
        }
    }

}
