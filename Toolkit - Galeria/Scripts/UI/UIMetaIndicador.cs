using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Galeria{

    public enum UIMetaIndicadorTipo{
        RELLENO,SELECCION
    }
    [System.Serializable]
    public class UIIndicador{
        [SerializeField]
        private Sprite []sprites = null;

        public Sprite GetSprite(int index){
            return sprites[index];
        }

        public Sprite GetSpriteRelleno(int index,float valor){

            valor -= index;                 

            if (sprites.Length == 0)
                return null;
            if (sprites.Length == 1){
                if (valor > 0)
                    return sprites[0];
                else
                    return null;
            }

            if (valor >= 1)
                return sprites[sprites.Length-1];
            if (valor <= 0)
                return sprites[0];
            
            int n = sprites.Length - 1;

            float secciones = 1.0f / n;
            float sumatoria = secciones;

            for (int i = 0; i < n; i++){
                if (valor < sumatoria)
                    return sprites[i + 1];
                sumatoria += valor;
            }

            return null;

        }
        public Sprite GetSpriteIndicador(int index,float valor){
            bool estado =  (index+1) == ((int)valor);

            if (sprites.Length == 0)
                return null;
            if (sprites.Length == 1){
                if (estado)
                    return sprites[0];
                return null;            
            }

            if (estado)
                return sprites[1];

            return sprites[0];

        }

        public int    GetSpriteCount(){
            return sprites.Length;
        }
    
    }
    public class UIMetaIndicador : MonoBehaviour{
        
        [Header("Configuración")]
        [SerializeField]
        private string nombre = "";
        [SerializeField]
        private UIMetaIndicadorTipo tipo = UIMetaIndicadorTipo.RELLENO;
        [SerializeField]
        private UIIndicador    indicador = new UIIndicador();
        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI uinombre = null;
        [SerializeField]
        private Image []uiimagenes = null;   

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
            try{
                
                float valor = float.Parse(contenido.Trim());  

                for(int i=0;i<uiimagenes.Length;i++)
                    switch(tipo){
                        case UIMetaIndicadorTipo.SELECCION:
                            uiimagenes[i].sprite = indicador.GetSpriteIndicador(i,valor);
                            break;
                        case UIMetaIndicadorTipo.RELLENO:
                            uiimagenes[i].sprite = indicador.GetSpriteRelleno(i,valor);
                            break;
                    }


            }
            catch(Exception){
                
            }
        }

    }


}
