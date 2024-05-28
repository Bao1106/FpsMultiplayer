using System;
using UnityEngine;

namespace Entities.Player
{
    public class Look : MonoBehaviour
    {
        [SerializeField] private Transform entity, eyes;
        [SerializeField] private float xSensitive, ySensitive, maxAngle;
        [SerializeField] private bool isCursorLocked;

        private Quaternion eyesCenter;
        private Camera soldierEyes;
        private float baseFOV;

        public float BaseFOV => baseFOV;
        public Camera SoldierEyes => soldierEyes;
        
        public void SetupEyes()
        {
            eyesCenter = eyes.localRotation;
            soldierEyes = eyes.GetComponent<Camera>();
            baseFOV = soldierEyes.fieldOfView;
        }

        public void EntityLook()
        {
            SetupX();
            SetupY();
            LockCursor();
        }
        
        private void SetupY()
        {
            var input = Input.GetAxis("Mouse Y") * ySensitive * Time.deltaTime;
            var inputAdjust = Quaternion.AngleAxis(input, -Vector3.right);
            var inputDelta = eyes.localRotation * inputAdjust;

            if (Quaternion.Angle(eyesCenter, inputDelta) < maxAngle)
                eyes.localRotation = inputDelta;
        }
        
        private void SetupX()
        {
            var input = Input.GetAxis("Mouse X") * xSensitive * Time.deltaTime;
            var inputAdjust = Quaternion.AngleAxis(input, Vector3.up);
            var inputDelta = entity.localRotation * inputAdjust;
            entity.localRotation = inputDelta;
        }

        private void LockCursor()
        {
            switch (isCursorLocked)
            {
                case true:
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        isCursorLocked = false;
                    }

                    break;
                }
                default:
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        isCursorLocked = true;
                    }

                    break;
                }
            }
        }
    }
}
