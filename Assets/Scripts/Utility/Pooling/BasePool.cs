using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Ebleme.Utility {

    public abstract class BasePool : MonoBehaviour {
        [SerializeField] private Enums.PoolType _poolType;

        [SerializeField]
        [Tooltip("Collection checks will throw errors if we try to release an item that is already in the pool.")]
        private bool _collectionChecks = true;

        [SerializeField] private int _maxPoolSize = 1000;

        protected Func<IPooledObject> onCreateRequest;

        private IObjectPool<IPooledObject> _pool;

        public IObjectPool<IPooledObject> Pool {
            get {
                if (_pool == null) {
                    if (_poolType == Enums.PoolType.Stack)
                        _pool = new ObjectPool<IPooledObject>(CreateInstance, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, 10, _maxPoolSize);
                    else
                        _pool = new LinkedPool<IPooledObject>(CreateInstance, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, _maxPoolSize);
                }
                return _pool;
            }
        }

        private IPooledObject CreateInstance() {
            var instance = onCreateRequest();
            instance.OnCreate(Pool);
            return instance;
        }

        private void OnTakeFromPool(IPooledObject instace) {
            instace.OnTakeFromPool();
        }

        private void OnReturnedToPool(IPooledObject instance) {
            instance.OnReturnedToPool();
        }

        private void OnDestroyPoolObject(IPooledObject instance) {
            instance.OnDestroyPoolObject();
        }
    }
}
