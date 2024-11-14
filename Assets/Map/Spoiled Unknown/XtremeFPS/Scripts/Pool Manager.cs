using System.Collections.Generic;
using UnityEngine;

namespace XtremeFPS.PoolingSystem
{
    [AddComponentMenu("Spoiled Unknown/XtremeFPS/Pool Manager")]
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance { get; private set; }

        [System.Serializable]
        public class ObjectPoolItem
        {
            public GameObject objectToPool;
            public int objectAmount;
            public bool canExpand;
        }

        public List<ObjectPoolItem> itemsToPool;
        private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
        private Dictionary<GameObject, GameObject> prefabParents = new Dictionary<GameObject, GameObject>();
        private int lastRecycledIndex;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;

            InitializeObjectPools();
        }

        private void InitializeObjectPools()
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool == null)
                {
                    Debug.LogError("The \"Object To Pool\" is null!");
                    return;
                }
                GameObject parentGameObject = new GameObject(item.objectToPool.name + " Pool");
                parentGameObject.transform.parent = transform;

                List<GameObject> objectPool = new List<GameObject>();
                for (int i = 0; i < item.objectAmount; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    obj.transform.parent = parentGameObject.transform;
                    objectPool.Add(obj);
                }
                pooledObjects.Add(item.objectToPool, objectPool);
                prefabParents.Add(item.objectToPool, parentGameObject);
            }
        }

        private ObjectPoolItem FindObjectPoolItem(GameObject objectToPool)
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool == objectToPool) return item;
            }
            return null;
        }

        public GameObject SpawnObject(GameObject objectToPool, Vector3 position, Quaternion rotation)
        {
            ObjectPoolItem item = FindObjectPoolItem(objectToPool);
            if (item == null)
            {
                Debug.LogWarning($"{objectToPool.name} not found in itemsToPool!");
                return null;
            }

            if (!pooledObjects.ContainsKey(objectToPool))
            {
                Debug.LogWarning($"{objectToPool.name} not found in the Pool!");
                return null;
            }

            List<GameObject> objectPool = pooledObjects[objectToPool];
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.SetPositionAndRotation(position, rotation);
                    obj.SetActive(true);
                    return obj;
                }
            }

            if (item.canExpand)
            {
                GameObject newObj = Instantiate(objectToPool, position, rotation);
                newObj.transform.parent = prefabParents[objectToPool].transform;
                newObj.SetActive(true);
                objectPool.Add(newObj);
                return newObj;
            }
            else
            {
                // Циклический рециклинг
                for (int i = 0; i < objectPool.Count; i++)
                {
                    lastRecycledIndex = (lastRecycledIndex + 1) % objectPool.Count;
                    GameObject recycledObj = objectPool[lastRecycledIndex];
                    if (!recycledObj.activeInHierarchy)
                    {
                        recycledObj.transform.SetPositionAndRotation(position, rotation);
                        recycledObj.SetActive(true);
                        return recycledObj;
                    }
                }
            }
            return null;
        }

        public void DespawnObject(GameObject obj)
        {
            bool foundInPool = false;
            foreach (var objectPool in pooledObjects.Values)
            {
                if (objectPool.Contains(obj))
                {
                    obj.SetActive(false);
                    foundInPool = true;
                    break;
                }
            }

            if (!foundInPool) Debug.LogWarning("Object is not managed by the pool system.");
        }
    }
}
