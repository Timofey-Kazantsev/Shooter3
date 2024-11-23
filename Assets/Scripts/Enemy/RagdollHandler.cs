using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XtremeFPS.WeaponSystem
{
    public class RagdollHandler : MonoBehaviour
    {
        private List<Rigidbody> _rigitbodies;

        public void Initialize()
        {
            _rigitbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        }

        public void Enable()
        {
            foreach (Rigidbody rigitbody in _rigitbodies)
            {
                rigitbody.isKinematic = false;
            }
        }

        public void Disable()
        {
            foreach (Rigidbody rigitbody in _rigitbodies)
            {
                rigitbody.isKinematic = true;
            }
        }
    }
}