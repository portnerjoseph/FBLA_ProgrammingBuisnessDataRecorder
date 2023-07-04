using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FBLA_project
{
    public partial class Form1 : Form
    {
        public Panel current, last;
        TaskCollection taskCollection;
        EmployeeCollection employeeCollection;
        CustomerCollection customerCollection;
        AutoCompleteStringCollection customersAuto, employeesAuto, tasksAuto;
        Bitmap img;
        public Form1()
        {
            InitializeComponent();
            current =panel_main_menu;
             if (!File.Exists("task.xml"))
             {
                 taskCollection = new TaskCollection();
                 taskCollection.save("task.xml");
             }
             else
                 taskCollection = (TaskCollection)load("task.xml",typeof(TaskCollection));
             if (!File.Exists("employee.xml"))
             {
                 employeeCollection = new EmployeeCollection();
                 employeeCollection.save("employee.xml");
             }
             else
                 employeeCollection = (EmployeeCollection)load("employee.xml", typeof(EmployeeCollection));
             if (!File.Exists("customer.xml"))
             {
                 customerCollection = new CustomerCollection();
                 customerCollection.save("customer.xml");
             }
             else
                 customerCollection = (CustomerCollection)load("customer.xml", typeof(CustomerCollection));
                

            customersAuto = new AutoCompleteStringCollection();
            employeesAuto = new AutoCompleteStringCollection();
            tasksAuto = new AutoCompleteStringCollection();
            fillCustomersAuto();
            fillEmployeesAuto();
            fillTasksAuto();
            txt_custName.AutoCompleteCustomSource = customersAuto;
            txt_EmployeeName.AutoCompleteCustomSource = employeesAuto;
            txt_TaskName.AutoCompleteCustomSource = tasksAuto;
            txt_employeeNameSched.AutoCompleteCustomSource = employeesAuto;
            txt_TaskAdd.AutoCompleteCustomSource = tasksAuto;


 
        }
        /// <summary>
        /// Fills customerName collection
        /// </summary>
        public void fillCustomersAuto()
        {
            customersAuto.Clear();
            for (int x = 0; x < customerCollection.Customers.Count; x++)
                customersAuto.Add(customerCollection.Customers[x].FirstName + " " + customerCollection.Customers[x].LastName);
        }
        /// <summary>
        /// fills employee name collection
        /// </summary>
        public void fillEmployeesAuto()
        {
            employeesAuto.Clear();
            for (int x = 0; x < employeeCollection.EmployeeSchedules.Count; x++)
                employeesAuto.Add(employeeCollection.EmployeeSchedules[x].Employee.FirstName + " " + employeeCollection.EmployeeSchedules[x].Employee.LastName);
        }
        /// <summary>
        /// fills tasks name collection
        /// </summary>
        public void fillTasksAuto()
        {
            tasksAuto.Clear();
            for (int x = 0; x < taskCollection.Tasks.Count; x++)
                tasksAuto.Add(taskCollection.Tasks[x].Name);
        }
        /// <summary>
        /// updates label for tasks
        /// </summary>
        /// <param name="day"></param>
        public void updateTaskNameSource(DaySchedule day)
        {
            lbl_Tasks.Text = "Tasks:\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
                for (int x = 0; x < day.Tasks.Count; x++)
               lbl_Tasks.Text += day.Tasks[x].Name+"\n\n";

        }
        /// <summary>
        /// saves all xml files
        /// </summary>
        public void saveAll()
        {
            taskCollection.save("task.xml");
            employeeCollection.save("employee.xml");
            customerCollection.save("customer.xml");

        }
       
       /// <summary>
       /// navigates to the main menu
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            navPanel(panel_main_menu);
            this.WindowState = FormWindowState.Normal;
        }
        /// <summary>
        /// loads xml file into object
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object load(string FileName,Type type)
        {
            using (var stream = System.IO.File.OpenRead(FileName))
            {
                var serializer = new XmlSerializer(type);
                return Convert.ChangeType(serializer.Deserialize(stream),type);
            }
        }
        /// <summary>
        /// navigates to employee management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_employee_management_Click(object sender, EventArgs e)
        {
            navPanel(panel_employee_management);
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// Rotates between panels by setting visability
        /// 
        /// </summary>
        /// <param name="current">current panel</param>
        /// <param name="next">panel to move to</param>
        private void navPanel(Panel next)
        {
            current.Visible = false;
            next.Visible = true;
            if (current.Parent is Panel)
                current.Parent.Visible = false;
            if (next.Parent is Panel)
                next.Parent.Visible = true;
            last = current;
            current = next;

        }
        /// <summary>
        /// navigates to employee management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            navPanel(panel_employee_management);
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// navigates to customer management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_customer_management_Click(object sender, EventArgs e)
        {
            navPanel(panel_customer_management);
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// Handles watermark on custName txtbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_custName_Enter(object sender, EventArgs e)
        {
            if (txt_custName.Text == "First Last")
            {
                txt_custName.Text = "";
                txt_custName.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// Handles watermark on custName txtbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_custName_Leave(object sender, EventArgs e)
        {
            if (txt_custName.Text == "")
            {
                txt_custName.Text = "First Last";
                txt_custName.ForeColor = Color.Gray;
            }
        }
        /// <summary>
        /// adds or updates customer info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            String[] temp = txt_custName.Text.Split(' ');
            if (temp.Length == 2)
            {
                Customer cust = new Customer(temp[0], temp[1]);
                if (customerCollection.Customers.Contains(cust))
                {
                    cust = customerCollection.Customers[customerCollection.Customers.IndexOf(cust)];
                    cust.Phone = txt_custPhone.Text;
                    if (checkBox_currentDateTime.Checked)
                        cust.Times.Add(DateTime.Now);
                    else
                        cust.Times.Add(dateTimePicker_cust.Value.Date);
                    cust.Email = txt_custEmail.Text;
                }
                else
                    customerCollection.Customers.Add(new Customer(checkBox_currentDateTime.Checked ? DateTime.Now : dateTimePicker_cust.Value.Date, temp[0], temp[1], txt_custEmail.Text, txt_custPhone.Text));
                fillCustomersAuto();
                txt_custPhone.Text = "";
                txt_custEmail.Text = "";
                txt_custName.Text = "";
                listBox_dates.DataSource = new List<String>();
            }
            else
                messageBox("Name input incorrect");
           
        }

        /// <summary>
        /// updates form inputs while customer name is typed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_custName_TextChanged(object sender, EventArgs e)
        {
            String[] temp = txt_custName.Text.Split(' ');
            if(temp.Length == 2)
            {
                Customer cust = new Customer(temp[0], temp[1]);
                if (customerCollection.Customers.Contains(cust))
                {
                    cust = customerCollection.Customers[customerCollection.Customers.IndexOf(cust)];
                    listBox_dates.DataSource = cust.Times;
                    txt_custEmail.Text = cust.Email;
                    txt_custPhone.Text = cust.Phone;

                }
            }
            else
            {
                txt_custPhone.Text = "";
                txt_custEmail.Text = "";
                listBox_dates.DataSource = new List<String>();
            }
        }
        /// <summary>
        /// saves all files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem14_Click(object sender, EventArgs e)
        {
            saveAll();
        }
        /// <summary>
        /// prompts user with dialogbox
        /// </summary>
        /// <param name="message"></param>
        private void messageBox(string message)
        {
            DialogResult result2 = MessageBox.Show(message,
            "",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }
        /// <summary>
        /// prompts user to save on exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result2 = MessageBox.Show("Do you want to save your work?",
            "Closing Program",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question);
            if (result2.Equals(DialogResult.Yes))
                saveAll();
            if (result2.Equals(DialogResult.Cancel))
                e.Cancel = true;

        }
        /// <summary>
        /// removes customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_removeCust_Click(object sender, EventArgs e)
        {
            String[] temp = txt_custName.Text.Split(' ');
            if (temp.Length == 2)
            {
                Customer cust = new Customer(temp[0], temp[1]);
                if (customerCollection.Customers.Contains(cust))
                {
                    customerCollection.Customers.Remove(cust);
                    txt_custPhone.Text = "";
                    txt_custEmail.Text = "";
                    listBox_dates.DataSource = new List<String>();
                    txt_custName.Text = "";
                    fillCustomersAuto();
                }
                else
                    messageBox("There is nobody with that name");
            }
            else
                messageBox("Enter valid input, example:(First Last)");
        }

     
        /// <summary>
        /// switches between current dateTime and custom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_currentDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_cust.Visible = !checkBox_currentDateTime.Checked;
        }

      
        /// <summary>
        /// exits program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// navigates to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem12_Click(object sender, EventArgs e)
        {
            navPanel(panel_main_menu);
            this.WindowState = FormWindowState.Normal;
        }
        /// <summary>
        /// updates and adds employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateEmployee_Click(object sender, EventArgs e)
        {
            String[] temp = txt_EmployeeName.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl = new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    empl = employeeCollection.EmployeeSchedules[employeeCollection.EmployeeSchedules.IndexOf(empl)];
                    empl.Employee.Phone = txt_PhoneEmployee.Text;
                    empl.Employee.Position = txt_EmployeePosition.Text;
                    empl.Employee.Email = txt_EmailEmployee.Text;
                }
                else
                    employeeCollection.EmployeeSchedules.Add(new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1], txt_EmailEmployee.Text, txt_PhoneEmployee.Text, txt_EmployeePosition.Text)));
                fillEmployeesAuto();
                txt_EmailEmployee.Text = "";
                txt_EmployeePosition.Text = "";
                txt_PhoneEmployee.Text = "";
                txt_EmployeeName.Text = "";
                lbl_Tasks.Text = "";
            }
            else
                messageBox("Name input incorrect");
        }
        /// <summary>
        /// handles watermark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_EmployeeName_Enter(object sender, EventArgs e)
        {
            if (txt_EmployeeName.Text == "First Last")
            {
                txt_EmployeeName.Text = "";
                txt_EmployeeName.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// handles watermark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_EmployeeName_Leave(object sender, EventArgs e)
        {
            if (txt_EmployeeName.Text == "")
            {
                txt_EmployeeName.Text = "First Last";
                txt_EmployeeName.ForeColor = Color.Gray;
            }
        }
        /// <summary>
        /// removes employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_removeEmployee_Click(object sender, EventArgs e)
        {
            String[] temp = txt_EmployeeName.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl =new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    employeeCollection.EmployeeSchedules.Remove(empl);
                    txt_EmployeeName.Text = "";
                    txt_EmailEmployee.Text = "";
                    txt_EmployeePosition.Text = "";
                    txt_PhoneEmployee.Text = "";
                    fillEmployeesAuto();
                }
                else
                    messageBox("There is nobody with that name");
            }
            else
                messageBox("Enter valid input, example:(First Last)");
        }
        /// <summary>
        /// updates tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TaskName_TextChanged(object sender, EventArgs e)
        {
           
                Task task =new Task(txt_TaskName.Text,"");
            if (taskCollection.Tasks.Contains(task))
            {
                task = taskCollection.Tasks[taskCollection.Tasks.IndexOf(task)];
                txt_TaskDesc.Text = task.Description;

            }
            else
                txt_TaskDesc.Text = "";
        }
        /// <summary>
        /// removes tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_removeTask_Click(object sender, EventArgs e)
        {
            Task task = new Task(txt_TaskName.Text, "");
            if (taskCollection.Tasks.Contains(task))
            {
                taskCollection.Tasks.Remove(task);
                txt_TaskDesc.Text = "";
                txt_TaskName.Text="";
                fillTasksAuto();
            }
            else
                messageBox("There is no task with that name.");
        }
        /// <summary>
        /// handles watermark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_employeeNameSched_Enter(object sender, EventArgs e)
        {
            if (txt_employeeNameSched.Text == "First Last")
            {
                txt_employeeNameSched.Text = "";
                txt_employeeNameSched.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// handles watermark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_employeeNameSched_Leave(object sender, EventArgs e)
        {
            if (txt_employeeNameSched.Text == "")
            {
                txt_employeeNameSched.Text = "First Last";
                txt_employeeNameSched.ForeColor = Color.Gray;
            }
        }
        /// <summary>
        /// removes tasks from employees day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_removeEmployeeSchedule_Click(object sender, EventArgs e)
        {
            String[] temp = txt_employeeNameSched.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl = new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    empl = employeeCollection.EmployeeSchedules[employeeCollection.EmployeeSchedules.IndexOf(empl)];
                    DaySchedule tempDay = new DaySchedule((DayOfWeek)(listBox_weekday.SelectedIndex == -1 ? 0 : listBox_weekday.SelectedIndex));
                    if (empl.Schedule.Contains(tempDay))
                    {
                        tempDay = empl.Schedule[empl.Schedule.IndexOf(tempDay)];
                        tempDay.Tasks.Clear();
                        updateTaskNameSource(tempDay);

                    }
                }
                else
                    messageBox("Nobody exists with that name");
            }
            else
            messageBox("Name input incorrect");
        }
        /// <summary>
        /// updates employee schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateEmployeeSched_Click(object sender, EventArgs e)
        {
            String[] temp = txt_employeeNameSched.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl = new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    empl = employeeCollection.EmployeeSchedules[employeeCollection.EmployeeSchedules.IndexOf(empl)];
                    DaySchedule tempDay = new DaySchedule((DayOfWeek)(listBox_weekday.SelectedIndex==-1? 0: listBox_weekday.SelectedIndex));
                    if (!empl.Schedule.Contains(tempDay))
                    {

                        empl.Schedule.Add(tempDay);

                    }
                    else
                    {
                        tempDay = empl.Schedule[empl.Schedule.IndexOf(tempDay)];
                    }
                    Task tempTask = new Task(txt_TaskAdd.Text, "");
                    if (tempTask.Name.Trim() != "")
                    {
                        if (taskCollection.Tasks.Contains(tempTask))
                        {
                            tempTask = taskCollection.Tasks[taskCollection.Tasks.IndexOf(tempTask)];
                            if (!tempDay.Tasks.Contains(tempTask))
                            {
                                tempDay.Tasks.Add(tempTask);
                                updateTaskNameSource(tempDay);
                            }
                            else
                                messageBox("Task already exists for that day and was not entered");
                        }
                        else
                            messageBox("Task doesnt exist and wasn't added to day (use Enter Task pane)");
                    }
                    tempDay.ShiftStart = dateTimePicker_StartHours.Value;
                    tempDay.ShiftEnd = dateTimePicker_EndHours.Value;
                }
                else
                messageBox("That name doesnt exist");

            }
            else
                messageBox("Name input incorrect");
        }
        /// <summary>
        /// updates on employee name entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_employeeNameSched_TextChanged(object sender, EventArgs e)
        {
            String[] temp = txt_employeeNameSched.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl = new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    empl = employeeCollection.EmployeeSchedules[employeeCollection.EmployeeSchedules.IndexOf(empl)];
                    DaySchedule tempDay = new DaySchedule((DayOfWeek)(listBox_weekday.SelectedIndex == -1 ? 0 : listBox_weekday.SelectedIndex));
                    if (empl.Schedule.Contains(tempDay))
                    {

                        tempDay = empl.Schedule[empl.Schedule.IndexOf(new DaySchedule((DayOfWeek)(listBox_weekday.SelectedIndex == -1 ? 0 : listBox_weekday.SelectedIndex)))];
                        updateTaskNameSource(tempDay);
                        dateTimePicker_StartHours.Value = tempDay.ShiftStart;
                        dateTimePicker_EndHours.Value = tempDay.ShiftEnd;
                    }
                   
                    

                }
            }
            else
            {
                
                txt_TaskAdd.Text = "";
                lbl_Tasks.Text = "";
            }
        }
        /// <summary>
        /// updates on weekday change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_weekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] temp = txt_employeeNameSched.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl = new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    empl = employeeCollection.EmployeeSchedules[employeeCollection.EmployeeSchedules.IndexOf(empl)];
                    DaySchedule tempDay = new DaySchedule((DayOfWeek)(listBox_weekday.SelectedIndex == -1 ? 0 : listBox_weekday.SelectedIndex));
                    if (empl.Schedule.Contains(tempDay))
                    {

                        tempDay = empl.Schedule[empl.Schedule.IndexOf(new DaySchedule((DayOfWeek)(listBox_weekday.SelectedIndex == -1 ? 0 : listBox_weekday.SelectedIndex)))];
                        updateTaskNameSource(tempDay);
                        dateTimePicker_StartHours.Value = tempDay.ShiftStart;
                        dateTimePicker_EndHours.Value = tempDay.ShiftEnd;
                    }



                }
            }
            else
            {

                txt_TaskAdd.Text = "";
                lbl_Tasks.Text = "";
            }
        }
        /// <summary>
        ///outputs employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OutputEmployee_Click(object sender, EventArgs e)
        {
            lbl_EmployeeTable.Text = "";
            lbl_EmployeeTable.Text += new Employee("FirstName", "LastName", "Email", "Phone", "Position").ToString();
            lbl_EmployeeTable.Text += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            for (int x=0;x<employeeCollection.EmployeeSchedules.Count;x++)
            {
                lbl_EmployeeTable.Text += "\r\n"+employeeCollection.EmployeeSchedules[x].Employee.ToString()+"\r\n";
            }
        }
        /// <summary>
        /// outputs tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OutputTasks_Click(object sender, EventArgs e)
        {
            lbl_EmployeeTable.Text = "";
            lbl_EmployeeTable.Text += taskCollection.ToString();
        }
        /// <summary>
        /// outputs work schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OutputWorkSched_Click(object sender, EventArgs e)
        {
            lbl_EmployeeTable.Text = "";
            for (int x=0;x<employeeCollection.EmployeeSchedules.Count;x++)
            {
                
                lbl_EmployeeTable.Text += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
                lbl_EmployeeTable.Text += employeeCollection.EmployeeSchedules[x].Employee.FirstName + " " + employeeCollection.EmployeeSchedules[x].Employee.LastName+"\r\n\r\n";
             
                lbl_EmployeeTable.Text += employeeCollection.EmployeeSchedules[x].ToString();
                lbl_EmployeeTable.Text += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
            }
        }
        /// <summary>
        /// exports xml files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem6_Click(object sender, EventArgs e)
        {
            string location = fileDialog();

            if (location != null && !location.Equals(""))
            {
                File.Copy("task.xml", location + "\\task.xml", true);
                File.Copy("customer.xml", location + "\\customer.xml", true);
                File.Copy("employee.xml", location + "\\employee.xml", true);
            }
           
                
        }
        /// <summary>
        /// opens file dialog
        /// </summary>
        /// <returns></returns>
        public string fileDialog()
           {
            using (var folderDialog = new FolderBrowserDialog())
             {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {

                    return folderDialog.SelectedPath;
                }
            }
           return null;
           }
        /// <summary>
        /// exports employee xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem7_Click(object sender, EventArgs e)
        {
            string location = fileDialog();
            if (location != null && !location.Equals(""))
            {
                File.Copy("task.xml", location + "\\task.xml", true);
                File.Copy("employee.xml", location + "\\employee.xml", true);
            }
        }
        /// <summary>
        /// exports customer xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem8_Click(object sender, EventArgs e)
        {

            string location = fileDialog();
            if (location != null && !location.Equals(""))
            {
                File.Copy("customer.xml", location + "\\customer.xml", true);
            }
        }
        /// <summary>
        /// opens print dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem4_Click(object sender, EventArgs e)
        {
            captureScreen();
           if (printDialog1.ShowDialog() == DialogResult.OK)
             {
                 printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                 printDocument1.Print();
                 printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
             }

        }


        /// <summary>
        /// prints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(img, 0, 0);
        }
        /// <summary>
        /// Saves a portion of the screen as a bitmap
        /// </summary>
        private void captureScreen()
        {
            Graphics graphic = this.CreateGraphics();
            Size s = this.Size;
            img = new Bitmap(s.Width, s.Height, graphic);
            Graphics memoryGraphics = Graphics.FromImage(img);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }
        /// <summary>
        /// opens print preview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem5_Click(object sender, EventArgs e)
        {
            captureScreen();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
        /// <summary>
        /// outputs all customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AllCustomers_Click(object sender, EventArgs e)
        {
            lbl_CustomerReport.Text = "";
            lbl_CustomerReport.Text += new Customer(new List<DateTime>(),"FirstName", "LastName", "Email", "Phone").ToString();
            lbl_CustomerReport.Text += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
            for (int x = 0; x < customerCollection.Customers.Count; x++)
                lbl_CustomerReport.Text += "\r\n" + customerCollection.Customers[x].ToString();
        }
        /// <summary>
        /// outputs customer attendence by weekday
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_attendenceByWeekday_Click(object sender, EventArgs e)
        {
            lbl_CustomerReport.Text = "";
            lbl_CustomerReport.Text += "Sunday: "+customerByDay(DayOfWeek.Sunday)+"\r\n\r\n";
            lbl_CustomerReport.Text += "Monday: " + customerByDay(DayOfWeek.Monday) + "\r\n\r\n";
            lbl_CustomerReport.Text += "Tuesday: " + customerByDay(DayOfWeek.Tuesday) + "\r\n\r\n";
            lbl_CustomerReport.Text += "Wednesday: " + customerByDay(DayOfWeek.Wednesday) + "\r\n\r\n";
            lbl_CustomerReport.Text += "Thursday: " + customerByDay(DayOfWeek.Thursday) + "\r\n\r\n";
            lbl_CustomerReport.Text += "Friday: " + customerByDay(DayOfWeek.Friday) + "\r\n\r\n";
            lbl_CustomerReport.Text += "Saturday: " + customerByDay(DayOfWeek.Saturday) + "\r\n\r\n";

        }
        /// <summary>
        /// finds customer attendence by day of week
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private int customerByDay(DayOfWeek d)
        {
            int count = 0;
            for(int x = 0; x < customerCollection.Customers.Count; x++)
            {
                for (int y = 0; y < customerCollection.Customers[x].Times.Count; y++)
                    count += customerCollection.Customers[x].Times[y].DayOfWeek == d ? 1 : 0;
            }
            return count;
        }
        /// <summary>
        /// finds number of customers for am pm
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        private int customerByAm(String am)
        {
            int count = 0;
            for (int x = 0; x < customerCollection.Customers.Count; x++)
            {
                for (int y = 0; y < customerCollection.Customers[x].Times.Count; y++)
                    count += customerCollection.Customers[x].Times[y].ToString("tt") == am ? 1 : 0;
            }
            return count;
        }
        /// <summary>
        /// outputs customer attendence by Am/Pm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AttendenceByAm_Click(object sender, EventArgs e)
        {
            lbl_CustomerReport.Text = "";
            lbl_CustomerReport.Text += "AM: " + customerByAm("AM")+"\r\n\r\n";
            lbl_CustomerReport.Text += "PM: " + customerByAm("PM");
        }

  

        /// <summary>
        /// updates or makes new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateTask_Click(object sender, EventArgs e)
        {
            Task task = new Task(txt_TaskName.Text, "");
            if (taskCollection.Tasks.Contains(task))
            {
                task = taskCollection.Tasks[taskCollection.Tasks.IndexOf(task)];
                task.Description = txt_TaskDesc.Text;
            }
            else
                taskCollection.Tasks.Add(new Task(txt_TaskName.Text, txt_TaskDesc.Text));
               fillTasksAuto();
            txt_TaskDesc.Text = "";
            txt_TaskName.Text = "";
        }
    
        /// <summary>
        /// navigates to customer managemnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            navPanel(panel_customer_management);
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// handles employee name change and updates form items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String[] temp = txt_EmployeeName.Text.Split(' ');
            if (temp.Length == 2)
            {
                EmployeeSchedule empl = new EmployeeSchedule(new List<DaySchedule>(), new Employee(temp[0], temp[1]));
                if (employeeCollection.EmployeeSchedules.Contains(empl))
                {
                    empl = employeeCollection.EmployeeSchedules[employeeCollection.EmployeeSchedules.IndexOf(empl)];
                    txt_EmailEmployee.Text = empl.Employee.Email;
                    txt_EmployeePosition.Text= empl.Employee.Position;
                    txt_PhoneEmployee.Text = empl.Employee.Phone;

                }
            }
            else
            {
                txt_EmailEmployee.Text ="";
                txt_EmployeePosition.Text = "";
                txt_PhoneEmployee.Text = "";
            }
        }
    }

       


    }

