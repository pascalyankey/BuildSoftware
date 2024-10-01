using Client.Interfaces;
using Client.Models;
using Domain.Enums;
using Domain.Models;
using Domain.Responses;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Text;

namespace Client.Services
{
    public class ClientService : IClientService
    {
        Uri baseAddress = new Uri("http://localhost:40498/api");
        HttpClient _client = new HttpClient();

        public ClientService()
        {
            Uri baseAddress = new Uri("http://localhost:40498/api");
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public async Task<List<WerfViewModel>> GetWervenAsync()
        {
            var werven = new List<WerfViewModel>();
            var response = await _client.GetAsync(_client.BaseAddress + "/werf/GetWerven");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                werven = JsonConvert.DeserializeObject<List<WerfViewModel>>(data);
            }

            return werven;
        }

        public async Task<List<WerknemerViewModel>> GetWerknemersByWerfNaam(string werfNaam)
        {
            var werknemers = new List<WerknemerViewModel>();
            var response = await _client.GetAsync(_client.BaseAddress + "/werknemer/GetWerknemersByWerfNaam/" + werfNaam);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                werknemers = JsonConvert.DeserializeObject<List<WerknemerViewModel>>(data);
            }

            return werknemers;
        }

        public async Task<Dictionary<int, WerfWerknemerInsertedResponse>> WerknemerToevoegen(WerknemerViewModel werknemer)
        {
            var werknemers = new List<WerknemerViewModel>() { werknemer };
            var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/werknemer/Post/", werknemers);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsAsync<Dictionary<int, WerfWerknemerInsertedResponse>>();
                return responseData;
            }

            return null!;
        }

        public async Task<Unit> DeleteWerknemer(int id)
        {
            var response = await _client.DeleteAsync(_client.BaseAddress + "/werknemer/Delete/" + id);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<Unit>().Result;

                return data;
            }

            return Unit.Value;
        }

        public async Task<Dictionary<int, WerfWerknemerInsertedResponse>> WerfToevoegen(WerfViewModel werf)
        {
            var werknemers = new List<WerknemerViewModel>() { new WerknemerViewModel { Voornaam = werf.Voornaam, Achternaam = werf.Achternaam, Emailadres = werf.Emailadres, Wachtwoord = werf.Wachtwoord, RolId = 3 } };
            werf.Werknemers = werknemers;
            var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Werf/Post", werf);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsAsync<Dictionary<int, WerfWerknemerInsertedResponse>>();
                return responseData;
            }

            return null!;
        }
    }
}
