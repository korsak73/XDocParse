using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media.Animation;
using XMLParsing.ViewModels;
using XMLParsing;

namespace XMLparsing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            Messenger.Default.Register<NavigateMessage>(this, (action) => ShowUserControl(action));
            Messenger.Default.Register<UserMessage>(this, (action) => ReceiveUserMessage(action));
            this.DataContext = new MainWindowViewModel();
        }

        private void ReceiveUserMessage(UserMessage msg)
        {
            UIMessage.Opacity = 1;
            UIMessage.Text = msg.Message;
            Storyboard sb = (Storyboard)this.FindResource("FadeUIMessage");
            sb.Begin();
        }
        private void ShowUserControl(NavigateMessage nm)
        {
            EditFrame.Content = nm.View;
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ItemID")
            {
                e.Cancel = true;
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var dialog = new MyDialog();
        //    dialog.Show();
        //    string inputID = dialog.InputText;
        //    //dialog.Closing += (sender, e) =>
        //    //{
        //    //    var d = sender as MyDialog;
        //    //    if (!d.Canceled)
        //    //        MessageBox.Show(d.InputText);
        //    //};
        //    long id = Convert.ToInt64(inputID);
        //}
    }
}
