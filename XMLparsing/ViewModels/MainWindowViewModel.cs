using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMLParsing.MVVMUtilities;
using XMLParsing.Models;
using XMLparsing;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;

namespace XMLParsing.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        // Properties
        private Cart cart;
        //public ObservableCollection<CommandVM> Commands { get; set; }
        //public ObservableCollection<ViewVM> Views { get; set; }
        //public string Message { get; set; }

        public Cart CartObject
        {
            get { return cart; }
            private set
            {
                cart = value;
                NotifyPropertyChanged("CartObject");
            }
        }
        private void InitiateState()
        {
            cart = new Cart();
        }

        //// Commands
        public RelayCommand ClearResultCommand { get; private set; }
        private void ClearResult()
        {
            CartObject = new Cart();
        }
        public RelayCommand ImportItemsCommand { get; private set; }
        private void ImportItems()
        {
            try
            {
                OpenFileDialog Fd = new OpenFileDialog();
                Fd.ShowDialog();
                string LoadedFileName = Fd.FileName;
                CartObject = XMLParsers.ImportItems(LoadedFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public RelayCommand CreateXMLDocumentCommand { get; private set; }
        private void CreateXMLDocument()
        {
            //19058817
            long id = cart.Header.SessionID;
            XMLParsers.CreateXMLDocument(id);
        }

        private void WireCommands()
        {
            ClearResultCommand = new RelayCommand(ClearResult);
            ClearResultCommand.IsEnabled = true;

            ImportItemsCommand = new RelayCommand(ImportItems);
            ImportItemsCommand.IsEnabled = true;

            CreateXMLDocumentCommand = new RelayCommand(CreateXMLDocument);
            CreateXMLDocumentCommand.IsEnabled = true;
        }

        // Constructor
        public MainWindowViewModel()
        {
            InitiateState();
            WireCommands();
        }
    }
}
