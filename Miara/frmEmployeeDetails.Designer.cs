
namespace Miara
{
    partial class frmEmployeeDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtlogInEmployee = new System.Windows.Forms.TextBox();
            this.dataGridEmployeeList = new System.Windows.Forms.DataGridView();
            this.txtEmployeeFirstname = new System.Windows.Forms.TextBox();
            this.checkBoxActiveEmployee = new System.Windows.Forms.CheckBox();
            this.checkBoxAllemployees = new System.Windows.Forms.CheckBox();
            this.txtEmployeeSurname = new System.Windows.Forms.TextBox();
            this.comboEmployeeDepartment = new System.Windows.Forms.ComboBox();
            this.checkBoxAdmin = new System.Windows.Forms.CheckBox();
            this.checkBoxInActiveEmployees = new System.Windows.Forms.CheckBox();
            this.lblnamelogin = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.Department = new System.Windows.Forms.Label();
            this.txtEmployeeEmail = new System.Windows.Forms.TextBox();
            this.txtEmployeePhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.checkBoxsitemanager = new System.Windows.Forms.CheckBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtEmployeeID = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.btnNewEmployee = new System.Windows.Forms.Button();
            this.btnSaveNewEmployee = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployeeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtlogInEmployee
            // 
            this.txtlogInEmployee.BackColor = System.Drawing.SystemColors.Control;
            this.txtlogInEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtlogInEmployee.Location = new System.Drawing.Point(22, 216);
            this.txtlogInEmployee.Name = "txtlogInEmployee";
            this.txtlogInEmployee.Size = new System.Drawing.Size(244, 27);
            this.txtlogInEmployee.TabIndex = 0;
            // 
            // dataGridEmployeeList
            // 
            this.dataGridEmployeeList.AllowUserToAddRows = false;
            this.dataGridEmployeeList.AllowUserToDeleteRows = false;
            this.dataGridEmployeeList.AllowUserToOrderColumns = true;
            this.dataGridEmployeeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridEmployeeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridEmployeeList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridEmployeeList.BackgroundColor = System.Drawing.Color.LightCoral;
            this.dataGridEmployeeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmployeeList.Location = new System.Drawing.Point(12, 406);
            this.dataGridEmployeeList.MultiSelect = false;
            this.dataGridEmployeeList.Name = "dataGridEmployeeList";
            this.dataGridEmployeeList.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridEmployeeList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridEmployeeList.RowTemplate.Height = 24;
            this.dataGridEmployeeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEmployeeList.Size = new System.Drawing.Size(1276, 359);
            this.dataGridEmployeeList.TabIndex = 1;
            // 
            // txtEmployeeFirstname
            // 
            this.txtEmployeeFirstname.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeFirstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeeFirstname.Location = new System.Drawing.Point(306, 111);
            this.txtEmployeeFirstname.Name = "txtEmployeeFirstname";
            this.txtEmployeeFirstname.Size = new System.Drawing.Size(261, 27);
            this.txtEmployeeFirstname.TabIndex = 2;
            // 
            // checkBoxActiveEmployee
            // 
            this.checkBoxActiveEmployee.AutoSize = true;
            this.checkBoxActiveEmployee.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxActiveEmployee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBoxActiveEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxActiveEmployee.Location = new System.Drawing.Point(99, 377);
            this.checkBoxActiveEmployee.Name = "checkBoxActiveEmployee";
            this.checkBoxActiveEmployee.Size = new System.Drawing.Size(97, 21);
            this.checkBoxActiveEmployee.TabIndex = 3;
            this.checkBoxActiveEmployee.Text = "Active Only";
            this.checkBoxActiveEmployee.UseVisualStyleBackColor = false;
            this.checkBoxActiveEmployee.CheckedChanged += new System.EventHandler(this.checkBoxActiveEmployee_CheckedChanged);
            // 
            // checkBoxAllemployees
            // 
            this.checkBoxAllemployees.AutoSize = true;
            this.checkBoxAllemployees.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAllemployees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBoxAllemployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAllemployees.Location = new System.Drawing.Point(18, 377);
            this.checkBoxAllemployees.Name = "checkBoxAllemployees";
            this.checkBoxAllemployees.Size = new System.Drawing.Size(41, 21);
            this.checkBoxAllemployees.TabIndex = 4;
            this.checkBoxAllemployees.Text = "All";
            this.checkBoxAllemployees.UseVisualStyleBackColor = false;
            this.checkBoxAllemployees.CheckedChanged += new System.EventHandler(this.checkBoxAllemployees_CheckedChanged);
            // 
            // txtEmployeeSurname
            // 
            this.txtEmployeeSurname.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeeSurname.Location = new System.Drawing.Point(616, 117);
            this.txtEmployeeSurname.Name = "txtEmployeeSurname";
            this.txtEmployeeSurname.Size = new System.Drawing.Size(228, 27);
            this.txtEmployeeSurname.TabIndex = 5;
            // 
            // comboEmployeeDepartment
            // 
            this.comboEmployeeDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboEmployeeDepartment.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboEmployeeDepartment.ForeColor = System.Drawing.Color.LightCoral;
            this.comboEmployeeDepartment.FormattingEnabled = true;
            this.comboEmployeeDepartment.Location = new System.Drawing.Point(875, 114);
            this.comboEmployeeDepartment.Name = "comboEmployeeDepartment";
            this.comboEmployeeDepartment.Size = new System.Drawing.Size(272, 24);
            this.comboEmployeeDepartment.TabIndex = 6;
            // 
            // checkBoxAdmin
            // 
            this.checkBoxAdmin.AutoSize = true;
            this.checkBoxAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAdmin.Location = new System.Drawing.Point(1022, 224);
            this.checkBoxAdmin.Name = "checkBoxAdmin";
            this.checkBoxAdmin.Size = new System.Drawing.Size(83, 24);
            this.checkBoxAdmin.TabIndex = 7;
            this.checkBoxAdmin.Text = "Admin";
            this.checkBoxAdmin.UseVisualStyleBackColor = true;
            // 
            // checkBoxInActiveEmployees
            // 
            this.checkBoxInActiveEmployees.AutoSize = true;
            this.checkBoxInActiveEmployees.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxInActiveEmployees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBoxInActiveEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxInActiveEmployees.Location = new System.Drawing.Point(229, 377);
            this.checkBoxInActiveEmployees.Name = "checkBoxInActiveEmployees";
            this.checkBoxInActiveEmployees.Size = new System.Drawing.Size(74, 21);
            this.checkBoxInActiveEmployees.TabIndex = 8;
            this.checkBoxInActiveEmployees.Text = "Inactive";
            this.checkBoxInActiveEmployees.UseVisualStyleBackColor = false;
            this.checkBoxInActiveEmployees.CheckedChanged += new System.EventHandler(this.checkBoxInActiveEmployees_CheckedChanged);
            // 
            // lblnamelogin
            // 
            this.lblnamelogin.AutoSize = true;
            this.lblnamelogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnamelogin.Location = new System.Drawing.Point(22, 193);
            this.lblnamelogin.Name = "lblnamelogin";
            this.lblnamelogin.Size = new System.Drawing.Size(109, 20);
            this.lblnamelogin.TabIndex = 9;
            this.lblnamelogin.Text = "LogIn Name";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(303, 88);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(102, 20);
            this.lblFirstName.TabIndex = 10;
            this.lblFirstName.Text = "First Name";
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurname.Location = new System.Drawing.Point(612, 90);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(83, 20);
            this.lblSurname.TabIndex = 11;
            this.lblSurname.Text = "Surname";
            // 
            // Department
            // 
            this.Department.AutoSize = true;
            this.Department.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Department.Location = new System.Drawing.Point(872, 91);
            this.Department.Name = "Department";
            this.Department.Size = new System.Drawing.Size(107, 20);
            this.Department.TabIndex = 12;
            this.Department.Text = "Department";
            // 
            // txtEmployeeEmail
            // 
            this.txtEmployeeEmail.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeeEmail.Location = new System.Drawing.Point(305, 215);
            this.txtEmployeeEmail.Name = "txtEmployeeEmail";
            this.txtEmployeeEmail.Size = new System.Drawing.Size(262, 27);
            this.txtEmployeeEmail.TabIndex = 13;
            // 
            // txtEmployeePhone
            // 
            this.txtEmployeePhone.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeePhone.Location = new System.Drawing.Point(611, 217);
            this.txtEmployeePhone.Name = "txtEmployeePhone";
            this.txtEmployeePhone.Size = new System.Drawing.Size(233, 27);
            this.txtEmployeePhone.TabIndex = 14;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(305, 192);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(56, 20);
            this.lblEmail.TabIndex = 15;
            this.lblEmail.Text = "Email";
            // 
            // checkBoxsitemanager
            // 
            this.checkBoxsitemanager.AutoSize = true;
            this.checkBoxsitemanager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxsitemanager.Location = new System.Drawing.Point(874, 224);
            this.checkBoxsitemanager.Name = "checkBoxsitemanager";
            this.checkBoxsitemanager.Size = new System.Drawing.Size(142, 24);
            this.checkBoxsitemanager.TabIndex = 16;
            this.checkBoxsitemanager.Text = "Site Manager";
            this.checkBoxsitemanager.UseVisualStyleBackColor = true;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(25, 110);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(34, 20);
            this.lblID.TabIndex = 17;
            this.lblID.Text = "ID:";
            // 
            // txtEmployeeID
            // 
            this.txtEmployeeID.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeeID.Location = new System.Drawing.Point(61, 107);
            this.txtEmployeeID.Name = "txtEmployeeID";
            this.txtEmployeeID.Size = new System.Drawing.Size(54, 27);
            this.txtEmployeeID.TabIndex = 18;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(611, 197);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(61, 20);
            this.lblPhone.TabIndex = 19;
            this.lblPhone.Text = "Phone";
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxActive.Location = new System.Drawing.Point(1116, 224);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(83, 24);
            this.checkBoxActive.TabIndex = 20;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // btnNewEmployee
            // 
            this.btnNewEmployee.BackColor = System.Drawing.Color.LightCoral;
            this.btnNewEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewEmployee.ForeColor = System.Drawing.Color.Black;
            this.btnNewEmployee.Location = new System.Drawing.Point(179, 304);
            this.btnNewEmployee.Name = "btnNewEmployee";
            this.btnNewEmployee.Size = new System.Drawing.Size(73, 31);
            this.btnNewEmployee.TabIndex = 21;
            this.btnNewEmployee.Text = "New";
            this.btnNewEmployee.UseVisualStyleBackColor = false;
            this.btnNewEmployee.Click += new System.EventHandler(this.btnNewEmployee_Click);
            // 
            // btnSaveNewEmployee
            // 
            this.btnSaveNewEmployee.BackColor = System.Drawing.Color.LightCoral;
            this.btnSaveNewEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveNewEmployee.ForeColor = System.Drawing.Color.Black;
            this.btnSaveNewEmployee.Location = new System.Drawing.Point(18, 304);
            this.btnSaveNewEmployee.Name = "btnSaveNewEmployee";
            this.btnSaveNewEmployee.Size = new System.Drawing.Size(85, 31);
            this.btnSaveNewEmployee.TabIndex = 22;
            this.btnSaveNewEmployee.Text = "Save";
            this.btnSaveNewEmployee.UseVisualStyleBackColor = false;
            this.btnSaveNewEmployee.Click += new System.EventHandler(this.btnSaveNewEmployee_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.BackColor = System.Drawing.Color.LightCoral;
            this.btnEditItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditItem.ForeColor = System.Drawing.Color.Black;
            this.btnEditItem.Location = new System.Drawing.Point(346, 304);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(83, 31);
            this.btnEditItem.TabIndex = 23;
            this.btnEditItem.Text = "Edit";
            this.btnEditItem.UseVisualStyleBackColor = false;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightCoral;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 368);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1300, 397);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.IndianRed;
            this.label1.Location = new System.Drawing.Point(369, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(444, 47);
            this.label1.TabIndex = 25;
            this.label1.Text = "Employee Maintenance";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightCoral;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1188, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 44);
            this.button1.TabIndex = 26;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmEmployeeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1300, 765);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridEmployeeList);
            this.Controls.Add(this.btnEditItem);
            this.Controls.Add(this.btnSaveNewEmployee);
            this.Controls.Add(this.btnNewEmployee);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtEmployeeID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.checkBoxsitemanager);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmployeePhone);
            this.Controls.Add(this.txtEmployeeEmail);
            this.Controls.Add(this.Department);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblnamelogin);
            this.Controls.Add(this.checkBoxInActiveEmployees);
            this.Controls.Add(this.checkBoxAdmin);
            this.Controls.Add(this.comboEmployeeDepartment);
            this.Controls.Add(this.txtEmployeeSurname);
            this.Controls.Add(this.checkBoxAllemployees);
            this.Controls.Add(this.checkBoxActiveEmployee);
            this.Controls.Add(this.txtEmployeeFirstname);
            this.Controls.Add(this.txtlogInEmployee);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmEmployeeDetails";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployeeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtlogInEmployee;
        private System.Windows.Forms.DataGridView dataGridEmployeeList;
        private System.Windows.Forms.TextBox txtEmployeeFirstname;
        private System.Windows.Forms.CheckBox checkBoxActiveEmployee;
        private System.Windows.Forms.CheckBox checkBoxAllemployees;
        private System.Windows.Forms.TextBox txtEmployeeSurname;
        private System.Windows.Forms.ComboBox comboEmployeeDepartment;
        private System.Windows.Forms.CheckBox checkBoxAdmin;
        private System.Windows.Forms.CheckBox checkBoxInActiveEmployees;
        private System.Windows.Forms.Label lblnamelogin;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Label Department;
        private System.Windows.Forms.TextBox txtEmployeeEmail;
        private System.Windows.Forms.TextBox txtEmployeePhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.CheckBox checkBoxsitemanager;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtEmployeeID;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Button btnNewEmployee;
        private System.Windows.Forms.Button btnSaveNewEmployee;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}