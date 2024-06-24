using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace APICREADACONSUMIR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Usuario
        {
            public string correo { get; set; }
            public string clave { get; set; }
        }

        public class Respuesta
        {
            public string token { get; set; }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var cliente1 = new HttpClient();

            //Creacion de credenciales
            Usuario ob_usuario = new Usuario() { correo = "ssjeduardo49@gmail.com", clave = "sori19" };

            //Creacion de contenido
            var content = new StringContent(JsonConvert.SerializeObject(ob_usuario), Encoding.UTF8, "application/json");

            //Ejecucion de la API
            var response1 = await cliente1.PostAsync("http://localhost:5080/api/Autenticacion/Validar", content);

            //Respuesta: Token en formato json
            var json_respuesta1 = await response1.Content.ReadAsStringAsync();

            //Convertir el JSON a una clase
            var ob_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_respuesta1);

            //Nuevo cliente para consumir la api (Listar en este caso)
            var cliente2 = new HttpClient();

            //Esto es para declarar BearerToken y pasar el token 
            cliente2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ob_respuesta.token);

            //Consumir la api
            var response = await cliente2.GetAsync("http://localhost:5080/api/Producto/Lista");

            //Obtener la respuesta en formato texto
            var test = await response.Content.ReadAsStringAsync();
        }
    }
}
