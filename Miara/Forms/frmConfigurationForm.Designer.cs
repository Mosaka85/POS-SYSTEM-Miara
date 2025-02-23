
using System;

namespace Miara
{
    partial class frmConfigurationForm
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
            this.txtSQLDataSource = new System.Windows.Forms.TextBox();
            this.txtSQLusername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.comboDatabase = new System.Windows.Forms.ComboBox();
            this.btnTestSQL = new System.Windows.Forms.Button();
            this.btnSQLSAVE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCloseSQLfrm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSQLDataSource
            // 
            this.txtSQLDataSource.BackColor = System.Drawing.SystemColors.Control;
            this.txtSQLDataSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLDataSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLDataSource.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSQLDataSource.Location = new System.Drawing.Point(327, 96);
            this.txtSQLDataSource.Name = "txtSQLDataSource";
            this.txtSQLDataSource.Size = new System.Drawing.Size(273, 27);
            this.txtSQLDataSource.TabIndex = 0;
            // 
            // txtSQLusername
            // 
            this.txtSQLusername.BackColor = System.Drawing.SystemColors.Control;
            this.txtSQLusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLusername.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSQLusername.Location = new System.Drawing.Point(327, 145);
            this.txtSQLusername.Name = "txtSQLusername";
            this.txtSQLusername.Size = new System.Drawing.Size(273, 27);
            this.txtSQLusername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtPassword.Location = new System.Drawing.Point(327, 199);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(273, 27);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // comboDatabase
            // 
            this.comboDatabase.FormattingEnabled = true;
            this.comboDatabase.Location = new System.Drawing.Point(352, 273);
            this.comboDatabase.Name = "comboDatabase";
            this.comboDatabase.Size = new System.Drawing.Size(246, 24);
            this.comboDatabase.TabIndex = 3;
            // 
            // btnTestSQL
            // 
            this.btnTestSQL.BackColor = System.Drawing.Color.LightCoral;
            this.btnTestSQL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnTestSQL.Location = new System.Drawing.Point(541, 236);
            this.btnTestSQL.Name = "btnTestSQL";
            this.btnTestSQL.Size = new System.Drawing.Size(57, 28);
            this.btnTestSQL.TabIndex = 4;
            this.btnTestSQL.Text = "Test";
            this.btnTestSQL.UseVisualStyleBackColor = false;
            this.btnTestSQL.Click += new System.EventHandler(this.btnTestSQL_Click);
            // 
            // btnSQLSAVE
            // 
            this.btnSQLSAVE.BackColor = System.Drawing.Color.LightCoral;
            this.btnSQLSAVE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSQLSAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSQLSAVE.Location = new System.Drawing.Point(541, 303);
            this.btnSQLSAVE.Name = "btnSQLSAVE";
            this.btnSQLSAVE.Size = new System.Drawing.Size(57, 32);
            this.btnSQLSAVE.TabIndex = 5;
            this.btnSQLSAVE.Text = "Save";
            this.btnSQLSAVE.UseVisualStyleBackColor = false;

            this.btnSQLSAVE.Click += new System.EventHandler(this.btnSQLSAVE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(286, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "SQL Connection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(193, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Server Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Firebrick;
            this.label3.Location = new System.Drawing.Point(216, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Firebrick;
            this.label4.Location = new System.Drawing.Point(222, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password";
            // 
            // btnCloseSQLfrm
            // 
            this.btnCloseSQLfrm.BackColor = System.Drawing.Color.LightCoral;
            this.btnCloseSQLfrm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseSQLfrm.Location = new System.Drawing.Point(733, 12);
            this.btnCloseSQLfrm.Name = "btnCloseSQLfrm";
            this.btnCloseSQLfrm.Size = new System.Drawing.Size(55, 27);
            this.btnCloseSQLfrm.TabIndex = 10;
            this.btnCloseSQLfrm.Text = "X";
            this.btnCloseSQLfrm.UseVisualStyleBackColor = false;
            this.btnCloseSQLfrm.Click += new System.EventHandler(this.btnCloseSQLfrm_Click);
            // 
            // frmConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(800, 379);
            this.Controls.Add(this.btnCloseSQLfrm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSQLSAVE);
            this.Controls.Add(this.btnTestSQL);
            this.Controls.Add(this.comboDatabase);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtSQLusername);
            this.Controls.Add(this.txtSQLDataSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
        #endregion

        private System.Windows.Forms.TextBox txtSQLDataSource;
        private System.Windows.Forms.TextBox txtSQLusername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox comboDatabase;
        private System.Windows.Forms.Button btnTestSQL;
        private System.Windows.Forms.Button btnSQLSAVE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCloseSQLfrm;
    }
}