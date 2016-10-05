using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;


namespace WeatherWebClient
{
    public partial class Form1 : Form
    {
        

        public class Rootobject
        {
            public City city { get; set; }
            public string cod { get; set; }
            public float message { get; set; }
            public int cnt { get; set; }
            public List[] list { get; set; }
        }

        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coord coord { get; set; }
            public string country { get; set; }
            public int population { get; set; }
        }

        public class Coord
        {
            public float lon { get; set; }
            public float lat { get; set; }
        }

        public class List
        {
            public int dt { get; set; }
            public Temp temp { get; set; }
            public float pressure { get; set; }
            public int humidity { get; set; }
            public Weather[] weather { get; set; }
            public float speed { get; set; }
            public int deg { get; set; }
            public int clouds { get; set; }
            public float rain { get; set; }
        }

        public class Temp
        {
            public float day { get; set; }
            public float min { get; set; }
            public float max { get; set; }
            public float night { get; set; }
            public float eve { get; set; }
            public float morn { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public static DateTime Today { get; }

        public Form1()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x = textBox1.Text;
            string k = "09220a3eaa7b1b278c4be7d01aaf0e82";
            string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q="+x+"&APPID="+k;
            using (WebClient wc = new WebClient())
            {

                string json = wc.DownloadString(url);

                Rootobject obj = JsonConvert.DeserializeObject<Rootobject>(json);
                string cityName = obj.city.name.ToString();
                string maxTempToday = obj.list[0].temp.max.ToString();
                string minTempToday = obj.list[0].temp.min.ToString();
                string maxTempTomorrow = obj.list[1].temp.max.ToString();
                string minTempTomorrow = obj.list[1].temp.min.ToString();
                DateTime today = DateTime.Today;
                string todayStr = today.ToString("D");
                string tomorrowStr = today.AddDays(1).ToString("D");
                string ic = string.Format("http://openweathermap.org/img/w/{0}.png", obj.list[0].weather[0].icon);
                string ic2 = string.Format("http://openweathermap.org/img/w/{0}.png", obj.list[1].weather[0].icon);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = ic;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.ImageLocation = ic2;
                richTextBox1.Text = string.Format("WEATHER {0}\n\n{5}\nMin: {1} F\nMax: {2} F\n\n\n{6}\nMin: {3} F\nMax: {4} F", cityName, minTempToday, maxTempToday, minTempTomorrow, maxTempTomorrow, todayStr, tomorrowStr);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        bool hasBeenClicked = false;

    }
}
