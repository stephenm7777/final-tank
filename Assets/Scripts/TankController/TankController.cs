using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
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
        PhotonView view; 
        #endregion

        #region BuiltInMethods
        void Start(){
            rb = GetComponent<Rigidbody>();
            input = GetComponent<TankInput>();
            view = GetComponent<PhotonView>();
        }
        
        void FixedUpdate(){
        if(view.IsMine){
                if(rb && input){
                HandleMovement();
            }
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

        #endregion
        
    }
}