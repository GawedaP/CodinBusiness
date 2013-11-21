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
using System.Data;
using System.Threading;
using AdministrationPanel.DatabaseEntityFramework;
using AdministrationPanel.EntryForms;
using AdministrationPanel.Various;

namespace AdministrationPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields - selectedItem

        Companies selectedCompany = new Companies();
        Descriptions selectedDesciption = new Descriptions();
        Empolyees selectedEmpolyee = new Empolyees();
        Projects selectedProject = new Projects();
        TypesOfEmployees selectedTypesOfEmployee = new TypesOfEmployees();
        TypesOfOffices selectedTypesOfOffice = new TypesOfOffices();
        Users selectedUser = new Users();

        #endregion

        #region Fields

        string selectedTable;
        short newSelectedItem = 1;
        Welcome welcome;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            this.textBoxQuery.TextChanged += textBoxQuery_TextChanged;
            this.textBoxQuery.TextChanged += this.TextChangedEventHandler;
            welcome = new Welcome(this);
            this.Hide();
            welcome.Show();
        }

        #endregion

        #region Controls

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItemSelectedItem = (ComboBoxItem)comboBoxSelectTable.SelectedItem;
            selectedTable = comboBoxItemSelectedItem.Content.ToString();
            Thread newThread = new Thread(CreatingQuery);
            newThread.Start();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (newSelectedItem > 0)
            {
                if (selectedTable == "Users")
                {
                    selectedUser = ((DataGrid)sender).SelectedItem as Users;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }

                else if (selectedTable == "Description")
                {
                    selectedDesciption = ((DataGrid)sender).SelectedItem as Descriptions;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }

                else if (selectedTable == "TypesOfEmployees")
                {
                    selectedTypesOfEmployee = ((DataGrid)sender).SelectedItem as TypesOfEmployees;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }

                else if (selectedTable == "Projects")
                {
                    selectedProject = ((DataGrid)sender).SelectedItem as Projects;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }


                else if (selectedTable == "TypesOfOffices")
                {
                    selectedTypesOfOffice = ((DataGrid)sender).SelectedItem as TypesOfOffices;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }

                else if (selectedTable == "Employees")
                {
                    selectedEmpolyee = ((DataGrid)sender).SelectedItem as Empolyees;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }

                else if (selectedTable == "Companies")
                {
                    selectedCompany = ((DataGrid)sender).SelectedItem as Companies;
                    buttonModify.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                }
                if (newSelectedItem == 2)
                {
                    buttonModify.IsEnabled = false;
                    buttonDelete.IsEnabled = false;
                    newSelectedItem = 1;
                }
            }
        }
        
        private void textBoxQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CleanString(richTextBoxQuery()).Length > 0)
            {
                this.buttonExecute.IsEnabled = true;
            }
            else
            {
                this.buttonExecute.IsEnabled = false;
            }
        }


        #endregion

        #region Buttons

        /// <summary>
        /// Obsługa przycisku "Execute"
        /// </summary>
        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            int result;
            using (var db = new BusinessCodingModelContex())
            {
                string query = richTextBoxQuery();
                try
                {
                    result = db.Database.ExecuteSqlCommand(query);
                }
                catch
                {
                    MessageBox.Show("Wrong query");
                    return;
                }
                if (result < 0)
                {
                    result = 0;
                }
                MessageBox.Show(string.Format("Result: {0} row(s) affected", result));

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Obsługa przycisku "Add"
        /// </summary>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable == "TypesOfEmployees")
            {
                EntryFormTypeOfEmployee entryForm = new EntryFormTypeOfEmployee();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    TypesOfEmployees toe = CreatingTypesOfEmployeesObjectFromEntryForm(entryForm);
                    Thread newThread = new Thread(AddRecordToTypesOfEmployees);
                    newThread.Start(toe);
                }
            }

            else if (selectedTable == "TypesOfOffices")
            {
                EntryFormTypeOfOffice entryForm = new EntryFormTypeOfOffice();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    TypesOfOffices too = CreatingTypesOfOfficesObjectFromEntryForm(entryForm);
                    Thread newThread = new Thread(AddRecordToTypesOfOffices);
                    newThread.Start(too);
                }
            }

            else if (selectedTable == "Users")
            {
                EntryFormUser entryForm = new EntryFormUser();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    Users newUser = CreatingUsersObjectFromEntryForm(entryForm);

                    Thread newThread = new Thread(AddRecordToUsers);
                    newThread.Start(newUser);
                }
            }
            else if (selectedTable == "Description")
            {
                EntryFormDescription entryForm = new EntryFormDescription();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    Descriptions newDescription = CreatingDescriptionObjectFromEntryForm(entryForm);
                    Thread newThread = new Thread(AddRecordToDescriptions);
                    newThread.Start(newDescription);
                }
            }

            else if (selectedTable == "Projects")
            {
                EntryFormProject entryForm = new EntryFormProject();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    Projects newProject = CreatingProjectsObjectFromEntryForm(entryForm);

                    Thread newThread = new Thread(AddRecordToProjects);
                    newThread.Start(newProject);
                }
            }

            else if (selectedTable == "Employees")
            {
                EntryFormEmpolyee entryForm = new EntryFormEmpolyee();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    Empolyees newEmployee = CreatingEmployeeObjectFromEntryForm(entryForm);

                    Thread newThread = new Thread(AddRecordToEmployees);
                    newThread.Start(newEmployee);
                }
            }

            else if (selectedTable == "Companies")
            {
                EntryFormCompany entryForm = new EntryFormCompany();
                bool? result = entryForm.ShowDialog();
                if ((bool)result)
                {
                    Companies newCompany = CreatingCompaniesObjectFromEntryForm(entryForm);

                    Thread newThread = new Thread(AddRecordToCompanies);
                    newThread.Start(newCompany);

                }
            }
        }

        /// <summary>
        /// Obsługa przycisku "Modify"
        /// </summary>
        private void buttonModify_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable == "Users")
            {
                ModifyRecordFromUsers();
            }

            else if (selectedTable == "Description")
            {
                ModifyRecordFromDescriptions();
            }

            else if (selectedTable == "TypesOfEmployees")
            {
                ModifyRecordFromTypesOfEmployees();
            }

            else if (selectedTable == "Projects")
            {
                ModifyRecordFromProjects();
            }

            else if (selectedTable == "TypesOfOffices")
            {
                ModifyRecordFromTypeOfOffices();
            }

            else if (selectedTable == "Employees")
            {
                ModifyRecordFromEmployees();
            }

            else if (selectedTable == "Companies")
            {
                ModifyRecordFromCompanies();
            }
            this.buttonDelete.IsEnabled = false;
            this.buttonModify.IsEnabled = false;
        }

        /// <summary>
        /// Obsługa przycisku "Delete"
        /// </summary>
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Attention", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                newSelectedItem = 2;
                if (selectedTable == "Users")
                {
                    Thread newThread = new Thread(DeleteRecordFromUsers);
                    newThread.Start();
                }

                else if (selectedTable == "Description")
                {
                    Thread newThread = new Thread(DeleteRecordFromDescriptions);
                    newThread.Start();
                }

                else if (selectedTable == "TypesOfEmployees")
                {
                    Thread newThread = new Thread(DeleteRecordFromTypesOfEmployees);
                    newThread.Start();
                }

                else if (selectedTable == "Projects")
                {
                    Thread newThread = new Thread(DeleteRecordFromProjects);
                    newThread.Start();
                }

                else if (selectedTable == "TypesOfOffices")
                {
                    Thread newThread = new Thread(DeleteRecordFromTypesOfOffices);
                    newThread.Start();;
                }

                else if (selectedTable == "Employees")
                {
                    Thread newThread = new Thread(DeleteRecordFromEmployees);
                    newThread.Start();
                }

                else if (selectedTable == "Companies")
                {
                    Thread newThread = new Thread(DeleteRecordFromCompanies);
                    newThread.Start();
                }
                this.buttonDelete.IsEnabled = false;
                this.buttonModify.IsEnabled = false;
            }
            
        }

        #endregion

        #region Creating Objects From Entry Form

        private static Empolyees CreatingEmployeeObjectFromEntryForm(EntryFormEmpolyee entryForm)
        {
            Empolyees newEmployee = new Empolyees();
            try
            {
                newEmployee.CompaniesCompanyID = int.Parse(entryForm.textBoxCompaniesCompanyID.Text);
            }
            catch
            {
                newEmployee.CompaniesCompanyID = null;
            }
            try
            {
                newEmployee.DateOfHiring = DateTime.Parse(entryForm.textBoxDateOfHiring.Text);
            }
            catch
            {
                newEmployee.DateOfHiring = null;
            }
            newEmployee.TypesOfEmployeesTOEmployeeID = int.Parse(entryForm.textBoxTypesOfEmployees_TOEmployeeID.Text);
            return newEmployee;
        }

        private static Descriptions CreatingDescriptionObjectFromEntryForm(EntryFormDescription entryForm)
        {
            Descriptions newDescription = new Descriptions();
            newDescription.String = entryForm.textBoxString.Text;
            newDescription.ProjectsProjectID = int.Parse(entryForm.textBoxProjectID.Text);
            return newDescription;
        }

        private static Companies CreatingCompaniesObjectFromEntryForm(EntryFormCompany entryForm)
        {
            Companies newCompany = new Companies();
            newCompany.CostOfLiving = double.Parse(entryForm.textBoxCostOfLiving.Text);
            newCompany.Money = double.Parse(entryForm.textBoxMoney.Text);
            newCompany.Name = entryForm.textBoxName.Text;
            newCompany.Respect = int.Parse(entryForm.textBoxRespect.Text);
            newCompany.TypesOfOfficesTOOfficeID = int.Parse(entryForm.textBoxTypesOfOffices_TOOfficeID.Text);
            newCompany.UsersUserID = int.Parse(entryForm.textBoxUsersUserID.Text);
            return newCompany;
        }

        private static TypesOfOffices CreatingTypesOfOfficesObjectFromEntryForm(EntryFormTypeOfOffice entryForm)
        {
            TypesOfOffices too = new TypesOfOffices();

            too.Capacity = short.Parse(entryForm.textBoxCapacity.Text);
            too.CostOfHiring = double.Parse(entryForm.textBoxCostOfHiring.Text);
            too.Name = entryForm.textBoxName.Text;
            return too;
        }

        private static TypesOfEmployees CreatingTypesOfEmployeesObjectFromEntryForm(EntryFormTypeOfEmployee entryForm)
        {
            TypesOfEmployees toe = new TypesOfEmployees();

            toe.Codity = short.Parse(entryForm.textBoxCodity.Text);
            toe.Name = entryForm.textBoxName.Text;
            toe.Salary = double.Parse(entryForm.textBoxSalary.Text);
            return toe;
        }

        private static Users CreatingUsersObjectFromEntryForm(EntryFormUser entryForm)
        {
            Users newUser = new Users();

            newUser.ActivationDate = DateTime.Parse(entryForm.textBoxActivationDate.Text);
            newUser.Email = entryForm.textBoxEmail.Text;
            newUser.LastLogin = DateTime.Parse(entryForm.textBoxLastLogin.Text);
            newUser.Login = entryForm.textBoxLogin.Text;
            newUser.Password = entryForm.textBoxPassword.Text;
            return newUser;
        }

        private static Projects CreatingProjectsObjectFromEntryForm(EntryFormProject entryForm)
        {
            Projects newProject = new Projects();

            try
            {
                newProject.CompaniesCompanyID = int.Parse(entryForm.textBoxCompaniesCompanyID.Text);
            }
            catch
            {
                newProject.CompaniesCompanyID = null;
            }
            newProject.Complexity = short.Parse(entryForm.textBoxComplexity.Text);
            newProject.Gratification = double.Parse(entryForm.textBoxGratification.Text);
            newProject.MinCodity = short.Parse(entryForm.textBoxMinCodity.Text);
            newProject.MinRespect = int.Parse(entryForm.textBoxMinRespect.Text);
            newProject.Name = entryForm.textBoxName.Text;
            newProject.TimeRounds = short.Parse(entryForm.textBoxTimeRounds.Text);
            try
            {
                newProject.TimeToEnd = short.Parse(entryForm.textBoxTimeToEnd.Text);
            }
            catch
            {
                newProject.TimeToEnd = null;
            }
            return newProject;
        }

        #endregion

        #region Display Records

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "Companies"
        /// </summary>
        private void DisplayAllRecordsFromCompanies()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.Companies select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);
            }
        }

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "Employees"
        /// </summary>
        private void DisplayAllRecordsFromEmployees()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.Empolyees select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);
            }
        }

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "TypesOfOffices"
        /// </summary>
        private void DisplayAllRecordsFromTypesOfOffices()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.TypesOfOffices select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);
            }
        }

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "Projects"
        /// </summary>
        private void DisplayAllRecordsFromProjects()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.Projects select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);
            }
        }

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "TypesOfEmployees"
        /// </summary>
        private void DisplayAllRecordsFromTypesOfEmployees()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.TypesOfEmployees select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);

            }
        }

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "Descriptions"
        /// </summary>
        private void DisplayAllRecordsFromDescriptions()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.Descriptions select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);
            }
        }

        /// <summary>
        /// Wyświetlanie wszystkich rekordó z tabeli "Users"
        /// </summary>
        private void DisplayAllRecordsFromUsers()
        {
            using (var db = new BusinessCodingModelContex())
            {
                var query = from x in db.Users select x;
                var result = query.ToList();
                dataGrid.InvokeIfRequired((value) => dataGrid.ItemsSource = value, result);
            }
        }

        #endregion

        #region Add Records

        /// <summary>
        /// Dodanie rekordu do tabeli Users.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToUsers(object entry)
        {
            Users newUser = (Users)entry;
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    newUser.UserID = db.Users.Max(x => x.UserID) + 1;
                }
                catch
                {
                    newUser.UserID = 1;
                }

                db.Users.Add(newUser);

                db.SaveChanges();

                Thread newThread = new Thread(DisplayAllRecordsFromUsers);
                newThread.Start();
            }
        }

        /// <summary>
        /// Dodanie rekordu do tabeli Projects.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToProjects(object entry)
        {
            Projects newProject = (Projects)entry;
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    newProject.ProjectID = db.Projects.Max(x => x.ProjectID) + 1;
                }
                catch
                {
                    newProject.ProjectID = 1;
                }

                db.Projects.Add(newProject);

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to add a good reference.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromProjects);
                newThread.Start();
            }
        }

        /// <summary>
        /// Dodanie rekordu do tabeli Employees.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToEmployees(object entry)
        {
            Empolyees newEmployee = (Empolyees)entry;
            using (var db = new BusinessCodingModelContex())
            {

                try
                {
                    newEmployee.EmployeeID = db.Empolyees.Max(x => x.EmployeeID) + 1;
                }
                catch
                {
                    newEmployee.EmployeeID = 1;
                }
                db.Empolyees.Add(newEmployee);

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to add a good reference.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromEmployees);
                newThread.Start();
            }
        }

        /// <summary>
        /// Dodanie rekordu do tabeli Descriptions.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToDescriptions(object entry)
        {
            Descriptions newDescription = (Descriptions)entry;
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    newDescription.DescriptionID = db.Descriptions.Max(x => x.DescriptionID) + 1;
                }
                catch
                {
                    newDescription.DescriptionID = 1;
                }

                db.Descriptions.Add(newDescription);

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to add a good reference.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromDescriptions);
                newThread.Start();
            }
        }

        /// <summary>
        /// Dodanie rekordu do tabeli Companies.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToCompanies(object entry)
        {
            Companies newCompany = (Companies)entry;
            using (var db = new BusinessCodingModelContex())
            {

                try
                {
                    newCompany.CompanyID = db.Companies.Max(x => x.CompanyID) + 1;
                }
                catch
                {
                    newCompany.CompanyID = 1;
                }
                db.Companies.Add(newCompany);

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to add a good reference.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromCompanies);
                newThread.Start();
            }
        }

        /// <summary>
        /// Dodanie rekordu do tabeli TypesOfOffices.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToTypesOfOffices(object entry)
        {
            TypesOfOffices too = (TypesOfOffices)entry;
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    too.TOOfficeID = db.TypesOfOffices.Max(x => x.TOOfficeID) + 1;
                }
                catch
                {
                    too.TOOfficeID = 1;
                }

                db.TypesOfOffices.Add(too);

                db.SaveChanges();

                Thread newThread = new Thread(DisplayAllRecordsFromTypesOfOffices);
                newThread.Start();
            }
        }
        /// <summary>
        /// Dodanie rekordu do tabeli TypesOfEmployees.
        /// </summary>
        /// <param name="entryForm">okno z którego pobieramy dane</param>
        private void AddRecordToTypesOfEmployees(object entry)
        {
            TypesOfEmployees toe = (TypesOfEmployees)entry;
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    toe.TOEmployeeID = db.TypesOfEmployees.Max(x => x.TOEmployeeID) + 1;
                }
                catch
                {
                    toe.TOEmployeeID = 1;
                }

                db.TypesOfEmployees.Add(toe);

                db.SaveChanges();

                Thread newThread = new Thread(DisplayAllRecordsFromTypesOfEmployees);
                newThread.Start();
            }
        }

        #endregion 

        #region Modify Records

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "Companiess"
        /// </summary>
        private void ModifyRecordFromCompanies()
        {
            EntryFormCompany entryForm = new EntryFormCompany(selectedCompany);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                newSelectedItem = 2;
                Thread newThread = new Thread(ModifyingCompaniesInNewThread);
                newThread.Start();
            }
            else
            {
                newSelectedItem = 1;
            }
        }

        private void ModifyingCompaniesInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.Companies.Attach(selectedCompany);
                var entry = db.Entry(selectedCompany);

                entry.Property(x => x.CostOfLiving).IsModified = true;
                entry.Property(x => x.Money).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;
                entry.Property(x => x.Respect).IsModified = true;
                entry.Property(x => x.TypesOfOfficesTOOfficeID).IsModified = true;
                entry.Property(x => x.UsersUserID).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to give a reference to existing record.");
                }
            }
            Thread newThread = new Thread(DisplayAllRecordsFromCompanies);
            newThread.Start();
        }

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "Descriptions"
        /// </summary>
        private void ModifyRecordFromDescriptions()
        {
            EntryFormDescription entryForm = new EntryFormDescription(selectedDesciption);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                newSelectedItem = 2;
                Thread newThread = new Thread(ModifyingDescriptionsInNewThread);
                newThread.Start();
            }
            else
            {
                newSelectedItem = 1;
            }
        }

        private void ModifyingDescriptionsInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.Descriptions.Attach(selectedDesciption);
                var entry = db.Entry(selectedDesciption);

                entry.Property(x => x.ProjectsProjectID).IsModified = true;
                entry.Property(x => x.String).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to give a reference to existing record.");
                }
            }
            Thread newThread = new Thread(DisplayAllRecordsFromDescriptions);
            newThread.Start();
        }

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "Employees"
        /// </summary>
        private void ModifyRecordFromEmployees()
        {
            EntryFormEmpolyee entryForm = new EntryFormEmpolyee(selectedEmpolyee);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                newSelectedItem = 2;
                Thread newThread = new Thread(ModifyingEmplyeesInNewThread);
                newThread.Start();
            }
            else
            {
                newSelectedItem = 1;
            }
        }

        private void ModifyingEmplyeesInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.Empolyees.Attach(selectedEmpolyee);
                var entry = db.Entry(selectedEmpolyee);

                entry.Property(x => x.CompaniesCompanyID).IsModified = true;
                entry.Property(x => x.DateOfHiring).IsModified = true;
                entry.Property(x => x.TypesOfEmployeesTOEmployeeID).IsModified = true;

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to give a reference to existing record.");
                }
            }
            Thread newThread = new Thread(DisplayAllRecordsFromEmployees);
            newThread.Start();
        }

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "Projects"
        /// </summary>
        private void ModifyRecordFromProjects()
        {
            EntryFormProject entryForm = new EntryFormProject(selectedProject);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                newSelectedItem = 2;
                Thread newThread = new Thread(ModyfingProjectsInNewThread);
                newThread.Start();
            }
            else
            {
                newSelectedItem = 1;
            }
        }

        private void ModyfingProjectsInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.Projects.Attach(selectedProject);
                var entry = db.Entry(selectedProject);

                entry.Property(x => x.CompaniesCompanyID).IsModified = true;
                entry.Property(x => x.Complexity).IsModified = true;
                entry.Property(x => x.Gratification).IsModified = true;
                entry.Property(x => x.MinCodity).IsModified = true;
                entry.Property(x => x.MinRespect).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;
                entry.Property(x => x.TimeRounds).IsModified = true;
                entry.Property(x => x.TimeToEnd).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to give a reference to existing record.");
                }
            }
            Thread newThread = new Thread(DisplayAllRecordsFromProjects);
            newThread.Start();
        }

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "TypesOfEmployees"
        /// </summary>
        private void ModifyRecordFromTypesOfEmployees()
        {
            EntryFormTypeOfEmployee entryForm = new EntryFormTypeOfEmployee(selectedTypesOfEmployee);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                newSelectedItem = 2;
                Thread newThread = new Thread(ModifyingTypesOfEmployeesInNewThread);
                newThread.Start();
            }
            else
            {
                newSelectedItem = 1;
            }
        }

        private void ModifyingTypesOfEmployeesInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.TypesOfEmployees.Attach(selectedTypesOfEmployee);
                var entry = db.Entry(selectedTypesOfEmployee);

                entry.Property(x => x.Codity).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;
                entry.Property(x => x.Salary).IsModified = true;

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("You have to give a reference to existing record.");
                }
            }
            Thread newThread = new Thread(DisplayAllRecordsFromTypesOfEmployees);
            newThread.Start();
        }

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "TypeOfOffices"
        /// </summary>
        private void ModifyRecordFromTypeOfOffices()
        {
            EntryFormTypeOfOffice entryForm = new EntryFormTypeOfOffice(selectedTypesOfOffice);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                newSelectedItem = 2;
                Thread newThread = new Thread(ModifyingTypeOfOfficesInNewThread);
                newThread.Start();
            }
            else
            {
                newSelectedItem = 1;
            }
        }

        private void ModifyingTypeOfOfficesInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.TypesOfOffices.Attach(selectedTypesOfOffice);
                var entry = db.Entry(selectedTypesOfOffice);

                entry.Property(x => x.Capacity).IsModified = true;
                entry.Property(x => x.CostOfHiring).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;

                db.SaveChanges();
            }
            Thread newThread = new Thread(DisplayAllRecordsFromTypesOfOffices);
            newThread.Start();
        }

        /// <summary>
        /// Modyfikowanie zaznaczonego rekordu w tabeli "Users"
        /// </summary>
        private void ModifyRecordFromUsers()
        {
            EntryFormUser entryForm = new EntryFormUser(selectedUser);

            bool? result = entryForm.ShowDialog();
            if ((bool)result)
            {
                Thread newThread = new Thread(ModyfingUsersInNewThread);
                newThread.Start();
            }
        }

        private void ModyfingUsersInNewThread()
        {
            using (var db = new BusinessCodingModelContex())
            {
                db.Users.Attach(selectedUser);
                var entry = db.Entry(selectedUser);

                entry.Property(x => x.ActivationDate).IsModified = true;
                entry.Property(x => x.Email).IsModified = true;
                entry.Property(x => x.LastLogin).IsModified = true;
                entry.Property(x => x.Login).IsModified = true;
                entry.Property(x => x.Password).IsModified = true;

                db.SaveChanges();
            }
            Thread newThread = new Thread(DisplayAllRecordsFromUsers);
            newThread.Start();
        }

        #endregion

        #region Delete Records

        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "Users"
        /// </summary>
        private void DeleteRecordFromUsers()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.Users.Attach(selectedUser);
                    db.Users.Remove(selectedUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromUsers);
                newThread.Start();
            }
        }

        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "Descriptions"
        /// </summary>
        private void DeleteRecordFromDescriptions()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.Descriptions.Attach(selectedDesciption);
                    db.Descriptions.Remove(selectedDesciption);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromDescriptions);
                newThread.Start();
            }
        }

        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "TypesOfEmployees"
        /// </summary>
        private void DeleteRecordFromTypesOfEmployees()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.TypesOfEmployees.Attach(selectedTypesOfEmployee);
                    db.TypesOfEmployees.Remove(selectedTypesOfEmployee);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromTypesOfEmployees);
                newThread.Start();
            }
        }

        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "TypesOfOffices"
        /// </summary>
        private void DeleteRecordFromTypesOfOffices()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.TypesOfOffices.Attach(selectedTypesOfOffice);
                    db.TypesOfOffices.Remove(selectedTypesOfOffice);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromTypesOfOffices);
                newThread.Start();
            }
        }

        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "Projects"
        /// </summary>
        private void DeleteRecordFromProjects()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.Projects.Attach(selectedProject);
                    db.Projects.Remove(selectedProject);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromProjects);
                newThread.Start();
            }
        }

        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "Employees"
        /// </summary>
        private void DeleteRecordFromEmployees()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.Empolyees.Attach(selectedEmpolyee);
                    db.Empolyees.Remove(selectedEmpolyee);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromEmployees);
                newThread.Start();
            }
        }
        
        /// <summary>
        /// Usuwanie zaznaczonego elementu z tabeli "Companies"
        /// </summary>
        private void DeleteRecordFromCompanies()
        {
            using (var db = new BusinessCodingModelContex())
            {
                try
                {
                    db.Companies.Attach(selectedCompany);
                    db.Companies.Remove(selectedCompany);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!");
                    return;
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Cannot delete a record to which reference is.");
                    return;
                }

                Thread newThread = new Thread(DisplayAllRecordsFromCompanies);
                newThread.Start();
            }
        }

        #endregion
  
        #region Additional Methods

        /// <summary>
        /// Wyciąganie tekstu z RichTextBoxa
        /// </summary>
        /// <returns>tekst z RichTextBoxa</returns>
        private string richTextBoxQuery()
        {
            string tr = new TextRange(textBoxQuery.Document.ContentStart, textBoxQuery.Document.ContentEnd).Text;
            return tr;
        }

        /// <summary>
        /// Wczytywanie danych do dataGrid w zależości od wybranej tabeli
        /// </summary>
        private void CreatingQuery()
        {
            switch (selectedTable)
            {
                case "Users":
                    DisplayAllRecordsFromUsers();
                    break;

                case "Description":
                    DisplayAllRecordsFromDescriptions();
                    break;

                case "TypesOfEmployees":
                    DisplayAllRecordsFromTypesOfEmployees();
                    break;

                case "Projects":
                    DisplayAllRecordsFromProjects();
                    break;

                case "TypesOfOffices":
                    DisplayAllRecordsFromTypesOfOffices();
                    break;

                case "Employees":
                    DisplayAllRecordsFromEmployees();
                    break;

                case "Companies":
                    DisplayAllRecordsFromCompanies();
                    break;
            }
        }

        /// <summary>
        /// Metoda oczyszczająca napis z '\'
        /// </summary>
        /// <param name="s">czyszczony napis</param>
        /// <returns>oczyszczony napis</returns>
        private string CleanString(string s)
        {
            string newString = "";

            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 13)
                {
                    i += 2;
                }
                else
                {
                    newString += s[i];
                }
            }
            return newString;
        }

        #endregion

        #region Syntax Highlithing

        private string value = string.Empty;
        private string[] keywords = new string[] { "alter", "update", "table", "insert", "delete", "select", "from", "where", "on", "in", "is", "as", "between", "and", "or", "where", "join", "left", "right", "count", "}", "{", ")", "(", "*" };
        private int all = 0;

        /// <summary>
        /// Podosi event gdy zostanie zmieniony text w RichTextBoxie
        /// Ten event używany jest do kolorowania składni
        /// </summary>
        private void TextChangedEventHandler(object sender, TextChangedEventArgs e)
        {

            TextChange change = (from x in e.Changes where x.Offset == e.Changes.Max(p => p.Offset) select x).FirstOrDefault();

            if (change != null && all != 0 && change.RemovedLength >= all)
            {
                textBoxQuery.Document.Blocks.Clear();
                return;
            }
            if (change != null && change.Offset != 0)
            {
                TextRange range = new TextRange(this.textBoxQuery.Document.ContentStart.GetPositionAtOffset(change.Offset - 1), this.textBoxQuery.Document.ContentStart.GetPositionAtOffset(change.Offset));

                if (range.Text != " ")
                {
                    if (change.AddedLength > 0)
                    {
                        all += change.AddedLength;
                        value += range.Text;
                    }
                    else if (change.RemovedLength > 0)
                    {
                        all -= change.RemovedLength;
                        if (value.Length != 0)
                        {

                            value = value.Remove(value.Length - 1);
                        }
                        else
                            value = string.Empty;
                    }
                    // to do
                    TryColorSyntax(value, change);
                }
                else
                    value = string.Empty;
            }
        }

        /// <summary>
        /// Koloruje składnie.
        /// </summary>
        private void TryColorSyntax(string value, TextChange current)
        {
            textBoxQuery.TextChanged -= this.TextChangedEventHandler;
            TextRange r;
            try
            {
                r = new TextRange(this.textBoxQuery.Document.ContentStart.GetPositionAtOffset(current.Offset - 1 - value.Length), this.textBoxQuery.Document.ContentStart.GetPositionAtOffset(current.Offset));
            }
            catch
            {
                textBoxQuery.Document.Blocks.Clear();
                textBoxQuery.AppendText(" ");
                textBoxQuery.TextChanged += this.TextChangedEventHandler;
                return;
            }
            if (CheckingKeywords(value))
            {
                r.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
            }
            else
            {
                r.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
            }

            textBoxQuery.TextChanged += this.TextChangedEventHandler;

        }

        private bool CheckingKeywords(string value)
        {

            foreach (string key in keywords)
            {
                if (value.ToLower().CompareTo(key) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}