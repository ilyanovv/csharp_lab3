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
    }
}
