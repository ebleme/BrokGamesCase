// maebleme2

using System.Threading.Tasks;
using UnityEngine;

namespace Ebleme
{
    public interface IPlayerFactory
    {
        Task<GameObject> Create(string enemyName);
    }
}