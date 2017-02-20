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

namespace XMLParsing.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        // Properties
        private StudentsInformation studentInformation;
        private Cart cart;
        //public ObservableCollection<CommandVM> Commands { get; set; }
        //public ObservableCollection<ViewVM> Views { get; set; }
        //public string Message { get; set; }
        //public StudentsInformation StudentInformationObject
        //{
        //    get { return studentInformation; }
        //    private set
        //    {
        //        studentInformation = value;
        //        NotifyPropertyChanged("StudentInformationObject");
        //    }
        //}
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
            studentInformation = new StudentsInformation();
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
            CartObject = XMLParsers.ImportItems();
        }

        public RelayCommand CreateXMLDocumentCommand { get; private set; }
        private void CreateXMLDocument()
        {
            //dialog.Show();19058817
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

            //ObservableCollection<ViewVM> views = new ObservableCollection<ViewVM>
            //{
            //    //new ViewVM{ ViewDisplay="Customers", ViewType = typeof(CustomersView), ViewModelType = typeof(CustomersViewModel)},
            //    //new ViewVM{ ViewDisplay="Products", ViewType = typeof(ProductsView), ViewModelType = typeof(ProductsViewModel)}
            //};
            //Views = views;
            //RaisePropertyChanged("Views");
            //views[0].NavigateExecute();

            //ObservableCollection<CommandVM> commands = new ObservableCollection<CommandVM>
            //{
            //   // new CommandVM{ CommandDisplay="Insert", IconGeometry=Application.Current.Resources["InsertIcon"] as Geometry , Message=new CommandMessage{ Command =CommandType.Insert}},
            //   // new CommandVM{ CommandDisplay="Edit", IconGeometry=Application.Current.Resources["EditIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Edit}},
            //    new CommandVM{ CommandDisplay="Delete", IconGeometry=Application.Current.Resources["DeleteIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Delete}},
            //    new CommandVM{ CommandDisplay="Commit", IconGeometry=Application.Current.Resources["SaveIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Commit}},
            //    new CommandVM{ CommandDisplay="Refresh", IconGeometry=Application.Current.Resources["RefreshIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Refresh}}

            //};
            //Commands = commands;
            //NotifyPropertyChanged("Commands");
        }
    }
}
