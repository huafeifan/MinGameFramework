using System.Collections;
using UnityEngine;

namespace CustomNamespace
{
    public class BaseManager : MonoBehaviour
    {
        public virtual IEnumerator Register() 
        {
            yield return null;
        }

        public virtual IEnumerator Unregister() 
        {
            yield return null;
        }

    }
}
