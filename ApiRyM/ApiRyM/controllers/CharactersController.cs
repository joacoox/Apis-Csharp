using ApiRyM.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRyM.controllers
{
    public class CharactersController
    {
        private HttpClient _client;

        public CharactersController()
        {
        _client = new HttpClient();
        }

        public async Task<Characters> GetAllCharacters()
        {
            try
            {
                Characters characters = new Characters();
                HttpResponseMessage response = await
                _client.GetAsync("https://rickandmortyapi.com/api/character");
                response.EnsureSuccessStatusCode();

                string responseJson = await 
                response.Content.ReadAsStringAsync();

                characters = JsonConvert.DeserializeObject<Characters>(responseJson);

                return characters;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
