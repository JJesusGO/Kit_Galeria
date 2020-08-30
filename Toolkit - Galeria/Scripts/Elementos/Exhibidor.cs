using UnityEngine;
using System.Collections;

namespace Galeria{

    public class Exhibidor : MonoBehaviour{

        [Header("Posición")]
        [Tooltip("Velocidad de ajuste a la posición.")]
        [SerializeField]
        private float velocidadLineal = 12;
        [Header("Rotación")]
        [Tooltip("Velocidad de ajuste a la rotación.")]
        [SerializeField]
        private float velocidadAngular = 40.0f;
        [Header("Mouse")]
        [Tooltip("Sensibilidad del mouse.")]
        [SerializeField]
        private float sensibilidad = 2.0f;
       
        private EntidadGaleria  entidad = null;
        private Temporizador temporizador = null;

        private EntidadGaleriaDireccion salida  = EntidadGaleriaDireccion.IZQUIERDA;
        private EntidadGaleriaDireccion entrada  = EntidadGaleriaDireccion.DERECHA;

        private void Start(){        
            temporizador = new Temporizador(0.5f);
        }
        private void Update(){

            temporizador.Update();
            if (Input.GetMouseButton(0))
            {
                if (temporizador.GetTiempo(true) <= 0)
                    temporizador.Start();
                if (temporizador.IsActivo())
                {
                    Vector2 direccion = new Vector2(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                    if (direccion.magnitude > 0)
                    {
                        direccion /= direccion.magnitude;
                        transform.Rotate(new Vector3(0, direccion.x * sensibilidad, 0));
                    }
                }
            }
            else
                temporizador.Reset();
            

        
        }

        public void ActivarTrigger(string nombre){
            if (entidad == null)
                return;
            if(entidad.GetAnimador()==null)
                return;
            entidad.GetAnimador().ActivarTrigger(nombre);
        }

        public void           SetEntidadGaleria(EntidadGaleria entidad,EntidadGaleriaDireccion entrada){
            salida = EntidadGaleriaDireccion.DESCONOCIDO;
            switch (entrada){
                case EntidadGaleriaDireccion.DERECHA:
                    salida = EntidadGaleriaDireccion.IZQUIERDA;
                    break;
                case EntidadGaleriaDireccion.IZQUIERDA:
                    salida = EntidadGaleriaDireccion.DERECHA;
                    break;
            }
            if (entidad == null){
                if (this.entidad != null)
                    this.entidad.SetEstado(EntidadGaleriaEstado.SALIDA,salida);                
                this.entidad = entidad;
            }
            else{
                if (this.entidad != null)
                    this.entidad.SetEstado(EntidadGaleriaEstado.SALIDA,salida);                
                this.entidad = entidad;
                this.entidad.SetEstado(EntidadGaleriaEstado.ENTRADA,entrada);
            }
        }
        public EntidadGaleria GetEntidadGaleria(){
            return entidad;
        }

        public            Vector3    GetPosicion(){
            return transform.position;
        }
        public            Quaternion GetRotacion(){
            return transform.rotation;
        }

        public float      GetVelocidadLineal(){
            return velocidadLineal;
        }
        public float      GetVelocidadAngular(){
            return velocidadAngular;
        }        

        public EntidadGaleriaDireccion GetEntrada(){
            return entrada;
        }
        public EntidadGaleriaDireccion GetSalida(){
            return salida;
        }

    }

}

