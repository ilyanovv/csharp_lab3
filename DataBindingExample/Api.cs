using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using DataBindingExample.ViewModel;

namespace DataBindingExample
{
    public class Api
    {
        private Persons personsCollection;

        public Api(Persons personsCollection)
        {
            this.personsCollection = personsCollection;
        }


        public IObservable<ObservableCollection<PersonViewModel>> Search(string filter)
        {
            filter = filter.Trim();
            IEnumerable<Person> filteredPersons = personsCollection.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            ObservableCollection<PersonViewModel> personsToShow = new ObservableCollection<PersonViewModel>();
            foreach (Person person in filteredPersons)
                personsToShow.Add(new PersonViewModel(person));
            return Observable.Return(personsToShow);
        }
    }
}
