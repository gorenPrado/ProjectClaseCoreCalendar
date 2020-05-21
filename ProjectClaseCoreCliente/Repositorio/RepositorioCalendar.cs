using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectClaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClaseCoreCliente.Repositorio
{
    public class RepositorioCalendar
    {
        private String url;
        private MediaTypeWithQualityHeaderValue header;
        public RepositorioCalendar()
        {
            url = "https://projectclasecoregpp.azurewebsites.net/";
            header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        public async Task<String> GetToken(String username, String password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                Login login = new Login();
                login.Username = username;
                login.Password = password;
                String json = JsonConvert.SerializeObject(login);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                String request = "api/Auth/Login";
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    String data = await response.Content.ReadAsStringAsync();
                    JObject jobject = JObject.Parse(data);
                    String token = jobject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<T> GetApi<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> GetApiToken<T>(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> InsertApi<T>(String request, T body, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(body);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    T data = await
                        response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> ModificarApi<T>(String request, T body, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(body);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    T data = await
                        response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> DeleteApi<T>(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.DeleteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<Eventos>> GetEventos()
        {
            List<Eventos> evento = await GetApi<List<Eventos>>("api/Project/GetEventos");
            return evento;
        }
        public async Task<Eventos> BuscarEvento(int idevento,String token)
        {
            Eventos evento = await GetApiToken<Eventos>("api/Project/BuscarEvento/" + idevento,token);
            return evento;
        }
        public async Task<Usuarios> Perfil(String token)
        {
            Usuarios usuario = await GetApiToken<Usuarios>("/api/Project/Perfil", token);
            return usuario;
        }
        public async Task<List<Eventos>> EventoUsuario(String token)
        {
            List<Eventos> evento = await GetApiToken<List<Eventos>>("api/Project/EventoUsuario", token);
            return evento;
        }
        public async Task Insertar(String token, Eventos evento)
        {
            await InsertApi<Eventos>("api/Project/CrearEvento", evento, token);
        }
        public async Task Delete(String token, int idevento)
        {
            await DeleteApi<Eventos>("api/Project/EliminarEvento/" + idevento, token);
        }
        public async Task Modificar(String token, Eventos evento)
        {
            await ModificarApi<Eventos>("/api/Project/ModificarEvento", evento, token);
        }
        public async Task InsertarUsuario(String token, Usuarios usuario)
        {
            await InsertApi<Usuarios>("api/Project/CrearEvento", usuario, token);
        }
        public async Task<List<Eventos>> EventosCalendar(String token)
        {
            List<Eventos> evento = await GetApiToken<List<Eventos>>("api/Project/EventosCalendar", token);
            return evento;
        }
    }
}
