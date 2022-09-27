using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace aspnetharjoitus
{
    public class Player : MonoBehaviour
    {
        public static Player instance;

        //Pelihahmon tilatiedot (Stats)
        [SerializeField] private int id;
        [SerializeField] private string tehtavanNimi;
        [SerializeField] private string tehtavanKuvaus;
        [SerializeField] private int palkkionMaara;
        [SerializeField] private int kokemusPisteet;
        [SerializeField] private bool onkoSuoritettu;

        public int Id { get => id; set => id = value; }
        public string TehtavanNimi { get => tehtavanNimi; set => tehtavanNimi = value; }
        public string TehtavanKuvaus { get => tehtavanKuvaus; set => tehtavanKuvaus = value; }
        public int PalkkionMaara { get => palkkionMaara; set => palkkionMaara = value; }
        public int KokemusPisteet { get => kokemusPisteet; set => kokemusPisteet = value; }
        public bool OnkoSuoritettu { get => onkoSuoritettu; set => onkoSuoritettu = value; }

        private void Awake()
        {
            instance = this;
        }

    }
}
