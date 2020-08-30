using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Galeria{

    [System.Serializable]
    public struct Entrada{
        [SerializeField]
        private string nombre;
        [SerializeField]
        private Tecla tecla;
        [SerializeField]
        private UnityEvent []eventos;

        public string GetNombre(){
            return nombre;
        }
        public Tecla GetTecla(){
            return tecla;
        }
        public void ActivarEvento(){

            if (eventos == null)
                return;
            for (int i = 0; i < eventos.Length; i++)
                eventos[i].Invoke();

        }

    }

    public class ManagerEntrada : MonoBehaviour{

        [SerializeField]
        private Entrada []entrada = null;

        private void Update(){

            if (entrada == null)
                return;
            for (int i = 0; i < entrada.Length; i++)
                if (entrada[i].GetTecla().IsClickDown())
                    entrada[i].ActivarEvento();

        }
            
    }

}