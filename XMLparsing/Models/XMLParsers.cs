using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using XMLparsing;
using XMLParsing.Models;
using System.Threading.Tasks;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace XMLParsing.Models
{
    public static class XMLParsers
    {

        public static async Task CreateXMLDocumentAsync(long sessionID)
        {
            //await Task.Run(() =>
            await Dispatcher.CurrentDispatcher.InvokeAsync( new Action(() =>
           {
                using (XDocContext db = new XDocContext())
                {
                    UserMessage msg = new UserMessage();
                   Header head = new Header();
                   head = (from h in db.Headers
                                where h.SessionID == sessionID
                                select h).FirstOrDefault();
                    if (head == null)
                    {
                        msg.Message = "This SessinID not exist";
                        Messenger.Default.Send<UserMessage>(msg);
                        return;
                    }
                    List<Item> items = new List<Item>();
                    items = (from i in db.Items
                             where i.SessionID == sessionID
                             select i).ToList<Item>();
                    using (XmlWriter writer = XmlWriter.Create("Output.xml"))
                    {

                        writer.WriteStartDocument();
                        writer.WriteStartElement("carts");

                        foreach (Item item in items)
                        {
                            writer.WriteStartElement("Item");

                            writer.WriteElementString("SessionID", sessionID.ToString());
                            writer.WriteElementString("Name", head.Contacts.Select(a => a.Name).FirstOrDefault());
                            writer.WriteElementString("FromArea", item.FromArea);
                            writer.WriteElementString("Qty", item.Qty.ToString());
                            writer.WriteElementString("Color", item.Color);
                            writer.WriteElementString("Size", item.Size);
                            foreach (var i in item.CustomFields)
                            {
                                if (i.Name.Length > 0)
                                {
                                    writer.WriteElementString(i.Name.Replace(" ", ""), i.Value.ToString());
                                }
                            }
                            writer.WriteElementString("ItemNum", item.ItemNum.ToString());
                            writer.WriteElementString("ProductName", item.ProductName);
                            writer.WriteElementString("PersonalNote", item.PersonalNote);
                            writer.WriteElementString("ItemSubTotal", item.ItemSubTotal);
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    if (File.Exists("Output.xml"))
                        msg.Message = "File Output created";
                    else
                        msg.Message = "File Not created!";
                    Messenger.Default.Send<UserMessage>(msg);
                }

            }));    
        }

        //public static void CreateXMLDocument(long sessionID)
        //{
        //    using (XDocContext db = new XDocContext())
        //    {
        //        UserMessage msg = new UserMessage();
        //        Header head = new Header();
        //        head = (from h in db.Headers
        //                where h.SessionID == sessionID
        //                select h).FirstOrDefault();
        //        if (head == null)
        //        {
        //            msg.Message = "This SessinID not exist";
        //            Messenger.Default.Send<UserMessage>(msg);
        //            return;
        //        }
        //        List<Item> items = new List<Item>();
        //        items = (from i in db.Items
        //                where i.SessionID == sessionID
        //                select i).ToList<Item>();     

        //        using (XmlWriter writer = XmlWriter.Create("Output.xml"))
        //        {
        //            writer.WriteStartDocument();
        //            writer.WriteStartElement("carts");

        //            foreach (Item item in items)
        //            {
        //                writer.WriteStartElement("Item");

        //                writer.WriteElementString("SessionID", sessionID.ToString());
        //                writer.WriteElementString("Name", head.Contacts.Select(a => a.Name).FirstOrDefault());
        //                writer.WriteElementString("FromArea", item.FromArea);
        //                writer.WriteElementString("Qty", item.Qty.ToString());
        //                writer.WriteElementString("Color", item.Color);
        //                writer.WriteElementString("Size", item.Size);
        //                foreach (var i in item.CustomFields)
        //                {
        //                    if (i.Name.Length > 0)
        //                    { 
        //                    writer.WriteElementString(i.Name.Replace(" ", ""), i.Value.ToString());                       
        //                    }
        //                }
        //                writer.WriteElementString("ItemNum", item.ItemNum.ToString());
        //                writer.WriteElementString("ProductName", item.ProductName);
        //                writer.WriteElementString("PersonalNote", item.PersonalNote);
        //                writer.WriteElementString("ItemSubTotal", item.ItemSubTotal);
        //                writer.WriteEndElement();
        //            }

        //            writer.WriteEndElement();
        //            writer.WriteEndDocument();
        //            writer.Close();
        //        }
        //        if (File.Exists("Output.xml"))
        //            msg.Message = "File Output created";
        //        else
        //            msg.Message = "File Not created!";
        //        Messenger.Default.Send<UserMessage>(msg);
        //    }


        //}
        public static Cart ImportItems(string path)
        {
          //await Dispatcher.CurrentDispatcher.InvokeAsync(new Func<Cart>(() =>
          //  {
                using (XDocContext db = new XDocContext())
                {
                    UserMessage msg = new UserMessage();
                    XDocument xdoc = XDocument.Load(path);
                    Cart cart = new Cart();
                    Header head = new Header();
                    bool query = false;
                    XElement generalElement = xdoc
                        .Element("Cart");
                    cart.Version = generalElement.Element("Version").Value;
                    cart.Description = generalElement.Element("Description").Value;
                    foreach (XElement level1Element in xdoc.Descendants("Header"))
                    {
                        head.SessionID = level1Element.Element("SessionID") == null ? -1 : Convert.ToInt64(level1Element.Element("SessionID").Value);
                        head.ItemCount = level1Element.Element("ItemCount") == null ? 0 : Convert.ToInt32(level1Element.Element("ItemCount").Value);
                        head.SentTo = level1Element.Element("SentTo") == null ? string.Empty : level1Element.Element("SentTo").Value;
                        head.StoreID = level1Element.Element("StoreID") == null ? 0 : Convert.ToInt32(level1Element.Element("StoreID").Value);
                        head.SentTimeStamp = level1Element.Element("SentTimeStamp") == null ? string.Empty : level1Element.Element("SentTimeStamp").Value;

                        foreach (XElement level2Element in level1Element.Elements("ContactInfo"))
                        {
                            ContactInfo cnt = new ContactInfo();
                            cnt.ContactID = level2Element.Element("ContactID") == null ? Guid.NewGuid().ToString() : level2Element.Element("ContactID").Value.ToString();
                            cnt.Name = level2Element.Element("Name") == null ? string.Empty : level2Element.Element("Name").Value;
                            cnt.Company = level2Element.Element("Company") == null ? string.Empty : level2Element.Element("Company").Value;
                            cnt.Phone = level2Element.Element("Phone") == null ? string.Empty : level2Element.Element("Phone").Value;
                            cnt.Email = level2Element.Element("Email") == null ? string.Empty : level2Element.Element("Email").Value;
                            head.Contacts.Add(cnt);
                        }
                        foreach (XElement level2Element in level1Element.Elements("BillingInfo"))
                        {
                            BillingInfo blg = new BillingInfo();
                            blg.BillingID = level2Element.Element("BillingID") == null ? Guid.NewGuid().ToString() : level2Element.Element("BillingID").Value.ToString();
                            blg.Name = level2Element.Element("Name") == null ? string.Empty : level2Element.Element("Name").Value;
                            blg.Company = level2Element.Element("Company") == null ? string.Empty : level2Element.Element("Company").Value;
                            blg.Address = level2Element.Element("Address") == null ? string.Empty : level2Element.Element("Address").Value;
                            blg.City = level2Element.Element("City") == null ? string.Empty : level2Element.Element("City").Value;
                            blg.State = level2Element.Element("State") == null ? string.Empty : level2Element.Element("State").Value;
                            blg.Zip = level2Element.Element("Zip") == null ? string.Empty : level2Element.Element("Zip").Value;
                            blg.Phone = level2Element.Element("Phone") == null ? string.Empty : level2Element.Element("Phone").Value;
                            blg.Email = level2Element.Element("Email") == null ? string.Empty : level2Element.Element("Email").Value;
                            head.Billings.Add(blg);
                        }
                        cart.Header = head;
                    }
                    query = db.Headers.Any(a => a.SessionID == head.SessionID);
                    if (!query)
                        db.Headers.Add(head);


                    foreach (XElement level1Element in xdoc.Descendants("Item"))
                    {
                        Item item = new Item();
                        item.ItemID = level1Element.Element("ItemID") == null ? Guid.NewGuid().ToString() : level1Element.Element("ItemID").Value.ToString();
                        item.ItemSequence = level1Element.Element("ItemSequence") == null ? 0 : Convert.ToInt32(level1Element.Element("ItemSequence").Value);
                        item.Type = level1Element.Element("Type") == null ? string.Empty : level1Element.Element("Type").Value;
                        item.FromArea = level1Element.Element("FromArea") == null ? string.Empty : level1Element.Element("FromArea").Value;
                        item.Color = level1Element.Element("Color") == null ? string.Empty : level1Element.Element("Color").Value;
                        item.Qty = level1Element.Element("Qty") == null ? 0 : Int32.Parse(level1Element.Element("Qty").Value);
                        item.Size = level1Element.Element("Size") == null ? string.Empty : level1Element.Element("Size").Value;
                        item.ItemNum = level1Element.Element("ItemNum") == null ? 0 : Int32.Parse(level1Element.Element("ItemNum").Value);
                        item.Error = level1Element.Element("Error") == null ? string.Empty : level1Element.Element("Error").Value;
                        item.ProductName = level1Element.Element("ProductName") == null ? string.Empty : level1Element.Element("ProductName").Value;
                        item.PersonalNote = level1Element.Element("PersonalNote") == null ? string.Empty : level1Element.Element("PersonalNote").Value;
                        item.ItemSubTotal = level1Element.Element("ItemSubTotal") == null ? "0" : level1Element.Element("ItemSubTotal").Value;
                        item.SessionID = head.SessionID;

                        for (int i = 1; i <= 3; i++)
                        {
                            foreach (XElement level2Element in level1Element.Elements("CustomField" + i.ToString()))
                            {
                                CustomField sub = new CustomField();
                                sub.CustomFieldID = level2Element.Element("CustomFieldID") == null ? Guid.NewGuid().ToString() : level2Element.Element("CustomFieldID").Value.ToString();
                                sub.Name = level2Element.Element("Name") == null ? string.Empty : level2Element.Element("Name").Value;
                                sub.Value = level2Element.Element("Value") == null ? string.Empty : level2Element.Element("Value").Value;
                                item.CustomFields.Add(sub);
                            }

                        }
                        cart.ItemList.Add(item);
                        db.Items.Add(item);
                    }
                    if (!query && db.ChangeTracker.HasChanges())
                        try
                        {
                            db.SaveChanges();
                            msg.Message = "Database Updated";
                        }
                        catch (Exception e)
                        {
                            msg.Message = "There was a problem updating the database " + e.Message;
                        }
                    else
                    {
                        msg.Message = "No changes to save";
                    }
                    Messenger.Default.Send<UserMessage>(msg);
                    return cart;
                }
            //}));
        }

    }
}
