using ConsumindoApiMarvel.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumindoApiMarvel.Controller
{
    class ControllerHeroi
    {
        public Heroi GetHeroiAPI(string nomeHeroi)
        {
            Heroi heroi = new Heroi();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Request
                    HttpResponseMessage response = client.GetAsync(string.Format("http://gateway.marvel.com/v1/public/characters?" + 
                        $"name={nomeHeroi}" + 
                        $"&ts={DateTime.Now.Ticks.ToString()}" + 
                        $"&apikey={"0ec8e93d8ef0fd9a59353772ee651d0f"}" + 
                        $"&hash={GerarHash(DateTime.Now.Ticks.ToString(), "0ec8e93d8ef0fd9a59353772ee651d0f", "45f01f9b175b57cf939f7927a29565a427e3b1bb")}")).Result;

                    //Deserealizando conteúdo
                    dynamic conteudoDeserialized = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    heroi = new Heroi
                    {
                        Nome = conteudoDeserialized.data.results[0].id,
                        Historia = conteudoDeserialized.data.results[0].description,
                        LinkFoto = conteudoDeserialized.data.results[0].thumbnail.path + "." + conteudoDeserialized.data.results[0].thumbnail.extension
                    };
                }
            }
            catch
            {
                MessageBox.Show("Heroi não encontrado");
            }

            return heroi;
        }

        private string GerarHash(string timeStamp, string chavePublica, string chavePrivada)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(timeStamp + chavePrivada + chavePublica);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).ToLower().Replace("-", String.Empty);
        }
    }
}
