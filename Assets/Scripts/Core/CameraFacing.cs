using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        // Update is called once per frame
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}