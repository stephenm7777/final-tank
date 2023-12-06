using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

namespace Tank
{
    public class TankInput : MonoBehaviour
    {
        #region Variables 
        [Header("Input Properties")]
        public Camera camera; 
        #endregion

        #region Properties 
        private Vector3 reticlePosition; 
        public Vector3 ReticlePosition {
            get {return reticlePosition;}
        }
        private Vector3 reticleNormal; 
        public Vector3 ReticleNormal{
            get{return reticleNormal;}
        }
        private float forwardInput; 
        public float FowardInput{
            get{return forwardInput;}
        }
        private float rotationInput; 
        public float RotationInput{
            get{return rotationInput;}
        }
        PhotonView view;
        #endregion

        #region BuiltIn Methods 
        void Start(){
            view = GetComponent<PhotonView>();
        }
        void Update(){
            if(view.IsMine){
                if(camera){ 
                    HandleInputs();
                }
            } 
        }

        #endregion

        #region Custom Methods
        protected virtual void HandleInputs(){
            Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if(Physics.Raycast(screenRay, out hit)){
                reticlePosition = hit.point; 
                reticleNormal = hit.normal;
            }
            forwardInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
        }
        #endregion
    }
}
