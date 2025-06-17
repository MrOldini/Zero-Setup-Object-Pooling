using UnityEngine;

namespace BetterPlan.Pool
{
    public class ObjectPoolingSpawner : MonoBehaviour
    {
        [SerializeField] PooledObject _objectPrefab;
        [SerializeField] bool _setDefaultParent = true;

        public void Spawn()
        {
            if (_setDefaultParent)
                _objectPrefab.CreateAndSetDefaultParent(transform.position, transform.rotation);
            else
                _objectPrefab.Create(transform.position, transform.rotation);
        }
    }
}