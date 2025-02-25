
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeDetails));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.dataGridEmployeeList = new System.Windows.Forms.DataGridView();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtEmployeePhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.checkBoxsitemanager = new System.Windows.Forms.CheckBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtEmployeeID = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnNewEmployee = new System.Windows.Forms.Button();
            this.btnSaveNewEmployee = new System.Windows.Forms.Button();
            this.btnEditEmployee = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxRoles = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployeeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Control;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtUsername.Location = new System.Drawing.Point(12, 166);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(244, 27);
            this.txtUsername.TabIndex = 0;
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
            // txtEmployeeName
            // 
            this.txtEmployeeName.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeeName.Location = new System.Drawing.Point(299, 111);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(261, 27);
            this.txtEmployeeName.TabIndex = 2;
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
            this.comboEmployeeDepartment.SelectedIndexChanged += new System.EventHandler(this.comboEmployeeDepartment_SelectedIndexChanged);
            // 
            // checkBoxAdmin
            // 
            this.checkBoxAdmin.AutoSize = true;
            this.checkBoxAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAdmin.Location = new System.Drawing.Point(425, 221);
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
            // 
            // lblnamelogin
            // 
            this.lblnamelogin.AutoSize = true;
            this.lblnamelogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnamelogin.Location = new System.Drawing.Point(12, 143);
            this.lblnamelogin.Name = "lblnamelogin";
            this.lblnamelogin.Size = new System.Drawing.Size(109, 20);
            this.lblnamelogin.TabIndex = 9;
            this.lblnamelogin.Text = "LogIn Name";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(295, 88);
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
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmail.Location = new System.Drawing.Point(295, 165);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(262, 27);
            this.txtEmail.TabIndex = 13;
            // 
            // txtEmployeePhone
            // 
            this.txtEmployeePhone.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeePhone.Location = new System.Drawing.Point(616, 170);
            this.txtEmployeePhone.Name = "txtEmployeePhone";
            this.txtEmployeePhone.Size = new System.Drawing.Size(233, 27);
            this.txtEmployeePhone.TabIndex = 14;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(295, 142);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(56, 20);
            this.lblEmail.TabIndex = 15;
            this.lblEmail.Text = "Email";
            // 
            // checkBoxsitemanager
            // 
            this.checkBoxsitemanager.AutoSize = true;
            this.checkBoxsitemanager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxsitemanager.Location = new System.Drawing.Point(277, 221);
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
            this.lblID.Location = new System.Drawing.Point(8, 111);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(34, 20);
            this.lblID.TabIndex = 17;
            this.lblID.Text = "ID:";
            // 
            // txtEmployeeID
            // 
            this.txtEmployeeID.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtEmployeeID.Location = new System.Drawing.Point(49, 108);
            this.txtEmployeeID.Name = "txtEmployeeID";
            this.txtEmployeeID.Size = new System.Drawing.Size(54, 27);
            this.txtEmployeeID.TabIndex = 18;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(612, 147);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(61, 20);
            this.lblPhone.TabIndex = 19;
            this.lblPhone.Text = "Phone";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsActive.Location = new System.Drawing.Point(514, 221);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(83, 24);
            this.chkIsActive.TabIndex = 20;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
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
            // btnEditEmployee
            // 
            this.btnEditEmployee.BackColor = System.Drawing.Color.LightCoral;
            this.btnEditEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditEmployee.ForeColor = System.Drawing.Color.Black;
            this.btnEditEmployee.Location = new System.Drawing.Point(346, 304);
            this.btnEditEmployee.Name = "btnEditEmployee";
            this.btnEditEmployee.Size = new System.Drawing.Size(83, 31);
            this.btnEditEmployee.TabIndex = 23;
            this.btnEditEmployee.Text = "Edit";
            this.btnEditEmployee.UseVisualStyleBackColor = false;
            this.btnEditEmployee.Click += new System.EventHandler(this.btnEditEmployee_Click);
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
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtPassword.Location = new System.Drawing.Point(8, 221);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(244, 27);
            this.txtPassword.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(871, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "Hire Date";
            // 
            // dtpHireDate
            // 
            this.dtpHireDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.dtpHireDate.Location = new System.Drawing.Point(876, 226);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(271, 27);
            this.dtpHireDate.TabIndex = 30;
            // 
            // comboBoxRoles
            // 
            this.comboBoxRoles.FormattingEnabled = true;
            this.comboBoxRoles.Location = new System.Drawing.Point(875, 165);
            this.comboBoxRoles.Name = "comboBoxRoles";
            this.comboBoxRoles.Size = new System.Drawing.Size(272, 24);
            this.comboBoxRoles.TabIndex = 31;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(872, 143);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(47, 20);
            this.lblRole.TabIndex = 32;
            this.lblRole.Text = "Role";
            // 
            // frmEmployeeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1300, 765);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.comboBoxRoles);
            this.Controls.Add(this.dtpHireDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridEmployeeList);
            this.Controls.Add(this.btnEditEmployee);
            this.Controls.Add(this.btnSaveNewEmployee);
            this.Controls.Add(this.btnNewEmployee);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtEmployeeID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.checkBoxsitemanager);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmployeePhone);
            this.Controls.Add(this.txtEmail);
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
            this.Controls.Add(this.txtEmployeeName);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEmployeeDetails";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.frmEmployeeDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployeeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.DataGridView dataGridEmployeeList;
        private System.Windows.Forms.TextBox txtEmployeeName;
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
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtEmployeePhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.CheckBox checkBoxsitemanager;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtEmployeeID;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnNewEmployee;
        private System.Windows.Forms.Button btnSaveNewEmployee;
        private System.Windows.Forms.Button btnEditEmployee;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.ComboBox comboBoxRoles;
        private System.Windows.Forms.Label lblRole;
    }
}