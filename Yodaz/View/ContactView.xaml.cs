using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yodaz.ViewModel;
using Yodaz.Model;

namespace Yodaz.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactView : ContentPage
    {
        public ContactView()
        {
            InitializeComponent();
            BindingContext = new ContactViewModel();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e) 
        {
            ListView contacts = (ListView)sender;
            User user = (User)contacts.SelectedItem;
            //Pusha till chatsida när en kontakt blir vald
            //Navigation.PushAsync(new ChatViewModel(user));
        }
    }
}
