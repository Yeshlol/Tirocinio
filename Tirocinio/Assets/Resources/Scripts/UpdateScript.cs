﻿using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;


// Questo script gestisce tramite Update() le modifiche da apportare all'interfaccia utente, utilizzando la variabile 'state' a cui è possibile assegnare uno dei possibili State.
// Di volta in volta, andrà a recuperare, tramite UnityWebRequest, il video o immagine da mostrare in Realtà Aumentata, e andrà a sostituire il prefab da associare all'immagine tracciata.
namespace UnityEngine.XR.Tirocinio
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class UpdateScript : MonoBehaviour
    {
        private GameObject originalPrefab;

        public GameObject videoPrefab;
        public GameObject imagePrefab;
        [SerializeField] private GoForwardScript goForwardScript;
        [SerializeField] private GoBackScript goBackScript;
        [HideInInspector] public Sprite currentSprite;
        [HideInInspector] public string currentUrl;

        public enum State
        {
            Idle,
            ReturnToIdle,
            SwapToFax1,
            Fax1,
            BackToFax1,
            SwapToFax2,
            Fax2,
            BackToFax2,
            SwapToFax3,            
            Fax3,
            BackToFax3,
            SwapToFax4,
            Fax4,
            SwapToScan1,            
            Scan1,
            BackToScan1,
            SwapToScan2,
            Scan2,
            BackToScan2,
            SwapToScan3,
            Scan3,
            BackToScan3,
            SwapToScan4,
            Scan4,
            BackToScan4,
            SwapToScan5,
            Scan5,
            SwapToCopy1,
            Copy1,
            BackToCopy1,
            SwapToCopy2,
            Copy2,
            BackToCopy2,
            SwapToCopy3,
            Copy3,
            BackToCopy3,
            SwapToCopy4,
            Copy4,
            SwapToToner1,
            Toner1,
            BackToToner1,
            SwapToToner2,
            Toner2,
            BackToToner2,
            SwapToToner3,
            Toner3,
            SwapToFogli1,
            Fogli1,
            BackToFogli1,
            SwapToFogli2,
            Fogli2,
            BackToFogli2,
            SwapToFogli3,
            Fogli3,
            SwapToCarta,
            Carta
        }

        public State state = State.Idle;

        public Text testo; //DEBUG

        public void Update()
        {
            var manager = GetComponent<PrefabImagePairManager>();
            var library = manager.imageLibrary;

            if (!originalPrefab)
                originalPrefab = manager.GetPrefabForReferenceImage(library[0]);

            switch (state)
            {
                // Zona Fax
                case State.SwapToFax1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Fax1;
                        break;
                    }
                case State.SwapToFax2:
                    {
                        ChangeToImage("PulsanteFax");
                        state = State.Fax2;
                        break;
                    }
                case State.SwapToFax3:
                    {
                        ChangeToVideo("AperturaCoperchio");
                        state = State.Fax3;
                        break;
                    }
                case State.SwapToFax4:
                    {
                        state = State.Fax4;
                        ChangeToImage("TastierinoNumerico");                        
                        break;
                    }

                case State.BackToFax3:
                    {
                        ChangeToVideo("AperturaCoperchio");
                        state = State.Fax3;
                        break;
                    }
                case State.BackToFax2:
                    {
                        ChangeToImage("PulsanteFax");
                        state = State.Fax2;
                        break;
                    }
                case State.BackToFax1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Fax1;
                        break;
                    }


                // Zona Scan
                case State.SwapToScan1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Scan1;
                        break;
                    }
                case State.SwapToScan2:
                    {
                        ChangeToVideo("InserimentoUSB");
                        state = State.Scan2;
                        break;
                    }
                case State.SwapToScan3:
                    {
                        ChangeToImage("PulsanteScan");
                        state = State.Scan3;
                        break;
                    }
                case State.SwapToScan4:
                    {
                        ChangeToVideo("AperturaCoperchio");
                        state = State.Scan4;
                        break;
                    }
                case State.SwapToScan5:
                    {
                        ChangeToImage("Pulsanti");
                        state = State.Scan5;
                        break;
                    }

                case State.BackToScan4:
                    {
                        ChangeToVideo("AperturaCoperchio");
                        state = State.Scan4;
                        break;
                    }
                case State.BackToScan3:
                    {
                        ChangeToImage("PulsanteScan");
                        state = State.Scan3;
                        break;
                    }
                case State.BackToScan2:
                    {
                        ChangeToVideo("InserimentoUSB");
                        state = State.Scan2;
                        break;
                    }
                case State.BackToScan1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Scan1;
                        break;
                    }


                // Zona Copy
                case State.SwapToCopy1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Copy1;
                        break;
                    }
                case State.SwapToCopy2:
                    {
                        ChangeToImage("PulsanteCopia");
                        state = State.Copy2;
                        break;
                    }
                case State.SwapToCopy3:
                    {
                        ChangeToVideo("AperturaCoperchio");
                        state = State.Copy3;
                        break;
                    }
                case State.SwapToCopy4:
                    {
                        ChangeToImage("TastierinoNumerico");
                        state = State.Copy4;
                        break;
                    }

                case State.BackToCopy3:
                    {
                        ChangeToVideo("AperturaCoperchio");
                        state = State.Copy3;
                        break;
                    }
                case State.BackToCopy2:
                    {
                        ChangeToImage("PulsanteCopia");
                        state = State.Copy2;
                        break;
                    }
                case State.BackToCopy1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Copy1;
                        break;
                    }


                // Zona Toner
                case State.SwapToToner1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Toner1;
                        break;
                    }
                case State.SwapToToner2:
                    {
                        ChangeToVideo("AperturaSportello");
                        state = State.Toner2;
                        break;
                    }
                case State.SwapToToner3:
                    {
                        state = State.Toner3;
                        ChangeToVideo("ChiusuraSportello");                        
                        break;
                    }
                case State.BackToToner2:
                    {
                        ChangeToVideo("AperturaSportello");
                        state = State.Toner2;
                        break;
                    }
                case State.BackToToner1:
                    {
                        ChangeToVideo("Accensione");
                        state = State.Toner1;
                        break;
                    }


                // Zona Fogli
                case State.SwapToFogli1:
                    {
                        ChangeToVideo("AperturaCarrello");
                        state = State.Fogli1;
                        break;
                    }
                case State.SwapToFogli2:
                    {
                        ChangeToVideo("InserimentoCarta");
                        state = State.Fogli2;
                        break;
                    }
                case State.SwapToFogli3:
                    {
                        state = State.Fogli3;
                        ChangeToVideo("ChiusuraCarrello");                        
                        break;
                    }
                case State.BackToFogli2:
                    {
                        ChangeToVideo("InserimentoCarta");
                        state = State.Fogli2;
                        break;
                    }
                case State.BackToFogli1:
                    {
                        ChangeToVideo("AperturaCarrello");
                        state = State.Fogli1;
                        break;
                    }


                // Zona Carta
                case State.SwapToCarta:
                    {
                        state = State.Carta;
                        ChangeToVideo("EstrazioneCarta");                        
                        break;
                    }

                case State.ReturnToIdle:
                    {
                        manager.SetPrefabForReferenceImage(library[0], originalPrefab);

                        goForwardScript.SetForwardActive();

                        state = State.Idle;
                        break;
                    }
            }
        }
               


        private IEnumerator SetImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                testo.text += "/n" + request.error;
            }
            else
            {
                Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, 4000, 3000), new Vector2(tex.width / 2, tex.height / 2));
                currentSprite = sprite;
                imagePrefab.GetComponentInChildren<Image>().sprite = sprite;

                var manager = GetComponent<PrefabImagePairManager>();
                var library = manager.imageLibrary;

                manager.SetPrefabForReferenceImage(library[0], imagePrefab);
            }

            if (state != State.Fax4 && state != State.Scan5 && state != State.Copy4)
            {
                goForwardScript.SetForwardActive();
                goBackScript.SetBackActive();
            }
        }


        private void ChangeToImage(string imageName)
        {
            goForwardScript.SetForwardInactive();
            goBackScript.SetBackInactive();

            string url = "http://93.41.140.68:8000/api/image/";

            switch (imageName)
            {
                case "PulsanteCopia":
                    url += "1";
                    break;
                case "PulsanteFax":
                    url += "2";
                    break;
                case "PulsanteScan":
                    url += "3";
                    break;
                case "Pulsanti":
                    url += "4";
                    break;
                case "TastierinoNumerico":
                    url += "5";
                    break;
            }

            StartCoroutine(SetImage(url));
        }

        private void ChangeToVideo(string videoName)
        {
            goForwardScript.SetForwardInactive();
            goBackScript.SetBackInactive();

            string url = "http://93.41.140.68:8000/api/video/";

            switch (videoName)
            {
                case "Accensione":
                    url += "1";
                    break;
                case "AperturaCarrello":
                    url += "2";
                    break;
                case "AperturaCoperchio":
                    url += "3";
                    break;
                case "AperturaSportello":
                    url += "4";
                    break;
                case "ChiusuraCarrello":
                    url += "5";
                    break;
                case "ChiusuraSportello":
                    url += "6";
                    break;
                case "EstrazioneCarta":
                    url += "7";
                    break;
                case "InserimentoCarta":
                    url += "8";
                    break;
                case "InserimentoUSB":
                    url += "9";
                    break;
            }

            VideoPlayer videoPlayer = videoPrefab.GetComponentInChildren<VideoPlayer>();
            videoPlayer.url = url;
            currentUrl = url;

            var manager = GetComponent<PrefabImagePairManager>();
            var library = manager.imageLibrary;

            manager.SetPrefabForReferenceImage(library[0], videoPrefab);

            if (state != State.Fogli3 && state != State.Toner3 && state != State.Carta)
            {
                goForwardScript.SetForwardActive();                
            }
            goBackScript.SetBackActive();
        }
    }    
}
