using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Galeria{

    [System.Serializable]
    public struct Metadata{
        #pragma warning disable CS0649
        [SerializeField]
        private string nombre;
        [SerializeField]
        private string contenido;
        #pragma warning restore CS0649

        public string GetNombre(){
            return nombre;
        }
        public string GetContenido(){
            return contenido;
        }

        public bool IsNombre(string nombre){
            return this.nombre.Equals(nombre);
        }
    }

    public enum EntidadGaleriaEstado{
        ALMACEN,ENTRADA,SALIDA,EXHIBIDA
    }
    public enum EntidadGaleriaDireccion{
        IZQUIERDA,DERECHA,DESCONOCIDO
    }

    public class EntidadGaleria : MonoBehaviour{

        [Header("Información")]
        [SerializeField]
        private Metadata []data = null;
        [Header("Estados")]
        [SerializeField]
        private EntidadPosicion almacen = null;
        [Tooltip("none: La entidad se movera directamente a almacen/exhibidor.")]
        [SerializeField]
        private EntidadPosicion izquierda = null;
        [Tooltip("none: La entidad se movera directamente a almacen/exhibidor.")]
        [SerializeField]
        private EntidadPosicion derecha = null;
        [Header("Eventos")]
        [SerializeField]
        private UnityEvent eventosalmacen = null;
        [SerializeField]
        private UnityEvent eventosentrada = null;
        [SerializeField]
        private UnityEvent eventossalida = null;
        [SerializeField]
        private UnityEvent eventosexhibidor = null;

        private EntidadGaleriaEstado estado = EntidadGaleriaEstado.ALMACEN;
        private EntidadGaleriaDireccion direccion = EntidadGaleriaDireccion.DESCONOCIDO;

        private Vector3    targetposicion = Vector3.zero;
        private Quaternion targetrotacion = Quaternion.identity;
        private float      targetposicionvelocidad = 0;
        private float      targetrotacionvelocidad = 0;

        private ManagerGaleria galeria  = null;
        private Animador       animador = null;
        private bool           control  = true;

        private void Start(){
            galeria = ManagerGaleria.GetInstancia();
            animador = GetComponentInChildren<Animador>();
        }
        private void Update(){

            switch (estado){
                case EntidadGaleriaEstado.ALMACEN:

                    targetposicion = almacen.GetPosicion();
                    targetposicionvelocidad = almacen.GetVelocidadLineal();

                    if (almacen.IsForzarRotacion())
                        targetrotacion = almacen.GetRotacion();
                    targetrotacionvelocidad = almacen.GetVelocidadAngular();
                    break;
                case EntidadGaleriaEstado.ENTRADA:
                    if (direccion == EntidadGaleriaDireccion.DERECHA)
                    {
                        if (derecha == null){
                            SetEstado(EntidadGaleriaEstado.EXHIBIDA, EntidadGaleriaDireccion.DESCONOCIDO);
                        }
                        else{
                            targetposicion = derecha.GetPosicion();
                            targetposicionvelocidad = derecha.GetVelocidadLineal();

                            if (derecha.IsForzarRotacion())
                                targetrotacion = derecha.GetRotacion();
                            targetrotacionvelocidad = derecha.GetVelocidadAngular();
                            if (derecha.IsPosicion(this))
                                SetEstado(EntidadGaleriaEstado.EXHIBIDA, EntidadGaleriaDireccion.DESCONOCIDO);
                        }
                    }
                    else  if (direccion == EntidadGaleriaDireccion.IZQUIERDA)
                    {
                        if (izquierda == null){
                            SetEstado(EntidadGaleriaEstado.EXHIBIDA, EntidadGaleriaDireccion.DESCONOCIDO);
                        }
                        else{
                            targetposicion = izquierda.GetPosicion();
                            targetposicionvelocidad = izquierda.GetVelocidadLineal();

                            if (izquierda.IsForzarRotacion())
                                targetrotacion = izquierda.GetRotacion();
                            targetrotacionvelocidad = izquierda.GetVelocidadAngular();
                            if (izquierda.IsPosicion(this))
                                SetEstado(EntidadGaleriaEstado.EXHIBIDA, EntidadGaleriaDireccion.DESCONOCIDO);
                        }
                    }
                        
                    break;
                case EntidadGaleriaEstado.SALIDA:
                    if (direccion == EntidadGaleriaDireccion.DERECHA) {
                        if (derecha == null){
                            SetEstado(EntidadGaleriaEstado.ALMACEN, EntidadGaleriaDireccion.DESCONOCIDO);
                        }
                        else{

                            targetposicion = derecha.GetPosicion();
                            targetposicionvelocidad = derecha.GetVelocidadLineal();

                            if (derecha.IsForzarRotacion())
                                targetrotacion = derecha.GetRotacion();
                            targetrotacionvelocidad = derecha.GetVelocidadAngular();
                            if (derecha.IsPosicion(this))
                                SetEstado(EntidadGaleriaEstado.ALMACEN, EntidadGaleriaDireccion.DESCONOCIDO);

                        }
                    }
                    else  if (direccion == EntidadGaleriaDireccion.IZQUIERDA)
                    {
                        if (izquierda == null){
                            SetEstado(EntidadGaleriaEstado.ALMACEN, EntidadGaleriaDireccion.DESCONOCIDO);
                        }
                        else{

                            targetposicion = izquierda.GetPosicion();
                            targetposicionvelocidad = izquierda.GetVelocidadLineal();

                            if (izquierda.IsForzarRotacion())
                                targetrotacion = izquierda.GetRotacion();
                            targetrotacionvelocidad = izquierda.GetVelocidadAngular();
                            if (izquierda.IsPosicion(this))
                                SetEstado(EntidadGaleriaEstado.ALMACEN, EntidadGaleriaDireccion.DESCONOCIDO);

                        }
                    }

                    break;
                case EntidadGaleriaEstado.EXHIBIDA:
                    targetposicion = galeria.GetExhibidor().GetPosicion();
                    targetposicionvelocidad = galeria.GetExhibidor().GetVelocidadLineal();

                    targetrotacion = galeria.GetExhibidor().GetRotacion();
                    targetrotacionvelocidad = galeria.GetExhibidor().GetVelocidadAngular();
                    break;
            }                    
            if (control){
                if (targetposicionvelocidad <= 0)
                    transform.position = targetposicion;
                else
                    transform.position = Vector3.MoveTowards(transform.position, targetposicion, targetposicionvelocidad * Time.deltaTime);
                if (targetrotacionvelocidad <= 0)
                    transform.rotation = targetrotacion;
                else 
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetrotacion, targetrotacionvelocidad * Time.deltaTime);
            }

        }

        private void ActivarAlmacen(){
            if (eventosalmacen == null)
                return;
            eventosalmacen.Invoke();
        }
        private void ActivarEntrada(){
            if (eventosentrada == null)
                return;
            eventosentrada.Invoke();
        }
        private void ActivarSalida(){
            if (eventossalida == null)
                return;
            eventossalida.Invoke();
        }
        private void ActivarExhibidor(){
            if (eventosexhibidor == null)
                return;
            eventosexhibidor.Invoke();
        }
            
        public void     SetEstado(EntidadGaleriaEstado estado,EntidadGaleriaDireccion direccion,bool forzar = false){
            if (this.estado == estado && !forzar)
                return;
            this.direccion = direccion;
            this.estado = estado;
            switch(estado){
                case EntidadGaleriaEstado.ALMACEN:
                    ActivarAlmacen();
                    break;
                case EntidadGaleriaEstado.ENTRADA:
                    ActivarEntrada();
                    break;
                case EntidadGaleriaEstado.SALIDA:
                    ActivarSalida();
                    break;
                case EntidadGaleriaEstado.EXHIBIDA:
                    ActivarExhibidor();
                    break;
            }
        }

        public Metadata GetMetadata(string nombre){
            for (int i = 0; i < data.Length; i++)
                if (data[i].IsNombre(nombre))
                    return data[i];
            return new Metadata();
        }            
        public Metadata GetMetadata(int index){
            return data[index];
        }
        public int      GetMetadataCount(){
            return data.Length;
        }

        public Animador GetAnimador(){
            return animador;
        }    
        public Vector3  GetPosicion(){
            return transform.position;
        }
      
        public void BotonSeleccionar(){
            if (estado != EntidadGaleriaEstado.EXHIBIDA)
                galeria.BotonSeleccionar(this);
        }
        public void AnimacionSetControl(bool control){
            this.control = control;
        }

    }

}

