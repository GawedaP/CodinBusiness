//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdministrationPanel.DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Descriptions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int descriptionID;
        private string _string;
        private int projectsProjectID;

        public int DescriptionID 
        {
            get { return descriptionID; }
            set
            {
                descriptionID = value;
                OnPropertyChanged("DescriptionID");
            }
        }

        public string String 
        {
            get { return _string; }
            set
            {
                _string = value;
                OnPropertyChanged("String");
            }
        }

        public int ProjectsProjectID 
        {
            get { return projectsProjectID; }
            set
            {
                projectsProjectID = value;
                OnPropertyChanged("ProjectsProjectID");
            }
        }
    }
}
