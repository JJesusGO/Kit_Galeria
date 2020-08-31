using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Galeria{

    public class Exhibidor : MonoBehaviour{

        [Header("Posición")]
        [Tooltip("Velocidad de ajuste a la posición.")]
        [SerializeField]
        private float velocidadLineal = 12;
        [Tooltip("Distancia minima que debe existir entra la entidad y la posición para ser detectada.")]
        private float precisionDeteccion = 0.01f;
        [Header("Rotación")]
        [Tooltip("Velocidad de ajuste a la rotación.")]
        [SerializeField]
        private float velocidadAngular = 40.0f;
        [Header("Mouse")]
        [Tooltip("Sensibilidad del mouse.")]
        [SerializeField]
        private float sensibilidad = 2.0f;
        [Header("Eventos")]
        [SerializeField]
        private UnityEvent eventoentrada = null;
        [SerializeField]
        private UnityEvent eventosalida = null;

        private bool elemento = false;

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
            
            if (!elemento){            
                if (entidad != null)
                    if (IsPosicion(entidad)){
                        elemento = true;
                        ActivarEntrada();
                    }                                   
            }

        }

        public void ActivarTrigger(string nombre){
            if (entidad == null)
                return;
            if(entidad.GetAnimador()==null)
                return;
            entidad.GetAnimador().ActivarTrigger(nombre);
        }

        private void ActivarEntrada(){
            if (eventoentrada == null)
                return;
            eventoentrada.Invoke();        
        }
        private void ActivarSalida(){
            if (eventosalida == null)
                return;
            eventosalida.Invoke();        
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
                if (this.entidad != null){
                    this.entidad.SetEstado(EntidadGaleriaEstado.SALIDA, salida); 
                    elemento = false;
                    ActivarSalida();
                }
                this.entidad = entidad;
            }
            else{
                if (this.entidad != null){
                    this.entidad.SetEstado(EntidadGaleriaEstado.SALIDA, salida);  
                    elemento = false;
                    ActivarSalida();
                }
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

        public bool       IsPosicion(EntidadGaleria entidad){
            Vector3 distancia = transform.position - entidad.GetPosicion();
            return distancia.magnitude <= precisionDeteccion;                
        }

    }

}

