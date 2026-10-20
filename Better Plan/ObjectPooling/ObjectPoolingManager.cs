using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BetterPlan.Pool
{
	public class ObjectPoolingManager : MonoBehaviour
	{
		private static ObjectPoolingManager _instance;

		HashSet<ObjectPoolHandler> _pools = new();

		public static PooledObject Create(PooledObject prefab, Vector3 position = default, Quaternion rotation = default)
		{
			if (!_instance) CreateInstance();

			PooledObject createdObject = GetPool(prefab).Create();
			createdObject.transform.position = position;
			createdObject.transform.rotation = rotation;

			return createdObject;
		}

		public static void Destroy(PooledObject pooledObjectInstance)
		{
			GetPool(pooledObjectInstance.Prefab).Destroy(pooledObjectInstance);
		}

		public static ObjectPoolHandler GetPool(PooledObject pooledObjectPrefab)
		{
			ObjectPoolHandler pool = _instance._pools.FirstOrDefault(x => x.IsPrefabSame(pooledObjectPrefab));
			if (pool == null) pool = CreatePool(pooledObjectPrefab);
			return pool;
		}

		private static ObjectPoolHandler CreatePool(PooledObject pooledObjectPrefab)
		{
			GameObject gameObject = new GameObject($"{pooledObjectPrefab.name} Pool");
			gameObject.transform.SetParent(_instance.transform);

			ObjectPoolHandler pool = gameObject.AddComponent<ObjectPoolHandler>();

			pool.Init(pooledObjectPrefab);
			_instance._pools.Add(pool);

			return pool;
		}

		private static void CreateInstance()
		{
			GameObject gameObject = new GameObject("Object Pooling Manager");

			_instance = gameObject.AddComponent<ObjectPoolingManager>();
		}
	}
}