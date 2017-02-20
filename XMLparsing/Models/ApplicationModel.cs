using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLParsing.Models
{
    public class Header
    {
        public Header()
        {
            this.Contacts = new HashSet<ContactInfo>();
            this.Billings = new HashSet<BillingInfo>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HeaderID { get; set; }
        public long SessionID { get; set; }
        public int ItemCount { get; set; }
        public string SentTimeStamp { get; set; }
        public String SentTo { get; set; }
        public int StoreID { get; set; }

        public virtual ICollection<ContactInfo> Contacts { get; set; }
        public virtual ICollection<BillingInfo> Billings { get; set; }

    }

    public class Cart
    {
        public Cart()
        {
            Version = "N/A";
            Description = "N/A";
            Header = new Header();
            ItemList = new List<Item>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Browsable(false)]
        public int CartID { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public Header Header { get; set; }
        public List<Item> ItemList { get; set; }
        
    }

        public class ContactInfo
    {
        [Key]
        public string ContactID { get; set; }
        public String Name { get; set; }
        public String Company { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
    }

    public class BillingInfo
    {
        [Key]
        public string BillingID { get; set; }
        public String Name { get; set; }
        public String Company { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
    }

    public class Item
    {

        public Item()
        {
            this.CustomFields = new HashSet<CustomField>();
        }

        [Key]
        //[Browsable(false)]
        //[Display(AutoGenerateField = false)]
        public string ItemID { get; set; }
        public int ItemSequence { get; set; }
        public String Type { get; set; }
        public String FromArea { get; set; }
        public int Qty { get; set; }
        public String Color { get; set; }
        public String Size { get; set; }
        public int ItemNum { get; set; }
        public String Error { get; set; }
        public String ProductName { get; set; }
        public String PersonalNote { get; set; }
        public string ItemSubTotal { get; set; }
        public long SessionID { get; set; }

        public virtual ICollection<CustomField> CustomFields { get; set; }
    }

    public class CustomField
    {
        [Key]
        public string CustomFieldID { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }
    }

    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string enrollment { get; set; }
        public string comment { get; set; }
    }

    public class StudentsInformation
    {
        public string School { get; set; }
        public string Department { get; set; }
        public List<Student> Studentlist { get; set; }

        public StudentsInformation()
        {
            School = "N/A";
            Department = "N/A";
            Studentlist = new List<Student>();
        }
    }
}
