using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Support;
namespace Support
{
    public class VMBase : NotifyUIBase
    {
        private bool isNew = true;
        public bool IsNew
        {
            get { return isNew; }
            set
            {
                isNew = value;
                NotifyPropertyChanged();
            }
        }
        private bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool isDeleted = false;

        public bool IsDeleted
        {
            get { return isDeleted; }
            set
            {
                isDeleted = value;
                NotifyPropertyChanged();
            }
        }  
    }
}
