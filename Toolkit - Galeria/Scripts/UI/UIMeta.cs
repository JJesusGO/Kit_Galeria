using UnityEngine;
using System.Collections;
using TMPro;

namespace Galeria{

    public class UIMeta : MonoBehaviour{

        [Header("Configuración")]
        [SerializeField]
        private string nombre = "";
        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI uinombre = null;
        [SerializeField]
        private TextMeshProUGUI uicontenido = null;

        private EntidadGaleria uientidad = null;
        private ManagerGaleria galeria = null;

        private void Start(){
            galeria = ManagerGaleria.GetInstancia();
        }
       
        private void Update(){

            if (galeria.GetExhibidor().GetEntidadGaleria() != uientidad){
                
                uientidad = galeria.GetExhibidor().GetEntidadGaleria();

                if (uientidad != null){
                    Metadata data = uientidad.GetMetadata(nombre);
                    if (data.IsNombre(nombre)){
                        SetUINombre(data.GetNombre());
                        SetUIContenido(data.GetContenido());
                    }
                }
                else{
                    SetUINombre("");
                    SetUIContenido("");
                }


            }

        }

        private void SetUINombre(string nombre){
            if (uinombre != null)
                uinombre.text = nombre;
        }
        private void SetUIContenido(string contenido){
            if (uicontenido != null)
                uicontenido.text = contenido;
        }

    }

}

