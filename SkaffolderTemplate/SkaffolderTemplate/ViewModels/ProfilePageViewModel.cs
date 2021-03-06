﻿using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetValue(ref _username, value);
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetValue(ref _name, value);
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                SetValue(ref _surname, value);
            }
        }

        private string _mail;
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                SetValue(ref _mail, value);
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                SetValue(ref _user, value);
            }
        }
        #endregion

        #region Commands
        public ICommand SaveDataCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        #endregion

        public ProfilePageViewModel(User userLogged)
        {
            User = userLogged;

            Username = userLogged.Username;
            Name = userLogged.Name;
            Surname = userLogged.Surname;
            Mail = userLogged.Mail;

            SaveDataCommand = new Command(async vm => await SaveInfo());
            BackCommand = new Command(GoBack);
        }

        private void GoBack()
        {
            var masterPage = App.Current.MainPage as MasterDetailPage;
            masterPage.Detail = new NavigationPage(new HomePage());
        }

        private async Task SaveInfo()
        {
            //Check if any field is left empty
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && !string.IsNullOrWhiteSpace(Mail))
            {
                User.Name = char.ToUpper(Name[0]) + Name.Substring(1);
                User.Surname = char.ToUpper(Surname[0]) + Surname.Substring(1);
                User.Mail = Mail;

                await App.userService.PUT(User);

                var masterPage = App.Current.MainPage as MasterDetailPage;
                masterPage.Detail = new NavigationPage(new HomePage());
            }
        }
    }
}
