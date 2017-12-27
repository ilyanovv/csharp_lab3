using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace DataBindingExample
{
    public class Person
    {
        public BitmapImage Avatar { get; set; }

        public string Name { get; set;}

        public DateTime? Birthday { get; set; }
        public bool Male { get; set; }
        public string HomeNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }

        public BitmapImage GetUnknownImage()
        {
            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            if(Male == true)
                bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/unknown.jpg");
            else
                bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/unknown2.png");
            bi.EndInit();
            return bi;
        }
    }
}
