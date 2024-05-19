using System;
using Entities.Base;
using Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities.Soldier
{
    public class Soldier : Entity
    {
        [SerializeField] private Look look;
        [SerializeField] private Transform feet;
        [SerializeField] private LayerMask layer;

        private const float SprintModifierFOV = 1.5f;
        private IAudioService audioService;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        
        protected override void Start()
        {
            base.Start();
            GetService();
            look.SetupEyes();
        }

        protected override void FixedUpdate()
        {
            Move();
            Look();
        }

        private void GetService()
        {
            ServiceLocator.For(this).Get(out audioService);
            audioService.AudioSource = GetComponent<AudioSource>();
        }

        private void PlayWalkSound()
        {
            //audioService.PlaySound(IAudioService.AudioType.Walk);
        }

        private void StopJump() => Animator.SetBool(IsJumping, false);
        
        #region Movement

        private new void Move()
        {
            var hMove = Input.GetAxisRaw("Horizontal");
            var vMove = Input.GetAxisRaw("Vertical");
            
            var direction = new Vector3(hMove, 0, vMove).normalized;
            var isMove = hMove != 0 || vMove != 0;
            var isGround = Physics.Raycast(feet.position, Vector3.down, 0.5f, layer);
            var adjustSpeed = 0f;
            
            Jump(isJump =>
            {
                if (!isGround) return;
                
                if (!isJump)
                {
                    Sprint(vMove, tmpSpeed => adjustSpeed = tmpSpeed);
                    //Animator.SetBool(IsMoving, isMove);
                    return;
                }
                
                Rig.AddForce(Vector3.up * JumpForce);
                //Animator.SetBool(IsJumping, true);
            });

            var soldierVelocity = transform.TransformDirection(direction) * (adjustSpeed * Time.deltaTime);
            soldierVelocity.y = Rig.velocity.y;
            
            Rig.velocity = soldierVelocity;
        }

        private void Sprint(float vMove, Action<float> speed)
        {
            var sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            var isSprinting = sprint && vMove > 0;
            var adjustSpeed = Speed;
            
            if (isSprinting)
            {
                adjustSpeed *= SprintModifier;
                look.SoldierEyes.fieldOfView = 
                    Mathf.Lerp(look.SoldierEyes.fieldOfView,look.BaseFOV * SprintModifierFOV, Time.deltaTime * 8f);
            }
            else
            {
                look.SoldierEyes.fieldOfView = 
                    Mathf.Lerp(look.SoldierEyes.fieldOfView,look.BaseFOV, Time.deltaTime * 8f);
            }
            
            speed.Invoke(adjustSpeed);
        }

        private void Jump(Action<bool> checkJump)
        {
            var jump = Input.GetKey(KeyCode.Space);
            var isJump = jump;
            
            checkJump.Invoke(isJump);
        }
        
        private new void Look()
        {
            look.EntityLook();
        }

        #endregion
    }
}
