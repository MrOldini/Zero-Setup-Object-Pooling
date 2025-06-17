using UnityEngine;
using UnityEngine.Pool;

namespace BetterPlan.Pool
{
	public class ObjectPoolHandler : MonoBehaviour
	{
		public bool IsPrefabSame(PooledObject prefab) => prefab == _objectPrefab;

		[SerializeField] PooledObject _objectPrefab;
		[SerializeField] bool _collectionCheck = true;
		[SerializeField] int _defaultSize = 10;
		[SerializeField] int _maxSize = 50;

		ObjectPool<PooledObject> _pool;

		public void Init(PooledObject pooledObjectPrefab)
		{
			_objectPrefab = pooledObjectPrefab;

			_pool = new ObjectPool<PooledObject>(
				() =>
				{
					PooledObject createdObject = Instantiate(_objectPrefab);
					createdObject.SetPrefabReference(_objectPrefab);
					return createdObject;
				},
				(obj) => obj.Activate(),
				(obj) => obj.Deactivate(),
				(obj) => Destroy(obj.gameObject),
				_collectionCheck, _defaultSize, _maxSize);
		}

		public PooledObject Create()
		{
			return _pool.Get();
		}

		public void Destroy(PooledObject objectInstance)
		{
			_pool.Release(objectInstance);
		}
	}
}