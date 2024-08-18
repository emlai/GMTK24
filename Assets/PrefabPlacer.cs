using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabPlacer : MonoBehaviour
{
    public GameObject[] prefabs;
    public int spawnCount;
    public int maxSpawnDistance;
    public float minScale;
    public float maxScale;
    public bool respawn;

    void OnValidate()
    {
        if (respawn)
        {
            EditorApplication.delayCall += () =>
            {
                respawn = false;
                var j = 0;
                while (transform.childCount != 0)
                {
                    DestroyImmediate(transform.GetChild(0).gameObject);
                    j++;
                    if (j > 2000) break;
                }

                for (var i = 0; i < spawnCount; i++)
                {
                    var spawned = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position + Random.insideUnitSphere * maxSpawnDistance, Random.rotation);
                    spawned.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
                    spawned.transform.parent = transform;
                }
            };
        }
    }
}
