// maebleme2

using System.Threading.Tasks;
using UnityEngine;

namespace Ebleme
{
    public interface IPlayerProvider
    {
        Task<GameObject> Load(string enemyName);
    }
}