using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace InitechSupport
{
    public class CustomerDataClass : INotifyPropertyChanged
    {
        #region propertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        public CustomerDataClass(string datafile)
        {
            DataFileName = datafile;
            CustomerList = LoadFile();
        }

        public CustomerListCollection CustomerList { get; set; }

        #region file load/save
        /// <summary>
        /// this function is used when loading the file
        /// </summary>

        public CustomerListCollection LoadFile()
        {
            if (!File.Exists(DataFileName))
            {
                return CreateNew();
            }
            try
            {
                XmlSerializer serialPlay = new XmlSerializer(typeof(CustomerListCollection));
                System.IO.StreamReader xmlfile = new System.IO.StreamReader(DataFileName);
                Object localfile = serialPlay.Deserialize(xmlfile);
                xmlfile.Close();
                return (CustomerListCollection)localfile;
            }
            catch
            {
                return CreateNew();
            }
        }

        public CustomerListCollection CreateNew()
        {
            // create a couple records...
            CustomerListCollection newCollection = new CustomerListCollection();
            CustomerListClass newItem = new CustomerListClass(); // add one customer
            CallListClass newCall = new CallListClass();  // add one call

            newItem.CallList.Add(newCall);
            newCollection.Add(newItem);
            return newCollection;
        }

        public void SaveFile()
        {
            if (!Autosave) return;

            XmlSerializer serialPlay = new XmlSerializer(typeof(CustomerListCollection));
            System.IO.StreamWriter xmlfile = new System.IO.StreamWriter(DataFileName, false);  // do not append
            serialPlay.Serialize(xmlfile, CustomerList);
            xmlfile.Close();
        }

        public string DataFileName { get; set; }
        public bool Autosave
        {
            get
            {
                return Properties.Settings.Default.Autosave;
            }
            set
            {
                Properties.Settings.Default.Autosave = value;
                Properties.Settings.Default.Save();
            }
        }

        #endregion file load/save

        public bool EditCustomer
        {
            get
            {
                return Properties.Settings.Default.EditCustomer;
            }
            set
            {
                Properties.Settings.Default.EditCustomer = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged("EditCustomer");
            }
        }
        
    }

    public class CustomerListCollection: ObservableCollection<CustomerListClass>
    {
        public CustomerListCollection()
        {
        }

    }

    public class CustomerListClass : INotifyPropertyChanged
    {
        public CustomerListClass()
        {
            CustomerName = "new customer";
            Address = "address";
            PurchaseDate = DateTime.Now.Date;
            CallList = new CallListCollection();
        }

        #region propertychanged
                public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        // Customer ID would be used if the data is stored in a database, but is not needed here
        //public int CustomerID { get; set; }

        private string l_customerName;
        public string CustomerName { get { return l_customerName; } set { l_customerName = value; OnPropertyChanged("CustomerName"); } }

        private string l_address;
        public string Address { get { return l_address; } set { l_address = value; OnPropertyChanged("Address"); } }

        private DateTime l_purchaseDate;
        public DateTime PurchaseDate { get { return l_purchaseDate; } set { l_purchaseDate = value; OnPropertyChanged("PurchaseDate"); } }

        public CallListCollection CallList { get; set; }

    }
}
