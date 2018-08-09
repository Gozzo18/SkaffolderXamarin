using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels.ResourcesViewModel
{
    public class UserEditViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetValue(ref _id, value);
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
        private string _roles;
        public string Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                SetValue(ref _roles, value);
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

        

        

        private bool _isPresent;
        //True = editing User, False = creating new User
        public bool IsPresent
        {
            get
            {
                return _isPresent;
            }
            set
            {
                SetValue(ref _isPresent, value);
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                SetValue(ref _errorMessage, value);
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
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SetDataForEditingCommand { get; private set; }
        
        public ICommand MailCompletedCommand { get; private set; }
        
        public ICommand NameCompletedCommand { get; private set; }
        
        public ICommand RolesCompletedCommand { get; private set; }
        
        public ICommand SurnameCompletedCommand { get; private set; }
        
        public ICommand UsernameCompletedCommand { get; private set; }
        
        
        

        
        #endregion

        public UserEditViewModel(User userToEdit)
        {
            User = userToEdit;

            
            SetDataForEditingCommand = new Command(async vm => await SetData());
            SaveCommand = new Command(async vm => await SaveUserData());
            BackCommand = new Command(async vm => await GoBack());
            
            MailCompletedCommand = new Command<Entry>(vm => MailEntryCompleted(vm));
            
            NameCompletedCommand = new Command<Entry>(vm => NameEntryCompleted(vm));
            
            RolesCompletedCommand = new Command<Entry>(vm => RolesEntryCompleted(vm));
            
            SurnameCompletedCommand = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            
            UsernameCompletedCommand = new Command<Entry>(vm => UsernameEntryCompleted(vm));
            
            
            
            
            
        }

        private async Task SetData()
        {
            
            
            

            if (User != null)
            {
                //Overwrite entries
                Id = User.Id;
                
                Mail = User.Mail;
                
                Name = User.Name;
                
                Roles = User.Roles;
                
                Surname = User.Surname;
                
                Username = User.Username;
                

                

                

                
                IsPresent = true;
            }
            else
            {
                User = new User();
                 
            }
                
        }

        
        private void MailEntryCompleted(Entry UserMail)
        {
            Mail = UserMail.Text;
        }
        private void NameEntryCompleted(Entry UserName)
        {
            Name = UserName.Text;
        }
        private void RolesEntryCompleted(Entry UserRoles)
        {
            Roles = UserRoles.Text;
        }
        private void SurnameEntryCompleted(Entry UserSurname)
        {
            Surname = UserSurname.Text;
        }
        private void UsernameEntryCompleted(Entry UserUsername)
        {
            Username = UserUsername.Text;
        }

        

        

        

        

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();    
            
        }

        private async Task SaveUserData()
        {

            
                User.Mail = Mail;
            
                User.Name = Name;
            
                User.Roles = Roles;
            
                User.Surname = Surname;
            
                User.Username = Username;
            
            
            

            
            
                if (IsPresent)
                {
                    User.Id = User.Id;
                    await App.userService.PUT(User);
                }
                else
                    await App.userService.POST(User);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();   
        }
    }
}
