using UnityEngine;

namespace BetterPlan.Pool
{
	public abstract class PooledObject : MonoBehaviour
	{

		public PooledObject Prefab => _prefab;

		PooledObject _prefab;

		public PooledObject Create(Vector3 position = default, Quaternion rotation = default)
		{
			return ObjectPoolingManager.Create(_prefab ? _prefab : this, position, rotation);
		}

		public PooledObject CreateAndSetDefaultParent(Vector3 position = default, Quaternion rotation = default)
		{
			PooledObject createdObject = Create(position, rotation);
			createdObject.SetDefaultParent();

			return createdObject;
		}

		public void SetDefaultParent()
		{
			transform.SetParent(ObjectPoolingManager.GetPool(this).transform);
		}

		public void SetPrefabReference(PooledObject prefab)
		{
			_prefab = prefab;
		}

		public void RequestDestroyThisObject()
		{
			ObjectPoolingManager.Destroy(this);
		}

		public virtual void Activate()
		{
			gameObject.SetActive(true);
		}

		public virtual void Deactivate()
		{
			gameObject.SetActive(false);
		}
	}
}