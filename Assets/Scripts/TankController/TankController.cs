using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tank { 
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TankInput))]
    public class TankController : MonoBehaviour{
        #region Variables 
        [Header("Movement Properties")]
        private Rigidbody rb;
        private TankInput input; 
        public float tankSpeed = 30f; 
        public float tankRotationSpeed = 30f; 
        public Transform reticleTransform;
        public Transform turrentTransform; 
        #endregion

        #region BuiltInMethods
        void Start(){
            rb = GetComponent<Rigidbody>();
            input = GetComponent<TankInput>();
        }
        
        void FixedUpdate(){
            if(rb && input){
                HandleMovement();
                HandleReticle();
                HandleTurret();
            }
        }
        #endregion

        #region Custom Methods 
        protected virtual void HandleMovement(){
            Vector3 wantedPosition = transform.position + (transform.forward * input.FowardInput * tankSpeed * Time.deltaTime);
            rb.MovePosition(wantedPosition);

            //Rotate the Tank 
            Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput * Time.deltaTime));
            rb.MoveRotation(wantedRotation);
        }
        protected virtual void HandleTurret(){
            if(turrentTransform){
                Vector3 turretLookDir = input.ReticlePosition - turrentTransform.position;
                turretLookDir.y = 0f;
                turrentTransform.rotation = Quaternion.LookRotation(turretLookDir);
            }
        }
        protected virtual void HandleReticle(){
            if(reticleTransform){
                reticleTransform.position = input.ReticlePosition;
            }
        }
        #endregion
        
    }
}