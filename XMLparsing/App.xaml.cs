using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using XMLparsing.DataAccess;
using XMLParsing;

namespace XMLparsing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //FrameworkElement.LanguageProperty.OverrideMetadata(
            //   typeof(FrameworkElement),
            //   new FrameworkPropertyMetadata(
            //       XmlLanguage.GetLanguage(
            //       CultureInfo.CurrentCulture.IetfLanguageTag)));
            Database.SetInitializer<XDocContext>(new XDocContextInitializer());
            base.OnStartup(e);
            
        }
    }
}
