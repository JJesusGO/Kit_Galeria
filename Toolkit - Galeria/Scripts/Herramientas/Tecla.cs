using UnityEngine;
using System.Collections;

namespace Galeria{

    [System.Serializable]
    public class Tecla{

        [SerializeField]
        private string key = "";

        public Tecla(string key){
            this.key = key;        
        }
        public Tecla(){
            this.key = "";
        }

        public bool IsClick(){
            return Input.GetKey(key);
        }
        public bool IsClickDown(){
            return Input.GetKeyDown(key);
        }
        public bool IsClickUp(){
            return Input.GetKeyUp(key);
        }
     

    }

}