//using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace BetterPlan.Pool
{
	public abstract class PooledObject : MonoBehaviour
	{
		public Action OnActivated;
		public Action OnDeactivated;

		public PooledObject Prefab => _prefab;

		//[ShowInInspector, ReadOnly] 
		PooledObject _prefab;
		[SerializeField] float _destroyDelay = 0f;

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
			transform.SetParent(ObjectPoolingManager.GetPool(Prefab).transform);
		}

		public void SetPrefabReference(PooledObject prefab)
		{
			_prefab = prefab;
		}

		public virtual void RequestDestroyThisObject()
		{
			if (_destroyDelay == 0) DestroyThisImmidiately();
			else
			{
				TimerManager.Instance.SetTimer(_destroyDelay, this, DestroyThisImmidiately);
			}
		}

		public void DestroyThisImmidiately()
		{
			ObjectPoolingManager.Destroy(this);
		}

		public virtual void Activate()
		{
			gameObject.SetActive(true);
			OnActivated?.Invoke();
		}

		public virtual void Deactivate()
		{
			gameObject.SetActive(false);
			OnDeactivated?.Invoke();
		}
	}
}