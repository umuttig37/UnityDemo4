using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

namespace aspnetharjoitus
{
    [System.Serializable]
    public class Stat
    {
        public int id { get; set; }
        public string tehtavanNimi { get; set; }
        public string tehtavanKuvaus { get; set; }
        public int palkkionMaara { get; set; }
        public int kokemusPisteet { get; set; }
        public bool onkoSuoritettu { get; set; }


        public Stat() { }

        public Stat(int id, string tehtavanNimi, string tehtavanKuvaus, int palkkionmaara, int kokemusPisteet, bool onkoSuoritettu)
        {
            this.id = id;
            this.tehtavanNimi = tehtavanNimi;
            this.tehtavanKuvaus = tehtavanKuvaus;
            this.palkkionMaara = palkkionMaara;
            this.kokemusPisteet = kokemusPisteet;
            this.onkoSuoritettu = onkoSuoritettu;
        }

        //Haetaan pelihahmon tilatiedot tietokannasta (JSON)
        public IEnumerator LoadStaDataFromDatabase(string uri, TMP_InputField outputArea)
        {
            using(UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                //Odotetaan vastausta
                yield return request.SendWebRequest();
                //tutkitaan onko virheit‰ 
                if (request.error != null)
                {
                    outputArea.text = $"Nettivirhe: {request.error}";
                }
                else
                {
                    outputArea.text = request.downloadHandler.text;
                }

                //otetaan JSON tietokannasta ja poistetaan [ ja ] merkit
                string newOutputArea = outputArea.text.Remove(0, 1);
                string newOutputArea2 = newOutputArea.Remove(newOutputArea.Length - 1, 1);


                Stat stat = JsonUtility.FromJson<Stat>(newOutputArea2);

                //p‰ivitet‰‰n pelihahmon tilatiedot
                Player.instance.Id = stat.id;
                Player.instance.TehtavanNimi = stat.tehtavanNimi;
                Player.instance.TehtavanKuvaus = stat.tehtavanKuvaus;
                Player.instance.PalkkionMaara = stat.palkkionMaara;
                Player.instance.KokemusPisteet = stat.kokemusPisteet;
                Player.instance.OnkoSuoritettu = stat.onkoSuoritettu;
               
            }
        }


        //Tallennetaan pelihahmon tilatiedot tietokantaan (JSON)
        public IEnumerator SaveStatDataToDatabase(string uri, TMP_InputField outputArea)
        {
            //Luodaan tallennusta JSON rakenne
            string id = $"\"id\":{this.id},";
            string tehtavanNimi = $"\"tehtavanNimi\":{this.tehtavanNimi},";
            string tehtavanKuvaus = $"\"tehtavanKuvaus\":{this.tehtavanKuvaus}";
            string palkkionMaara = $"\"palkkionMaara\":{this.palkkionMaara}";
            string kokemusPisteet = $"\"kokemusPisteet\":{this.kokemusPisteet}";
            string onkoSuoritettu = $"\"onkoSuoritettu\":{this.onkoSuoritettu}";
            string bodyData = "{" + id + tehtavanNimi + tehtavanKuvaus + palkkionMaara + kokemusPisteet + onkoSuoritettu + "}";

            //pyydet‰‰n palvelinta p‰ivitt‰m‰‰n (PUT) tilatiedot
            using (UnityWebRequest request = UnityWebRequest.Put(uri, bodyData))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.SendWebRequest();
                if(request.error != null)
                {
                    outputArea.text = $"Nettivirhe : {request.error}";
                    outputArea.text = "aaaaa"; //virhe on t‰‰ll‰
                }
                else
                {
                    outputArea.text = request.downloadHandler.text;
                }
            }
        }

    }
}
