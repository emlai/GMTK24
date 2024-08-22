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

#if UNITY_EDITOR
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
                    var spawned = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[Random.Range(0, prefabs.Length)]);
                    spawned.transform.position = transform.position + Random.insideUnitSphere * maxSpawnDistance;
                    spawned.transform.rotation = Random.rotation;
                    spawned.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
                    spawned.transform.parent = transform;
                }
            };
        }
    }
#endif
}
