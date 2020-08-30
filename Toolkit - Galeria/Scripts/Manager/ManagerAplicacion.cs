using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Galeria{


    [System.Serializable]
    public struct AudioPerfil{
        [SerializeField]
        public Transform carpeta;
        [SerializeField]
        public string nombre;
        [SerializeField]
        public Audio  prefab;
    }
    public class ManagerAplicacion : MonoBehaviour{

        [Header("Audio")]
        [SerializeField]
        private AudioPerfil []perfiles = null;
        [Header("Eventos")]
        private UnityEvent []eventoinicioaplicacion = null;

        public static ManagerAplicacion instanciabase;

        private void Awake(){

            instanciabase = this;

        }
        private void Start(){
            ActivarInicioAplicacion();
        }

        private void ActivarInicioAplicacion(){
            if(eventoinicioaplicacion!=null)
                for (int i = 0; i < eventoinicioaplicacion.Length; i++)
                    eventoinicioaplicacion[i].Invoke();
        }

        public Audio PlayAudio(string perfil,AudioClip clip, Vector3 posicion){            
            for (int i = 0; i < perfiles.Length; i++)
                if (perfil == perfiles[i].nombre)
                    return PlayAudio(i, clip, posicion);
            return null;
        }
        public Audio PlayAudio(int perfil,AudioClip clip, Vector3 posicion){
            Audio audio = perfiles[perfil].prefab;
            audio.Create(clip,perfiles[perfil].carpeta,posicion);
            return audio;
        }

        public static ManagerAplicacion GetInstanciaBase(){
            if (instanciabase == null)
                instanciabase = GameObject.FindObjectOfType<ManagerAplicacion>();
            return instanciabase;
        }

    }


}