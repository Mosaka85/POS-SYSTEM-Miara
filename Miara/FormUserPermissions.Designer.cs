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
            this.panelUserInput = new System.Windows.Forms.Panel();
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
            this.toolStripUsers = new System.Windows.Forms.ToolStrip();
            this.btnAddUser = new System.Windows.Forms.ToolStripButton();
            this.btnEditUser = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteUser = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshUsers = new System.Windows.Forms.ToolStripButton();
            this.txtSearchUsers = new System.Windows.Forms.ToolStripTextBox();
            this.tabGroups = new System.Windows.Forms.TabPage();
            this.dataGridViewGroups = new System.Windows.Forms.DataGridView();
            this.toolStripGroups = new System.Windows.Forms.ToolStrip();
            this.btnAddGroup = new System.Windows.Forms.ToolStripButton();
            this.btnEditGroup = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteGroup = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshGroups = new System.Windows.Forms.ToolStripButton();
            this.tabRoles = new System.Windows.Forms.TabPage();
            this.dataGridViewRoles = new System.Windows.Forms.DataGridView();
            this.toolStripRoles = new System.Windows.Forms.ToolStrip();
            this.btnAddRole = new System.Windows.Forms.ToolStripButton();
            this.btnEditRole = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteRole = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshRoles = new System.Windows.Forms.ToolStripButton();
            this.tabPermissions = new System.Windows.Forms.TabPage();
            this.dataGridViewPermissions = new System.Windows.Forms.DataGridView();
            this.toolStripPermissions = new System.Windows.Forms.ToolStrip();
            this.btnAddPermission = new System.Windows.Forms.ToolStripButton();
            this.btnEditPermission = new System.Windows.Forms.ToolStripButton();
            this.btnDeletePermission = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshPermissions = new System.Windows.Forms.ToolStripButton();
            this.contextMenuUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.panelUserInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.toolStripUsers.SuspendLayout();
            this.tabGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).BeginInit();
            this.toolStripGroups.SuspendLayout();
            this.tabRoles.SuspendLayout();
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
            this.tabUsers.Controls.Add(this.panelUserInput);
            this.tabUsers.Controls.Add(this.dataGridViewUsers);
            this.tabUsers.Controls.Add(this.toolStripUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 29);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(1165, 541);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Users";
            // 
            // panelUserInput
            // 
            this.panelUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panelUserInput.Location = new System.Drawing.Point(10, 35);
            this.panelUserInput.Name = "panelUserInput";
            this.panelUserInput.Size = new System.Drawing.Size(560, 500);
            this.panelUserInput.TabIndex = 18;
            // 
            // CheckActiveUser
            // 
            this.CheckActiveUser.AutoSize = true;
            this.CheckActiveUser.Checked = true;
            this.CheckActiveUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckActiveUser.Location = new System.Drawing.Point(20, 410);
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
            this.comboRole.FormattingEnabled = true;
            this.comboRole.Location = new System.Drawing.Point(20, 370);
            this.comboRole.Name = "comboRole";
            this.comboRole.Size = new System.Drawing.Size(250, 28);
            this.comboRole.TabIndex = 8;
            this.toolTip1.SetToolTip(this.comboRole, "Select the user\'s role");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 350);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Role:";
            // 
            // comboUserGroup
            // 
            this.comboUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUserGroup.FormattingEnabled = true;
            this.comboUserGroup.Location = new System.Drawing.Point(290, 370);
            this.comboUserGroup.Name = "comboUserGroup";
            this.comboUserGroup.Size = new System.Drawing.Size(250, 28);
            this.comboUserGroup.TabIndex = 7;
            this.toolTip1.SetToolTip(this.comboUserGroup, "Select the user\'s group");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Group:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(20, 290);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(520, 27);
            this.txtEmail.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtEmail, "Enter the user\'s email address");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(290, 210);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(250, 27);
            this.txtPassword.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtPassword, "Enter the user\'s password");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(20, 210);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(250, 27);
            this.txtUsername.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtUsername, "Enter the user\'s username");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(290, 130);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(250, 27);
            this.txtLastName.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtLastName, "Enter the user\'s last name");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "First Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(20, 130);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(250, 27);
            this.txtFirstName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtFirstName, "Enter the user\'s first name");
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAddNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewUser.ForeColor = System.Drawing.Color.White;
            this.btnAddNewUser.Location = new System.Drawing.Point(360, 450);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(90, 30);
            this.btnAddNewUser.TabIndex = 10;
            this.btnAddNewUser.Text = "Add User";
            this.toolTip1.SetToolTip(this.btnAddNewUser, "Add a new user to the system");
            this.btnAddNewUser.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(460, 450);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel and clear the form");
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeight = 29;
            this.dataGridViewUsers.Location = new System.Drawing.Point(580, 35);
            this.dataGridViewUsers.MultiSelect = false;
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.RowHeadersWidth = 51;
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.Size = new System.Drawing.Size(564, 500);
            this.dataGridViewUsers.TabIndex = 12;
            this.toolTip1.SetToolTip(this.dataGridViewUsers, "List of registered users");
            // 
            // toolStripUsers
            // 
            this.toolStripUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.toolStripUsers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddUser,
            this.btnEditUser,
            this.btnDeleteUser,
            this.btnRefreshUsers,
            this.txtSearchUsers});
            this.toolStripUsers.Location = new System.Drawing.Point(0, 0);
            this.toolStripUsers.Name = "toolStripUsers";
            this.toolStripUsers.Size = new System.Drawing.Size(1165, 27);
            this.toolStripUsers.TabIndex = 1;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUser.Image")));
            this.btnAddUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(61, 24);
            this.btnAddUser.Text = "Add";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Image = ((System.Drawing.Image)(resources.GetObject("btnEditUser.Image")));
            this.btnEditUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(59, 24);
            this.btnEditUser.Text = "Edit";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteUser.Image")));
            this.btnDeleteUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(77, 24);
            this.btnDeleteUser.Text = "Delete";
            // 
            // btnRefreshUsers
            // 
            this.btnRefreshUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshUsers.Image")));
            this.btnRefreshUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshUsers.Name = "btnRefreshUsers";
            this.btnRefreshUsers.Size = new System.Drawing.Size(82, 24);
            this.btnRefreshUsers.Text = "Refresh";
            this.btnRefreshUsers.Click += new System.EventHandler(this.btnRefreshUsers_Click);
            // 
            // txtSearchUsers
            // 
            this.txtSearchUsers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchUsers.Name = "txtSearchUsers";
            this.txtSearchUsers.Size = new System.Drawing.Size(200, 27);
            this.txtSearchUsers.Text = "Search users...";
            // 
            // tabGroups
            // 
            this.tabGroups.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabGroups.Controls.Add(this.dataGridViewGroups);
            this.tabGroups.Controls.Add(this.toolStripGroups);
            this.tabGroups.Location = new System.Drawing.Point(4, 29);
            this.tabGroups.Name = "tabGroups";
            this.tabGroups.Size = new System.Drawing.Size(1165, 541);
            this.tabGroups.TabIndex = 1;
            this.tabGroups.Text = "Groups";
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
            this.tabRoles.Controls.Add(this.dataGridViewRoles);
            this.tabRoles.Controls.Add(this.toolStripRoles);
            this.tabRoles.Location = new System.Drawing.Point(4, 29);
            this.tabRoles.Name = "tabRoles";
            this.tabRoles.Size = new System.Drawing.Size(1165, 541);
            this.tabRoles.TabIndex = 2;
            this.tabRoles.Text = "Roles";
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
            this.tabPermissions.Controls.Add(this.dataGridViewPermissions);
            this.tabPermissions.Controls.Add(this.toolStripPermissions);
            this.tabPermissions.Location = new System.Drawing.Point(4, 29);
            this.tabPermissions.Name = "tabPermissions";
            this.tabPermissions.Size = new System.Drawing.Size(1165, 541);
            this.tabPermissions.TabIndex = 3;
            this.tabPermissions.Text = "Permissions";
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
            // 
            // contextMenuUsers
            // 
            this.contextMenuUsers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuUsers.Name = "contextMenuUsers";
            this.contextMenuUsers.Size = new System.Drawing.Size(61, 4);
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
            this.tabControl1.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.panelUserInput.ResumeLayout(false);
            this.panelUserInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.toolStripUsers.ResumeLayout(false);
            this.toolStripUsers.PerformLayout();
            this.tabGroups.ResumeLayout(false);
            this.tabGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).EndInit();
            this.toolStripGroups.ResumeLayout(false);
            this.toolStripGroups.PerformLayout();
            this.tabRoles.ResumeLayout(false);
            this.tabRoles.PerformLayout();
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
        private ToolStrip toolStripUsers;
        private ToolStrip toolStripGroups;
        private ToolStrip toolStripRoles;
        private ToolStrip toolStripPermissions;
        private ToolStripButton btnAddUser;
        private ToolStripButton btnEditUser;
        private ToolStripButton btnDeleteUser;
        private ToolStripButton btnRefreshUsers;
        private ToolStripTextBox txtSearchUsers;
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

    }

}
