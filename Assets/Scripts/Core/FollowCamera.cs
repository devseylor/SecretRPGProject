using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _target;

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}