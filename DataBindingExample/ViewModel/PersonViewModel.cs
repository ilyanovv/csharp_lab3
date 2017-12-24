﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace DataBindingExample.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        Person _person;

        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }

        public PersonViewModel(Person person)
        {
            _person = person;

           UpdateCommand = new DelegateCommand(ChangeName, CanChangeName);

        }
        
        public string Name
        {
            get => _person.Name;
            set
            {
                _person.Name = value;
                // RaisePropertyChanged("Name");
                OnPropertyChanged();
                // еще вариант пост-обработка, например Fody.Weavers
            }
        }

        //void RaisePropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

        // public string Birthday => _person.Birthday.ToString("dd.MM.yyyy");

        public DateTime? Birthday
        {
            get => _person.Birthday;
            set
            {
                _person.Birthday = value;
                OnPropertyChanged();
            }
        }

        public bool Male
        {
            get
            {
                return _person.Male;
            }
            set
            {
                _person.Male = value;
                OnPropertyChanged();
                // OnPropertyChanged("BackgroundColor");
            }
        }

        //public Brush BackgroundColor => _person.Male ? 
        //    Brushes.CornflowerBlue :
        //    Brushes.Pink;

        public DelegateCommand UpdateCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChangeName(object name)
        {
            Name = name as string;
        }

        public bool CanChangeName(object name)
        {
            return !string.IsNullOrWhiteSpace(name as string);
        }
    }
}
