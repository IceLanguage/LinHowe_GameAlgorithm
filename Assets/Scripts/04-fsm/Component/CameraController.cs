using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweFSM
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 focus = Vector3.zero;
        [SerializeField]
        private GameObject focusObj = null;

        void Start()
        {
            if (this.focusObj == null)
                this.setupFocusObject("CameraFocusObject");

            Transform trans = this.transform;
            transform.parent = this.focusObj.transform;

            trans.LookAt(this.focus);

            return;
        }

        private void setupFocusObject(string name)
        {
            GameObject obj = this.focusObj = new GameObject(name);
            obj.transform.position = this.focus;
            obj.transform.LookAt(this.transform.position);

            return;
        }
    }
}

