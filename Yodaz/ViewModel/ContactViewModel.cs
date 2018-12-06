using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Realms.Sync;
using System.Net.Http;
using Acr.UserDialogs;


using User = Yodaz.Model.User;

using Credentials = Realms.Sync.Credentials;

namespace Yodaz.ViewModel
{
    public class ContactViewModel : INotifyPropertyChanged
    {
        private User _cont;
        public IEnumerable<User> Contacts { get; private set; }

        private Realm _realm;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddContactCommand {get; private set;}

        public ContactViewModel()
        {
            AddContactCommand = new Command(() => AddContact().IgnoreResult());
            Initialize().IgnoreResult();
        }

        //Visar upp kontakterna i listan
        private async Task Initialize() 
        {
            _realm = await OpenRealm();
            // _user = _realm.All<User>().Where(u => u.UserId).First();
            //Contacts = _user.Contacts;
            //Contacts = _realm.All<User>().OrderBy(u => u.Username);

            //Hur hämtar vi alla kontakter från current user?
            Contacts = _realm.All<User>();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacts)));
        }

        private async Task AddContact()
        {
            var result = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                Title = "New contact",
                Message = "Write email correctly buddster",
            });

           // var n = _realm.All<User>.Where(cont => cont.email == result.Value);

            if (result.Ok)
            {
                _realm = await OpenRealm();
                User cont = _realm.All<User>().Where(c => c.Email == result.Value).First();
                //Måste hitta användaren vi skriver in
                _realm.Write(() =>
                {
                    _realm.Add(new User
                    {
                        //Fyll i från steg 11 
                        Email = "em@hej.com",
                        Username = result.Value,
                    });
                });
            }
        }

        //Är denna funktionen ens rätt
        private async Task<Realm> OpenRealm()
        {
            var user = Realms.Sync.User.Current;
            if (user != null)
            {
                var configuration = new FullSyncConfiguration(new Uri(Constants.RealmPath, UriKind.Relative), user);
                return Realm.GetInstance(configuration);
            }

            try 
            {
                var configuration = new FullSyncConfiguration(new Uri(Constants.RealmPath, UriKind.Relative), user);
                var realm = await Realm.GetInstanceAsync(configuration);
                return realm;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(new AlertConfig
                {
                    Title = "An error has occurred",
                    Message = $"An error occurred while trying to open realm: {ex.Message}"
                });

                return await OpenRealm();
            }
        }
    }
}
