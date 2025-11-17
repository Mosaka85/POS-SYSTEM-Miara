using System;
using System.Drawing;
using System.Windows.Forms;

namespace Miara
{
    partial class FormUserPermissions : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserPermissions));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.txtSearchUsers = new System.Windows.Forms.TextBox();
            this.BtnRefreshGrid = new System.Windows.Forms.Button();
            this.contextMenuUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelUserInput = new System.Windows.Forms.Panel();
            this.lblIDuser = new System.Windows.Forms.Label();
            this.btnEditUsers = new System.Windows.Forms.Button();
            this.CheckActiveUser = new System.Windows.Forms.CheckBox();
            this.comboRole = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboUserGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.tabGroups = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnAddGrp = new System.Windows.Forms.Button();
            this.dataGridInactiveGroups = new System.Windows.Forms.DataGridView();
            this.btnDeactivategrp = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.dataGridUserGroupsActive = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGroupDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.dataGridViewGroups = new System.Windows.Forms.DataGridView();
            this.toolStripGroups = new System.Windows.Forms.ToolStrip();
            this.btnAddGroup = new System.Windows.Forms.ToolStripButton();
            this.btnEditGroup = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteGroup = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshGroups = new System.Windows.Forms.ToolStripButton();
            this.tabRoles = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dataGridViewInactiveRoles = new System.Windows.Forms.DataGridView();
            this.btnActiveteRoles = new System.Windows.Forms.Button();
            this.btnDeactiveteRole = new System.Windows.Forms.Button();
            this.dataGridViewActiveRoles = new System.Windows.Forms.DataGridView();
            this.btnAddRoleActive = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRoleDesc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.dataGridViewRoles = new System.Windows.Forms.DataGridView();
            this.toolStripRoles = new System.Windows.Forms.ToolStrip();
            this.btnAddRole = new System.Windows.Forms.ToolStripButton();
            this.btnEditRole = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteRole = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshRoles = new System.Windows.Forms.ToolStripButton();
            this.tabPermissions = new System.Windows.Forms.TabPage();
            this.comboSelectGroup = new System.Windows.Forms.ComboBox();
            this.btnSavePermission = new System.Windows.Forms.Button();
            this.clbPermissions = new System.Windows.Forms.CheckedListBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboRoles = new System.Windows.Forms.ComboBox();
            this.btnAssignRoles = new System.Windows.Forms.Button();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridViewPermissions = new System.Windows.Forms.DataGridView();
            this.toolStripPermissions = new System.Windows.Forms.ToolStrip();
            this.btnAddPermission = new System.Windows.Forms.ToolStripButton();
            this.btnEditPermission = new System.Windows.Forms.ToolStripButton();
            this.btnDeletePermission = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshPermissions = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNewUserReg = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.panelUserInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.tabGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInactiveGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUserGroupsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).BeginInit();
            this.toolStripGroups.SuspendLayout();
            this.tabRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInactiveRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActiveRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoles)).BeginInit();
            this.toolStripRoles.SuspendLayout();
            this.tabPermissions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPermissions)).BeginInit();
            this.toolStripPermissions.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabUsers);
            this.tabControl1.Controls.Add(this.tabGroups);
            this.tabControl1.Controls.Add(this.tabRoles);
            this.tabControl1.Controls.Add(this.tabPermissions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1173, 574);
            this.tabControl1.TabIndex = 1;
            // 
            // tabUsers
            // 
            this.tabUsers.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabUsers.Controls.Add(this.txtSearchUsers);
            this.tabUsers.Controls.Add(this.BtnRefreshGrid);
            this.tabUsers.Controls.Add(this.panelUserInput);
            this.tabUsers.Controls.Add(this.dataGridViewUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 29);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(1165, 541);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Users";
            this.tabUsers.Click += new System.EventHandler(this.tabUsers_Click);
            // 
            // txtSearchUsers
            // 
            this.txtSearchUsers.Location = new System.Drawing.Point(580, 31);
            this.txtSearchUsers.Name = "txtSearchUsers";
            this.txtSearchUsers.Size = new System.Drawing.Size(468, 27);
            this.txtSearchUsers.TabIndex = 19;
            // 
            // BtnRefreshGrid
            // 
            this.BtnRefreshGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BtnRefreshGrid.ContextMenuStrip = this.contextMenuUsers;
            this.BtnRefreshGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefreshGrid.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefreshGrid.Location = new System.Drawing.Point(1054, 31);
            this.BtnRefreshGrid.Name = "BtnRefreshGrid";
            this.BtnRefreshGrid.Size = new System.Drawing.Size(90, 27);
            this.BtnRefreshGrid.TabIndex = 17;
            this.BtnRefreshGrid.Text = "Refesh";
            this.toolTip1.SetToolTip(this.BtnRefreshGrid, "Cancel and clear the form");
            this.BtnRefreshGrid.UseVisualStyleBackColor = false;
            this.BtnRefreshGrid.Click += new System.EventHandler(this.BtnRefreshGrid_Click);
            // 
            // contextMenuUsers
            // 
            this.contextMenuUsers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuUsers.Name = "contextMenuUsers";
            this.contextMenuUsers.Size = new System.Drawing.Size(61, 4);
            // 
            // panelUserInput
            // 
            this.panelUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserInput.Controls.Add(this.btnNewUserReg);
            this.panelUserInput.Controls.Add(this.lblIDuser);
            this.panelUserInput.Controls.Add(this.btnEditUsers);
            this.panelUserInput.Controls.Add(this.CheckActiveUser);
            this.panelUserInput.Controls.Add(this.comboRole);
            this.panelUserInput.Controls.Add(this.label7);
            this.panelUserInput.Controls.Add(this.comboUserGroup);
            this.panelUserInput.Controls.Add(this.label6);
            this.panelUserInput.Controls.Add(this.label5);
            this.panelUserInput.Controls.Add(this.txtEmail);
            this.panelUserInput.Controls.Add(this.label4);
            this.panelUserInput.Controls.Add(this.txtPassword);
            this.panelUserInput.Controls.Add(this.label3);
            this.panelUserInput.Controls.Add(this.txtUsername);
            this.panelUserInput.Controls.Add(this.label2);
            this.panelUserInput.Controls.Add(this.txtLastName);
            this.panelUserInput.Controls.Add(this.label1);
            this.panelUserInput.Controls.Add(this.txtFirstName);
            this.panelUserInput.Controls.Add(this.btnAddNewUser);
            this.panelUserInput.Controls.Add(this.btnCancel);
            this.panelUserInput.Location = new System.Drawing.Point(10, 31);
            this.panelUserInput.Name = "panelUserInput";
            this.panelUserInput.Size = new System.Drawing.Size(560, 504);
            this.panelUserInput.TabIndex = 18;
            this.panelUserInput.Paint += new System.Windows.Forms.PaintEventHandler(this.panelUserInput_Paint);
            // 
            // lblIDuser
            // 
            this.lblIDuser.AutoSize = true;
            this.lblIDuser.Location = new System.Drawing.Point(3, 0);
            this.lblIDuser.Name = "lblIDuser";
            this.lblIDuser.Size = new System.Drawing.Size(35, 20);
            this.lblIDuser.TabIndex = 17;
            this.lblIDuser.Text = "ID : ";
            // 
            // btnEditUsers
            // 
            this.btnEditUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnEditUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditUsers.Location = new System.Drawing.Point(11, 421);
            this.btnEditUsers.Name = "btnEditUsers";
            this.btnEditUsers.Size = new System.Drawing.Size(90, 30);
            this.btnEditUsers.TabIndex = 16;
            this.btnEditUsers.Text = "Edit";
            this.toolTip1.SetToolTip(this.btnEditUsers, "Cancel and clear the form");
            this.btnEditUsers.UseVisualStyleBackColor = false;
            this.btnEditUsers.Click += new System.EventHandler(this.btnEditUsers_Click);
            // 
            // CheckActiveUser
            // 
            this.CheckActiveUser.AutoSize = true;
            this.CheckActiveUser.Checked = true;
            this.CheckActiveUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckActiveUser.Enabled = false;
            this.CheckActiveUser.Location = new System.Drawing.Point(7, 348);
            this.CheckActiveUser.Name = "CheckActiveUser";
            this.CheckActiveUser.Size = new System.Drawing.Size(105, 24);
            this.CheckActiveUser.TabIndex = 9;
            this.CheckActiveUser.Text = "Active User";
            this.toolTip1.SetToolTip(this.CheckActiveUser, "Check to mark the user as active");
            this.CheckActiveUser.UseVisualStyleBackColor = true;
            // 
            // comboRole
            // 
            this.comboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRole.Enabled = false;
            this.comboRole.FormattingEnabled = true;
            this.comboRole.Location = new System.Drawing.Point(7, 308);
            this.comboRole.Name = "comboRole";
            this.comboRole.Size = new System.Drawing.Size(250, 28);
            this.comboRole.TabIndex = 8;
            this.toolTip1.SetToolTip(this.comboRole, "Select the user\'s role");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 288);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Role:";
            // 
            // comboUserGroup
            // 
            this.comboUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUserGroup.Enabled = false;
            this.comboUserGroup.FormattingEnabled = true;
            this.comboUserGroup.Location = new System.Drawing.Point(277, 308);
            this.comboUserGroup.Name = "comboUserGroup";
            this.comboUserGroup.Size = new System.Drawing.Size(250, 28);
            this.comboUserGroup.TabIndex = 7;
            this.toolTip1.SetToolTip(this.comboUserGroup, "Select the user\'s group");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(277, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Group:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new System.Drawing.Point(7, 228);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(520, 27);
            this.txtEmail.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtEmail, "Enter the user\'s email address");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(277, 148);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(250, 27);
            this.txtPassword.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtPassword, "Enter the user\'s password");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(7, 148);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(250, 27);
            this.txtUsername.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtUsername, "Enter the user\'s username");
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Enabled = false;
            this.txtLastName.Location = new System.Drawing.Point(277, 68);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(250, 27);
            this.txtLastName.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtLastName, "Enter the user\'s last name");
            this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "First Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Enabled = false;
            this.txtFirstName.Location = new System.Drawing.Point(7, 68);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(250, 27);
            this.txtFirstName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtFirstName, "Enter the user\'s first name");
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAddNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewUser.ForeColor = System.Drawing.Color.White;
            this.btnAddNewUser.Location = new System.Drawing.Point(456, 421);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(90, 30);
            this.btnAddNewUser.TabIndex = 10;
            this.btnAddNewUser.Text = "Save";
            this.toolTip1.SetToolTip(this.btnAddNewUser, "Add a new user to the system");
            this.btnAddNewUser.UseVisualStyleBackColor = false;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(360, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel and clear the form");
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(580, 66);
            this.dataGridViewUsers.MultiSelect = false;
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.Size = new System.Drawing.Size(564, 469);
            this.dataGridViewUsers.TabIndex = 12;
            this.toolTip1.SetToolTip(this.dataGridViewUsers, "List of registered users");
            this.dataGridViewUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUsers_CellContentClick);
            // 
            // tabGroups
            // 
            this.tabGroups.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabGroups.Controls.Add(this.label15);
            this.tabGroups.Controls.Add(this.label14);
            this.tabGroups.Controls.Add(this.btnAddGrp);
            this.tabGroups.Controls.Add(this.dataGridInactiveGroups);
            this.tabGroups.Controls.Add(this.btnDeactivategrp);
            this.tabGroups.Controls.Add(this.btnDeactivate);
            this.tabGroups.Controls.Add(this.dataGridUserGroupsActive);
            this.tabGroups.Controls.Add(this.label9);
            this.tabGroups.Controls.Add(this.txtGroupDesc);
            this.tabGroups.Controls.Add(this.label8);
            this.tabGroups.Controls.Add(this.txtGroupName);
            this.tabGroups.Controls.Add(this.dataGridViewGroups);
            this.tabGroups.Controls.Add(this.toolStripGroups);
            this.tabGroups.Location = new System.Drawing.Point(4, 29);
            this.tabGroups.Name = "tabGroups";
            this.tabGroups.Size = new System.Drawing.Size(1165, 541);
            this.tabGroups.TabIndex = 1;
            this.tabGroups.Text = "Groups";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(837, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 20);
            this.label15.TabIndex = 12;
            this.label15.Text = "Inactive Groups";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(371, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 20);
            this.label14.TabIndex = 11;
            this.label14.Text = "Active Groups";
            // 
            // btnAddGrp
            // 
            this.btnAddGrp.Location = new System.Drawing.Point(48, 273);
            this.btnAddGrp.Name = "btnAddGrp";
            this.btnAddGrp.Size = new System.Drawing.Size(93, 31);
            this.btnAddGrp.TabIndex = 10;
            this.btnAddGrp.Text = "New Group";
            this.btnAddGrp.UseVisualStyleBackColor = true;
            this.btnAddGrp.Click += new System.EventHandler(this.btnAddGrp_Click);
            // 
            // dataGridInactiveGroups
            // 
            this.dataGridInactiveGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInactiveGroups.Location = new System.Drawing.Point(841, 67);
            this.dataGridInactiveGroups.Name = "dataGridInactiveGroups";
            this.dataGridInactiveGroups.RowHeadersWidth = 51;
            this.dataGridInactiveGroups.RowTemplate.Height = 24;
            this.dataGridInactiveGroups.Size = new System.Drawing.Size(321, 185);
            this.dataGridInactiveGroups.TabIndex = 9;
            // 
            // btnDeactivategrp
            // 
            this.btnDeactivategrp.Location = new System.Drawing.Point(737, 167);
            this.btnDeactivategrp.Name = "btnDeactivategrp";
            this.btnDeactivategrp.Size = new System.Drawing.Size(75, 35);
            this.btnDeactivategrp.TabIndex = 8;
            this.btnDeactivategrp.Text = "<--";
            this.btnDeactivategrp.UseVisualStyleBackColor = true;
            this.btnDeactivategrp.Click += new System.EventHandler(this.btnDeactivategrp_Click);
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.Location = new System.Drawing.Point(737, 89);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(75, 35);
            this.btnDeactivate.TabIndex = 7;
            this.btnDeactivate.Text = "-->";
            this.btnDeactivate.UseVisualStyleBackColor = true;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            // 
            // dataGridUserGroupsActive
            // 
            this.dataGridUserGroupsActive.ColumnHeadersHeight = 29;
            this.dataGridUserGroupsActive.Location = new System.Drawing.Point(375, 67);
            this.dataGridUserGroupsActive.Name = "dataGridUserGroupsActive";
            this.dataGridUserGroupsActive.RowHeadersWidth = 51;
            this.dataGridUserGroupsActive.RowTemplate.Height = 24;
            this.dataGridUserGroupsActive.Size = new System.Drawing.Size(321, 185);
            this.dataGridUserGroupsActive.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 20);
            this.label9.TabIndex = 5;
            this.label9.Text = "Group Description";
            // 
            // txtGroupDesc
            // 
            this.txtGroupDesc.Location = new System.Drawing.Point(48, 167);
            this.txtGroupDesc.Multiline = true;
            this.txtGroupDesc.Name = "txtGroupDesc";
            this.txtGroupDesc.Size = new System.Drawing.Size(269, 85);
            this.txtGroupDesc.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Group Name:";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(48, 89);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(269, 27);
            this.txtGroupName.TabIndex = 2;
            // 
            // dataGridViewGroups
            // 
            this.dataGridViewGroups.ColumnHeadersHeight = 29;
            this.dataGridViewGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewGroups.Location = new System.Drawing.Point(0, 27);
            this.dataGridViewGroups.MultiSelect = false;
            this.dataGridViewGroups.Name = "dataGridViewGroups";
            this.dataGridViewGroups.ReadOnly = true;
            this.dataGridViewGroups.RowHeadersWidth = 51;
            this.dataGridViewGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGroups.Size = new System.Drawing.Size(1165, 514);
            this.dataGridViewGroups.TabIndex = 0;
            // 
            // toolStripGroups
            // 
            this.toolStripGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.toolStripGroups.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripGroups.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddGroup,
            this.btnEditGroup,
            this.btnDeleteGroup,
            this.btnRefreshGroups});
            this.toolStripGroups.Location = new System.Drawing.Point(0, 0);
            this.toolStripGroups.Name = "toolStripGroups";
            this.toolStripGroups.Size = new System.Drawing.Size(1165, 27);
            this.toolStripGroups.TabIndex = 1;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.Image")));
            this.btnAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(61, 24);
            this.btnAddGroup.Text = "Add";
            // 
            // btnEditGroup
            // 
            this.btnEditGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnEditGroup.Image")));
            this.btnEditGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditGroup.Name = "btnEditGroup";
            this.btnEditGroup.Size = new System.Drawing.Size(59, 24);
            this.btnEditGroup.Text = "Edit";
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.Image")));
            this.btnDeleteGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(77, 24);
            this.btnDeleteGroup.Text = "Delete";
            // 
            // btnRefreshGroups
            // 
            this.btnRefreshGroups.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshGroups.Image")));
            this.btnRefreshGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshGroups.Name = "btnRefreshGroups";
            this.btnRefreshGroups.Size = new System.Drawing.Size(82, 24);
            this.btnRefreshGroups.Text = "Refresh";
            // 
            // tabRoles
            // 
            this.tabRoles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabRoles.Controls.Add(this.label16);
            this.tabRoles.Controls.Add(this.label17);
            this.tabRoles.Controls.Add(this.dataGridViewInactiveRoles);
            this.tabRoles.Controls.Add(this.btnActiveteRoles);
            this.tabRoles.Controls.Add(this.btnDeactiveteRole);
            this.tabRoles.Controls.Add(this.dataGridViewActiveRoles);
            this.tabRoles.Controls.Add(this.btnAddRoleActive);
            this.tabRoles.Controls.Add(this.label11);
            this.tabRoles.Controls.Add(this.txtRoleDesc);
            this.tabRoles.Controls.Add(this.label10);
            this.tabRoles.Controls.Add(this.txtRoleName);
            this.tabRoles.Controls.Add(this.dataGridViewRoles);
            this.tabRoles.Controls.Add(this.toolStripRoles);
            this.tabRoles.Location = new System.Drawing.Point(4, 29);
            this.tabRoles.Name = "tabRoles";
            this.tabRoles.Size = new System.Drawing.Size(1165, 541);
            this.tabRoles.TabIndex = 2;
            this.tabRoles.Text = "Roles";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(778, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 20);
            this.label16.TabIndex = 18;
            this.label16.Text = "Inactive Groups";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(312, 81);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 20);
            this.label17.TabIndex = 17;
            this.label17.Text = "Active Groups";
            // 
            // dataGridViewInactiveRoles
            // 
            this.dataGridViewInactiveRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInactiveRoles.Location = new System.Drawing.Point(782, 104);
            this.dataGridViewInactiveRoles.Name = "dataGridViewInactiveRoles";
            this.dataGridViewInactiveRoles.RowHeadersWidth = 51;
            this.dataGridViewInactiveRoles.RowTemplate.Height = 24;
            this.dataGridViewInactiveRoles.Size = new System.Drawing.Size(321, 185);
            this.dataGridViewInactiveRoles.TabIndex = 16;
            this.dataGridViewInactiveRoles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInactiveRoles_CellContentClick);
            // 
            // btnActiveteRoles
            // 
            this.btnActiveteRoles.Location = new System.Drawing.Point(678, 204);
            this.btnActiveteRoles.Name = "btnActiveteRoles";
            this.btnActiveteRoles.Size = new System.Drawing.Size(75, 35);
            this.btnActiveteRoles.TabIndex = 15;
            this.btnActiveteRoles.Text = "<--";
            this.btnActiveteRoles.UseVisualStyleBackColor = true;
            this.btnActiveteRoles.Click += new System.EventHandler(this.btnActiveteRoles_Click);
            // 
            // btnDeactiveteRole
            // 
            this.btnDeactiveteRole.Location = new System.Drawing.Point(678, 126);
            this.btnDeactiveteRole.Name = "btnDeactiveteRole";
            this.btnDeactiveteRole.Size = new System.Drawing.Size(75, 35);
            this.btnDeactiveteRole.TabIndex = 14;
            this.btnDeactiveteRole.Text = "-->";
            this.btnDeactiveteRole.UseVisualStyleBackColor = true;
            this.btnDeactiveteRole.Click += new System.EventHandler(this.btnDeactiveteRole_Click);
            // 
            // dataGridViewActiveRoles
            // 
            this.dataGridViewActiveRoles.ColumnHeadersHeight = 29;
            this.dataGridViewActiveRoles.Location = new System.Drawing.Point(316, 104);
            this.dataGridViewActiveRoles.Name = "dataGridViewActiveRoles";
            this.dataGridViewActiveRoles.RowHeadersWidth = 51;
            this.dataGridViewActiveRoles.RowTemplate.Height = 24;
            this.dataGridViewActiveRoles.Size = new System.Drawing.Size(321, 185);
            this.dataGridViewActiveRoles.TabIndex = 13;
            this.dataGridViewActiveRoles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewActiveRoles_CellContentClick);
            // 
            // btnAddRoleActive
            // 
            this.btnAddRoleActive.Location = new System.Drawing.Point(42, 267);
            this.btnAddRoleActive.Name = "btnAddRoleActive";
            this.btnAddRoleActive.Size = new System.Drawing.Size(90, 30);
            this.btnAddRoleActive.TabIndex = 6;
            this.btnAddRoleActive.Text = "Add Role";
            this.btnAddRoleActive.UseVisualStyleBackColor = true;
            this.btnAddRoleActive.Click += new System.EventHandler(this.btnAddRoleActive_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(46, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 20);
            this.label11.TabIndex = 5;
            this.label11.Text = "Role Description";
            // 
            // txtRoleDesc
            // 
            this.txtRoleDesc.Location = new System.Drawing.Point(42, 164);
            this.txtRoleDesc.Multiline = true;
            this.txtRoleDesc.Name = "txtRoleDesc";
            this.txtRoleDesc.Size = new System.Drawing.Size(236, 97);
            this.txtRoleDesc.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(46, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "RoleName";
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(42, 100);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(236, 27);
            this.txtRoleName.TabIndex = 2;
            // 
            // dataGridViewRoles
            // 
            this.dataGridViewRoles.ColumnHeadersHeight = 29;
            this.dataGridViewRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRoles.Location = new System.Drawing.Point(0, 27);
            this.dataGridViewRoles.MultiSelect = false;
            this.dataGridViewRoles.Name = "dataGridViewRoles";
            this.dataGridViewRoles.ReadOnly = true;
            this.dataGridViewRoles.RowHeadersWidth = 51;
            this.dataGridViewRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRoles.Size = new System.Drawing.Size(1165, 514);
            this.dataGridViewRoles.TabIndex = 0;
            // 
            // toolStripRoles
            // 
            this.toolStripRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.toolStripRoles.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripRoles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddRole,
            this.btnEditRole,
            this.btnDeleteRole,
            this.btnRefreshRoles});
            this.toolStripRoles.Location = new System.Drawing.Point(0, 0);
            this.toolStripRoles.Name = "toolStripRoles";
            this.toolStripRoles.Size = new System.Drawing.Size(1165, 27);
            this.toolStripRoles.TabIndex = 1;
            this.toolStripRoles.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripRoles_ItemClicked);
            // 
            // btnAddRole
            // 
            this.btnAddRole.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRole.Image")));
            this.btnAddRole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(61, 24);
            this.btnAddRole.Text = "Add";
            // 
            // btnEditRole
            // 
            this.btnEditRole.Image = ((System.Drawing.Image)(resources.GetObject("btnEditRole.Image")));
            this.btnEditRole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditRole.Name = "btnEditRole";
            this.btnEditRole.Size = new System.Drawing.Size(59, 24);
            this.btnEditRole.Text = "Edit";
            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteRole.Image")));
            this.btnDeleteRole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(77, 24);
            this.btnDeleteRole.Text = "Delete";
            // 
            // btnRefreshRoles
            // 
            this.btnRefreshRoles.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshRoles.Image")));
            this.btnRefreshRoles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshRoles.Name = "btnRefreshRoles";
            this.btnRefreshRoles.Size = new System.Drawing.Size(82, 24);
            this.btnRefreshRoles.Text = "Refresh";
            // 
            // tabPermissions
            // 
            this.tabPermissions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPermissions.Controls.Add(this.comboSelectGroup);
            this.tabPermissions.Controls.Add(this.btnSavePermission);
            this.tabPermissions.Controls.Add(this.clbPermissions);
            this.tabPermissions.Controls.Add(this.label18);
            this.tabPermissions.Controls.Add(this.comboRoles);
            this.tabPermissions.Controls.Add(this.btnAssignRoles);
            this.tabPermissions.Controls.Add(this.clbRoles);
            this.tabPermissions.Controls.Add(this.label13);
            this.tabPermissions.Controls.Add(this.label12);
            this.tabPermissions.Controls.Add(this.dataGridViewPermissions);
            this.tabPermissions.Controls.Add(this.toolStripPermissions);
            this.tabPermissions.Location = new System.Drawing.Point(4, 29);
            this.tabPermissions.Name = "tabPermissions";
            this.tabPermissions.Size = new System.Drawing.Size(1165, 541);
            this.tabPermissions.TabIndex = 3;
            this.tabPermissions.Text = "Permissions";
            // 
            // comboSelectGroup
            // 
            this.comboSelectGroup.FormattingEnabled = true;
            this.comboSelectGroup.Location = new System.Drawing.Point(20, 88);
            this.comboSelectGroup.Name = "comboSelectGroup";
            this.comboSelectGroup.Size = new System.Drawing.Size(227, 28);
            this.comboSelectGroup.TabIndex = 11;
            this.comboSelectGroup.SelectedIndexChanged += new System.EventHandler(this.comboSelectGroup_SelectedIndexChanged);
            // 
            // btnSavePermission
            // 
            this.btnSavePermission.Location = new System.Drawing.Point(675, 378);
            this.btnSavePermission.Name = "btnSavePermission";
            this.btnSavePermission.Size = new System.Drawing.Size(90, 42);
            this.btnSavePermission.TabIndex = 10;
            this.btnSavePermission.Text = "Save Permission";
            this.btnSavePermission.UseVisualStyleBackColor = true;
            this.btnSavePermission.Click += new System.EventHandler(this.btnSavePermission_Click);
            // 
            // clbPermissions
            // 
            this.clbPermissions.FormattingEnabled = true;
            this.clbPermissions.Location = new System.Drawing.Point(675, 133);
            this.clbPermissions.Name = "clbPermissions";
            this.clbPermissions.Size = new System.Drawing.Size(227, 224);
            this.clbPermissions.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(671, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 20);
            this.label18.TabIndex = 8;
            this.label18.Text = "SelectRole";
            // 
            // comboRoles
            // 
            this.comboRoles.FormattingEnabled = true;
            this.comboRoles.Location = new System.Drawing.Point(671, 99);
            this.comboRoles.Name = "comboRoles";
            this.comboRoles.Size = new System.Drawing.Size(227, 28);
            this.comboRoles.TabIndex = 7;
            this.comboRoles.SelectedIndexChanged += new System.EventHandler(this.comboRoles_SelectedIndexChanged);
            // 
            // btnAssignRoles
            // 
            this.btnAssignRoles.Location = new System.Drawing.Point(20, 388);
            this.btnAssignRoles.Name = "btnAssignRoles";
            this.btnAssignRoles.Size = new System.Drawing.Size(90, 42);
            this.btnAssignRoles.TabIndex = 6;
            this.btnAssignRoles.Text = "button1";
            this.btnAssignRoles.UseVisualStyleBackColor = true;
            this.btnAssignRoles.Click += new System.EventHandler(this.btnAssignRoles_Click);
            // 
            // clbRoles
            // 
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(20, 162);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(227, 202);
            this.clbRoles.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 130);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 20);
            this.label13.TabIndex = 4;
            this.label13.Text = "Roles";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 20);
            this.label12.TabIndex = 3;
            this.label12.Text = "SelectGroup";
            // 
            // dataGridViewPermissions
            // 
            this.dataGridViewPermissions.ColumnHeadersHeight = 29;
            this.dataGridViewPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPermissions.Location = new System.Drawing.Point(0, 27);
            this.dataGridViewPermissions.MultiSelect = false;
            this.dataGridViewPermissions.Name = "dataGridViewPermissions";
            this.dataGridViewPermissions.ReadOnly = true;
            this.dataGridViewPermissions.RowHeadersWidth = 51;
            this.dataGridViewPermissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPermissions.Size = new System.Drawing.Size(1165, 514);
            this.dataGridViewPermissions.TabIndex = 0;
            // 
            // toolStripPermissions
            // 
            this.toolStripPermissions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.toolStripPermissions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPermissions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddPermission,
            this.btnEditPermission,
            this.btnDeletePermission,
            this.btnRefreshPermissions});
            this.toolStripPermissions.Location = new System.Drawing.Point(0, 0);
            this.toolStripPermissions.Name = "toolStripPermissions";
            this.toolStripPermissions.Size = new System.Drawing.Size(1165, 27);
            this.toolStripPermissions.TabIndex = 1;
            // 
            // btnAddPermission
            // 
            this.btnAddPermission.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPermission.Image")));
            this.btnAddPermission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddPermission.Name = "btnAddPermission";
            this.btnAddPermission.Size = new System.Drawing.Size(61, 24);
            this.btnAddPermission.Text = "Add";
            // 
            // btnEditPermission
            // 
            this.btnEditPermission.Image = ((System.Drawing.Image)(resources.GetObject("btnEditPermission.Image")));
            this.btnEditPermission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditPermission.Name = "btnEditPermission";
            this.btnEditPermission.Size = new System.Drawing.Size(59, 24);
            this.btnEditPermission.Text = "Edit";
            // 
            // btnDeletePermission
            // 
            this.btnDeletePermission.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePermission.Image")));
            this.btnDeletePermission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeletePermission.Name = "btnDeletePermission";
            this.btnDeletePermission.Size = new System.Drawing.Size(77, 24);
            this.btnDeletePermission.Text = "Delete";
            // 
            // btnRefreshPermissions
            // 
            this.btnRefreshPermissions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshPermissions.Image")));
            this.btnRefreshPermissions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshPermissions.Name = "btnRefreshPermissions";
            this.btnRefreshPermissions.Size = new System.Drawing.Size(82, 24);
            this.btnRefreshPermissions.Text = "Refresh";
            this.btnRefreshPermissions.Click += new System.EventHandler(this.btnRefreshPermissions_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1173, 26);
            this.statusStrip1.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(50, 20);
            this.statusLabel.Text = "Ready";
            // 
            // btnNewUserReg
            // 
            this.btnNewUserReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnNewUserReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewUserReg.Location = new System.Drawing.Point(107, 421);
            this.btnNewUserReg.Name = "btnNewUserReg";
            this.btnNewUserReg.Size = new System.Drawing.Size(90, 30);
            this.btnNewUserReg.TabIndex = 18;
            this.btnNewUserReg.Text = "NewUser";
            this.toolTip1.SetToolTip(this.btnNewUserReg, "Cancel and clear the form");
            this.btnNewUserReg.UseVisualStyleBackColor = false;
            this.btnNewUserReg.Click += new System.EventHandler(this.btnNewUserReg_Click);
            // 
            // FormUserPermissions
            // 
            this.ClientSize = new System.Drawing.Size(1173, 600);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "FormUserPermissions";
            this.Text = "Permissions Management";
            this.Load += new System.EventHandler(this.FormUserPermissions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.panelUserInput.ResumeLayout(false);
            this.panelUserInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.tabGroups.ResumeLayout(false);
            this.tabGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInactiveGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUserGroupsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).EndInit();
            this.toolStripGroups.ResumeLayout(false);
            this.toolStripGroups.PerformLayout();
            this.tabRoles.ResumeLayout(false);
            this.tabRoles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInactiveRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActiveRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoles)).EndInit();
            this.toolStripRoles.ResumeLayout(false);
            this.toolStripRoles.PerformLayout();
            this.tabPermissions.ResumeLayout(false);
            this.tabPermissions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPermissions)).EndInit();
            this.toolStripPermissions.ResumeLayout(false);
            this.toolStripPermissions.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabUsers;
        private TabPage tabGroups;
        private TabPage tabRoles;
        private TabPage tabPermissions;
        private Panel panelUserInput;
        private DataGridView dataGridViewUsers;
        private DataGridView dataGridViewGroups;
        private DataGridView dataGridViewRoles;
        private DataGridView dataGridViewPermissions;
        private ToolStrip toolStripGroups;
        private ToolStrip toolStripRoles;
        private ToolStrip toolStripPermissions;
        private ToolStripButton btnAddGroup;
        private ToolStripButton btnEditGroup;
        private ToolStripButton btnDeleteGroup;
        private ToolStripButton btnRefreshGroups;
        private ToolStripButton btnAddRole;
        private ToolStripButton btnEditRole;
        private ToolStripButton btnDeleteRole;
        private ToolStripButton btnRefreshRoles;
        private ToolStripButton btnAddPermission;
        private ToolStripButton btnEditPermission;
        private ToolStripButton btnDeletePermission;
        private ToolStripButton btnRefreshPermissions;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private ContextMenuStrip contextMenuUsers;
        private ToolTip toolTip1;
        private CheckBox CheckActiveUser;
        private ComboBox comboRole;
        private Label label7;
        private ComboBox comboUserGroup;
        private Label label6;
        private Label label5;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtPassword;
        private Label label3;
        private TextBox txtUsername;
        private Label label2;
        private TextBox txtLastName;
        private Label label1;
        private TextBox txtFirstName;
        private Button btnAddNewUser;
        private Button btnCancel;
        private Label label8;
        private TextBox txtGroupName;
        private Label label9;
        private TextBox txtGroupDesc;
        private Label label10;
        private TextBox txtRoleName;
        private Label label11;
        private TextBox txtRoleDesc;
        private CheckedListBox clbRoles;
        private Label label13;
        private Label label12;
        private Button BtnRefreshGrid;
        private Button btnEditUsers;
        private TextBox txtSearchUsers;
        private DataGridView dataGridUserGroupsActive;
        private Button btnDeactivategrp;
        private Button btnDeactivate;
        private DataGridView dataGridInactiveGroups;
        private Button btnAssignRoles;
        private Button btnAddGrp;
        private Label label15;
        private Label label14;
        private Button btnAddRoleActive;
        private Label label16;
        private Label label17;
        private DataGridView dataGridViewInactiveRoles;
        private Button btnActiveteRoles;
        private Button btnDeactiveteRole;
        private DataGridView dataGridViewActiveRoles;
        private CheckedListBox clbPermissions;
        private Label label18;
        private ComboBox comboRoles;
        private Button btnSavePermission;
        private ComboBox comboSelectGroup;
        private Label lblIDuser;
        private Button btnNewUserReg;
    }

}
