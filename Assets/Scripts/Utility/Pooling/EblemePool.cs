// maebleme2

using System.Collections.Generic;
using UnityEngine;

namespace Utility.Pooling
{
    using System.Collections.Generic;
    using UnityEngine;

    public class EblemePool<T> where T : Component
    {
        private readonly Queue<T> pool = new Queue<T>();
        private readonly List<T> activeObjects = new List<T>(); // Havuzdan alınan objeleri takip etmek için
        private readonly T prefab;
        private readonly Transform parent;

        public EblemePool()
        {
            
        }
        
        public EblemePool(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                AddToPool(CreateNewInstance());
            }
        }

        // Havuzdan nesne al
        public T Get(Vector3 position = default, Quaternion rotation = default)
        {
            T instance;

            if (pool.Count > 0)
            {
                instance = pool.Dequeue();
            }
            else
            {
                instance = CreateNewInstance();
            }

            instance.gameObject.SetActive(true);
            instance.transform.SetPositionAndRotation(position, rotation);
        
            activeObjects.Add(instance); // Takip listesine ekle
            return instance;
        }

        // Nesneyi havuza geri gönder
        public void ReturnToPool(T instance)
        {
            if (activeObjects.Remove(instance)) // Eğer gerçekten aktif listede varsa
            {
                instance.gameObject.SetActive(false);
                AddToPool(instance);
            }
        }

        // Tüm dequeued edilen objeleri geri gönder
        public void ReturnAll()
        {
            for (int i = activeObjects.Count - 1; i >= 0; i--)
            {
                ReturnToPool(activeObjects[i]);
            }
        }

        // Yeni bir nesne oluştur
        private T CreateNewInstance()
        {
            var instance = UnityEngine.Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            return instance;
        }

        // Havuza nesne ekle
        private void AddToPool(T instance)
        {
            pool.Enqueue(instance);
        }
    }

    // public class EblemePool<T> where T : Component
    // {
    //     private readonly Queue<T> pool = new Queue<T>();
    //     private readonly T prefab;
    //     private readonly Transform parent;
    //
    //     public EblemePool(T prefab, int initialSize, Transform parent = null)
    //     {
    //         this.prefab = prefab;
    //         this.parent = parent;
    //
    //         for (int i = 0; i < initialSize; i++)
    //         {
    //             AddToPool(CreateNewInstance());
    //         }
    //     }
    //
    //     // Havuzdan nesne al
    //     public T Get(Vector3 position = default, Quaternion rotation = default)
    //     {
    //         T instance;
    //
    //         if (pool.Count > 0)
    //         {
    //             instance = pool.Dequeue();
    //             instance.gameObject.SetActive(true);
    //         }
    //         else
    //         {
    //             instance = CreateNewInstance();
    //         }
    //
    //         instance.transform.SetPositionAndRotation(position, rotation);
    //         return instance;
    //     }
    //
    //     // Nesneyi havuza geri gönder
    //     public void ReturnToPool(T instance)
    //     {
    //         instance.gameObject.SetActive(false);
    //         AddToPool(instance);
    //     }
    //
    //     // Yeni bir nesne oluştur
    //     private T CreateNewInstance()
    //     {
    //         var instance = UnityEngine.Object.Instantiate(prefab, parent);
    //         instance.gameObject.SetActive(false);
    //         return instance;
    //     }
    //
    //     // Havuza nesne ekle
    //     private void AddToPool(T instance)
    //     {
    //         pool.Enqueue(instance);
    //     }
    // }
}