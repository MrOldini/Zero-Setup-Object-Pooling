using System;
using UnityEngine;

namespace BetterPlan.Pool
{
	public class ObjectPoolingSpawner : MonoBehaviour
	{
		[SerializeField] PooledObject _objectPrefab;
		[SerializeField] bool _setDefaultParent = true;

		PooledObject _mostRecentSpawnedObject;


		public void Spawn()
		{
			if (_mostRecentSpawnedObject != null) _mostRecentSpawnedObject.OnDeactivated -= ObjectDeactivated;

			if (_setDefaultParent)
				_mostRecentSpawnedObject = _objectPrefab.CreateAndSetDefaultParent(transform.position, transform.rotation);
			else
				_mostRecentSpawnedObject = _objectPrefab.Create(transform.position, transform.rotation);

			_mostRecentSpawnedObject.OnDeactivated += ObjectDeactivated;
		}

		private void ObjectDeactivated()
		{
			_mostRecentSpawnedObject.OnDeactivated -= ObjectDeactivated;
			_mostRecentSpawnedObject = null;
		}

		public void TryDestroyMostRecentSpawnedObject()
		{
			if (_mostRecentSpawnedObject) _mostRecentSpawnedObject.RequestDestroyThisObject();
		}
	}
}