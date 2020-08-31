using UnityEngine;
using System.Collections;

namespace Galeria{

    public class ManagerGaleria : MonoBehaviour{

        [Header("Configuracion")]
        [SerializeField]
        private int seleccioninicial = 0;
        [Header("Entidades")]
        [SerializeField]
        private EntidadGaleria[] entidades = null;
        [Header("Escena")]
        [SerializeField]
        private Exhibidor exhibidor = null;

        private static ManagerGaleria instancia = null;

        private int seleccion = 0;

        private void Awake(){
            instancia = this;
        }
        private void Start(){
            SetSeleccion(seleccioninicial,EntidadGaleriaDireccion.DERECHA);
        }

        public void SetSeleccion(int index,EntidadGaleriaDireccion entrada){
            if (index >= entidades.Length || index < 0)
                return;
            seleccion = index;
            exhibidor.SetEntidadGaleria(entidades[seleccion],entrada);
        }
        public void SetSeleccion(EntidadGaleria entidad,EntidadGaleriaDireccion entrada){
            for (int i = 0; i < entidades.Length; i++)
                if (entidades[i] == entidad)
                    SetSeleccion(i,entrada);
        }

        public void Siguiente(){
            if ((seleccion + 1) >= entidades.Length)
                SetSeleccion(0,EntidadGaleriaDireccion.DERECHA);
            else
                SetSeleccion(seleccion + 1,EntidadGaleriaDireccion.DERECHA);
        }
        public void Anterior(){
            if ((seleccion - 1) < 0)
                SetSeleccion(entidades.Length-1,EntidadGaleriaDireccion.IZQUIERDA);
            else
                SetSeleccion(seleccion - 1,EntidadGaleriaDireccion.IZQUIERDA);
        }

        public Exhibidor GetExhibidor(){
            return exhibidor;
        }

        public void BotonSiguiente(){
            Siguiente();
        }
        public void BotonAnterior(){
            Anterior();
        }

        public void BotonDeseleccionar(){
            exhibidor.SetEntidadGaleria(null, exhibidor.GetEntrada());
        }
        public void BotonSeleccionar(EntidadGaleria entidad){
            SetSeleccion(entidad, exhibidor.GetEntrada());
        }

        public static ManagerGaleria GetInstancia(){
            if (instancia == null)
                instancia = GameObject.FindObjectOfType<ManagerGaleria>();
            return instancia;
        }

    }

}