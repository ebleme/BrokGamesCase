using UnityEngine.Pool;

namespace Ebleme.Utility {

    public interface IPooledObject {

        public void OnCreate(IObjectPool<IPooledObject> pool);

        public void Release();

        public void OnTakeFromPool();

        public void OnReturnedToPool();

        public void OnDestroyPoolObject();
    }
}
