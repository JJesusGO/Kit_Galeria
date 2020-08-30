using UnityEngine;
using System.Collections;

namespace Galeria{

    public class EntidadPosicion : MonoBehaviour{

        [Header("Posición")]
        [Tooltip("Velocidad de ajuste a la posición.")]
        [SerializeField]
        private float velocidadLineal = 12;
        [SerializeField]
        [Tooltip("Distancia minima que debe existir entra la entidad y la posición para ser detectada.")]
        private float precisionDeteccion = 0.01f;
        [Header("Rotación")]
        [Tooltip("¿La entidad debe ajustarse a la rotación de este elemento?.")]
        [SerializeField]
        private bool forzarrotacion = true;
        [Tooltip("Velocidad de ajuste a la rotacion.")]
        [SerializeField]
        private float velocidadAngular = 20;


        public Quaternion GetRotacion(){
            return transform.rotation;
        }
        public Vector3    GetPosicion(){
            return transform.position;
        }

        public float      GetVelocidadLineal(){
            return velocidadLineal;
        }
        public float      GetVelocidadAngular(){
            return velocidadAngular;
        }

        public bool       IsForzarRotacion(){
            return forzarrotacion;
        }
        public bool       IsPosicion(EntidadGaleria entidad){
            Vector3 distancia = transform.position - entidad.GetPosicion();
            return distancia.magnitude <= precisionDeteccion;                
        }


    }

}

