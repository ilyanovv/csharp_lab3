using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DataBindingExample.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        Persons _persons = new Persons();

        ObservableCollection<PersonViewModel> _personViewModels = new ObservableCollection<PersonViewModel>();

        CollectionViewSource _cvs = new CollectionViewSource();

        CollectionViewSource _cvs_birthdays = new CollectionViewSource(); 

        public MainViewModel()
        {
            _persons = new Persons();
            _persons.Load();

            foreach (Person p in _persons)
            {
                _personViewModels.Add(new PersonViewModel(p));
            }

            _persons.CollectionChanged += _persons_CollectionChanged;

            _cvs.Source = _personViewModels;
            _cvs.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            _cvs.Filter += new FilterEventHandler(_cvs_Filter);

            _cvs.View.CurrentChanged += View_CurrentChanged;
            _cvs.View.CollectionChanged += View_CollectionChanged;

            _cvs_birthdays.Source = _personViewModels;
            _cvs_birthdays.Filter += new FilterEventHandler(_cvs_birthday_Filter); 

            AddCommand = new XCommand(Add);
            DeleteCommand = new XCommand(Delete);
            FilterCommand = new YCommand(FilterMethod);
            FilterBirthdayCommand = new XCommand(FilterBirthdayMethod);



        }


        void _persons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                _personViewModels.Add(new PersonViewModel((Person)e.NewItems[0]));
            }  
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var p = (Person) e.OldItems[0];

                var v = _personViewModels.FirstOrDefault(vm => vm.Person == p);
                _personViewModels.Remove(v);
            }
        }

        void View_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("PersonsCount");
        }

        void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedPerson");
            DeleteCommand.CanExecuteProperty = (SelectedPerson != null);
        }

        string _filter = "";

        public string Filter 
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                _cvs.View.Refresh();
            }
        }

        public Persons PersonsCollection
        {
            get
            {
                return _persons;
            }
        }

        bool _filter_birthday = false; 

        public bool FilterBirthday
        {
            get
            {
                return _filter_birthday;
            }
            set
            {
                _filter_birthday = value;
                _cvs.View.Refresh();
            }
        }

        void _cvs_Filter(object sender, FilterEventArgs e)
        {
            PersonViewModel p = (PersonViewModel)e.Item;
            if (p.Name != null)
                e.Accepted = p.Name.ToLower().StartsWith(Filter.ToLower());
            if (e.Accepted)
            {
                //if (p.Birthday != null)
                //{
                //    DateTime birthday = ((DateTime)p.Birthday);
                //    DateTime nextBirthday = new DateTime(DateTime.Now.Year, birthday.Month, birthday.Day);
                //    if (nextBirthday < DateTime.Now)
                //        nextBirthday = nextBirthday.AddYears(1);
                //    e.Accepted = FilterBirthday == false || nextBirthday <= DateTime.Now.AddDays(7 * 2);
                //}
                _cvs_birthday_Filter(sender, e); 
            }
        }

        //фильтр людей, у которых др не позднее, чем через две недели
        void _cvs_birthday_Filter(object sender, FilterEventArgs e)
        {
            PersonViewModel p = (PersonViewModel)e.Item;
            if (p.Birthday != null)
            {
                DateTime birthday = ((DateTime)p.Birthday);
                DateTime nextBirthday = new DateTime(DateTime.Now.Year, birthday.Month, birthday.Day);
                if (nextBirthday < DateTime.Now)
                    nextBirthday = nextBirthday.AddYears(1);
                e.Accepted =  FilterBirthday == false ||  nextBirthday <= DateTime.Now.AddDays(7 * 2);
            }
                
        }


        public ICollectionView Persons
        {
            get
            {
                return _cvs.View;
            }
        }

        public int PersonsCount
        {
            get
            {
                int count = 0;
                foreach (object obj in _cvs.View)
                {
                    count++;
                }
                return count;
            }
        }

        public PersonViewModel SelectedPerson
        {
            get
            {
                return (PersonViewModel)_cvs.View.CurrentItem;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void Add()
        {
            //WindowAddContact.ShowAddPerson(_persons.Add);

            var personViewModel = new PersonViewModel(new Person());
            if (WindowManager.ShowDialog(personViewModel))
            {
                _persons.Add(personViewModel.Person);
            }
        }

        void Delete()
        {
            if (SelectedPerson != null)
                _persons.Remove(SelectedPerson.Person);
        }

        private void FilterMethod(object buttonName)
        {
            string btnName = buttonName as string;
            Filter = btnName;
        }

        void FilterBirthdayMethod()
        {
            _cvs.Filter += _cvs_birthday_Filter; 
        }

        public XCommand AddCommand { get; set; }

        public XCommand DeleteCommand { get; set; }

        public YCommand FilterCommand { get; set; }

        public XCommand FilterBirthdayCommand { get; set; }
    }  

  
}
