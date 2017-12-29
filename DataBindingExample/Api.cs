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
            IEnumerable<Person> filteredPersons = personsCollection.Where(x => 
                            (x.Name != null && x.Name.ToLower().Contains(filter.ToLower()))
                        || (x.Comment != null && x.Comment.ToLower().Contains(filter.ToLower()))
                        || (x.Skype != null && x.Skype.ToLower().Contains(filter.ToLower()))
                        || (x.Email != null && x.Email.ToLower().Contains(filter.ToLower()))
                        || (x.WorkNumber != null && x.WorkNumber.ToLower().Contains(filter.ToLower()))
                        || (x.HomeNumber != null && x.HomeNumber.ToLower().Contains(filter.ToLower()))
                     );
            ObservableCollection<PersonViewModel> personsToShow = new ObservableCollection<PersonViewModel>();
            foreach (Person person in filteredPersons)
                personsToShow.Add(new PersonViewModel(person));
            return Observable.Return(personsToShow);
        }
    }
}
