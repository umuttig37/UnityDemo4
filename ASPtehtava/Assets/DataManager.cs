using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;         // Tietoliikennettä varten
using TMPro;


namespace aspnetharjoitus
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField]
      private TMP_InputField outputArea;



        public void GetData()
        {
            outputArea.text = null;
            string uri = "https://localhost:7103/TestiTehtavat";
            Stat stat = new Stat();
            StartCoroutine(stat.LoadStaDataFromDatabase(uri, outputArea));
        }

        public void PutData()
        {
            outputArea.text = "Loading...";
            string uri = "https://localhost:7103/TestiTehtavat/1";

            //Luodaan Stat-olio
            Stat stat = new Stat(Player.instance.Id, Player.instance.TehtavanNimi, Player.instance.TehtavanKuvaus, Player.instance.PalkkionMaara,
                Player.instance.KokemusPisteet, Player.instance.OnkoSuoritettu);

            //Suoritetaan päivitys
            StartCoroutine(stat.SaveStatDataToDatabase(uri, outputArea));
        }
    }
}
