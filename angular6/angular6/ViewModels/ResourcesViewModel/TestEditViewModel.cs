using angular6.Models;
using angular6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace angular6.ViewModels.ResourcesViewModel
{
    public class TestEditViewModel : BaseViewModel
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

        
        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                SetValue(ref _nome, value);
            }
        }

        

        

        private bool _isPresent;
        //True = editing Test, False = creating new Test
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

        

        private Test _test;
        public Test Test
        {
            get
            {
                return _test;
            }
            set
            {
                SetValue(ref _test, value);
            }
        }

        
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        
        public ICommand NomeCompletedCommand { get; private set; }
        
        
        

        public ICommand SetDataForEditingCommand { get; private set; }
        
        #endregion

        public TestEditViewModel(Test testToEdit)
        {
            Test = testToEdit;

            SetDataForEditingCommand = new Command(async vm => await SetData());
            
            SaveCommand = new Command(async vm => await SaveTestData());
            BackCommand = new Command(async vm => await GoBack());
            
            NomeCompletedCommand = new Command<Entry>(vm => NomeEntryCompleted(vm));
            
            
            
            
        }
        
        private async Task SetData()
        {
            
            
            

            if (Test != null)
            {
                IsPresent = true;
                //Overwrite entries
                Id = Test.Id;
                
                Nome = Test.Nome;
                

                

                
            

                

                
            }
            else
            {
                Test = new Test();
                 
            }
                
        }

        
        private void NomeEntryCompleted(Entry TestNome)
        {
            Nome = TestNome.Text;
        }

        

        

        

        

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();    
            
        }

        private async Task SaveTestData()
        {
            Test test = new Test();

            
                test.Nome = Nome;
            
            
            

            
            
                if (IsPresent)
                {
                    test.Id = Test.Id;
                    await App.testService.PUT(test);
                }
                else
                    await App.testService.POST(test);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();   
        }
    }
}
