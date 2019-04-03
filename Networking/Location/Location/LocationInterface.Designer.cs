namespace Location
{
    partial class LocationInterface
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
            this.finlbl = new System.Windows.Forms.Button();
            this.warnlbl = new System.Windows.Forms.Label();
            this.implbl = new System.Windows.Forms.Label();
            this.addlbl = new System.Windows.Forms.Label();
            this.addtxt = new System.Windows.Forms.TextBox();
            this.portlbl = new System.Windows.Forms.Label();
            this.porttxt = new System.Windows.Forms.TextBox();
            this.timetxt = new System.Windows.Forms.TextBox();
            this.timelbl = new System.Windows.Forms.Label();
            this.loctxt = new System.Windows.Forms.TextBox();
            this.loclbl = new System.Windows.Forms.Label();
            this.usrlbl = new System.Windows.Forms.Label();
            this.usrtxt = new System.Windows.Forms.TextBox();
            this.protgrp = new System.Windows.Forms.GroupBox();
            this.advgrp = new System.Windows.Forms.GroupBox();
            this.titlelbl = new System.Windows.Forms.Label();
            this.whorad = new System.Windows.Forms.RadioButton();
            this.http9rad = new System.Windows.Forms.RadioButton();
            this.HTTP0rad = new System.Windows.Forms.RadioButton();
            this.HTTP1rad = new System.Windows.Forms.RadioButton();
            this.advimplbl = new System.Windows.Forms.Label();
            this.advlbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.protgrp.SuspendLayout();
            this.advgrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // finlbl
            // 
            this.finlbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.finlbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.finlbl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.finlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.finlbl.Location = new System.Drawing.Point(188, 595);
            this.finlbl.Name = "finlbl";
            this.finlbl.Size = new System.Drawing.Size(75, 23);
            this.finlbl.TabIndex = 0;
            this.finlbl.Text = "Send";
            this.finlbl.UseVisualStyleBackColor = false;
            this.finlbl.Click += new System.EventHandler(this.button1_Click);
            // 
            // warnlbl
            // 
            this.warnlbl.AutoSize = true;
            this.warnlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.warnlbl.Location = new System.Drawing.Point(15, 191);
            this.warnlbl.Name = "warnlbl";
            this.warnlbl.Size = new System.Drawing.Size(334, 17);
            this.warnlbl.TabIndex = 1;
            this.warnlbl.Text = "Any text box left empty will assume the default input.";
            // 
            // implbl
            // 
            this.implbl.AutoSize = true;
            this.implbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.implbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.implbl.Location = new System.Drawing.Point(15, 171);
            this.implbl.Name = "implbl";
            this.implbl.Size = new System.Drawing.Size(102, 17);
            this.implbl.TabIndex = 2;
            this.implbl.Text = "IMPORTANT!";
            // 
            // addlbl
            // 
            this.addlbl.AutoSize = true;
            this.addlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.addlbl.Location = new System.Drawing.Point(6, 89);
            this.addlbl.Name = "addlbl";
            this.addlbl.Size = new System.Drawing.Size(64, 17);
            this.addlbl.TabIndex = 3;
            this.addlbl.Text = "Address:";
            // 
            // addtxt
            // 
            this.addtxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.addtxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.addtxt.Location = new System.Drawing.Point(74, 88);
            this.addtxt.Margin = new System.Windows.Forms.Padding(0);
            this.addtxt.Name = "addtxt";
            this.addtxt.Size = new System.Drawing.Size(342, 20);
            this.addtxt.TabIndex = 4;
            // 
            // portlbl
            // 
            this.portlbl.AutoSize = true;
            this.portlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.portlbl.Location = new System.Drawing.Point(32, 58);
            this.portlbl.Name = "portlbl";
            this.portlbl.Size = new System.Drawing.Size(38, 17);
            this.portlbl.TabIndex = 5;
            this.portlbl.Text = "Port:";
            // 
            // porttxt
            // 
            this.porttxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.porttxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.porttxt.Location = new System.Drawing.Point(74, 57);
            this.porttxt.Margin = new System.Windows.Forms.Padding(0);
            this.porttxt.Name = "porttxt";
            this.porttxt.Size = new System.Drawing.Size(342, 20);
            this.porttxt.TabIndex = 6;
            // 
            // timetxt
            // 
            this.timetxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.timetxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.timetxt.Location = new System.Drawing.Point(74, 27);
            this.timetxt.Margin = new System.Windows.Forms.Padding(0);
            this.timetxt.Name = "timetxt";
            this.timetxt.Size = new System.Drawing.Size(342, 20);
            this.timetxt.TabIndex = 7;
            // 
            // timelbl
            // 
            this.timelbl.AutoSize = true;
            this.timelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timelbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.timelbl.Location = new System.Drawing.Point(8, 28);
            this.timelbl.Name = "timelbl";
            this.timelbl.Size = new System.Drawing.Size(63, 17);
            this.timelbl.TabIndex = 8;
            this.timelbl.Text = "Timeout:";
            // 
            // loctxt
            // 
            this.loctxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.loctxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.loctxt.Location = new System.Drawing.Point(85, 256);
            this.loctxt.Margin = new System.Windows.Forms.Padding(0);
            this.loctxt.Name = "loctxt";
            this.loctxt.Size = new System.Drawing.Size(342, 20);
            this.loctxt.TabIndex = 11;
            // 
            // loclbl
            // 
            this.loclbl.AutoSize = true;
            this.loclbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loclbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.loclbl.Location = new System.Drawing.Point(13, 257);
            this.loclbl.Name = "loclbl";
            this.loclbl.Size = new System.Drawing.Size(66, 17);
            this.loclbl.TabIndex = 12;
            this.loclbl.Text = "Location:";
            // 
            // usrlbl
            // 
            this.usrlbl.AutoSize = true;
            this.usrlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.usrlbl.Location = new System.Drawing.Point(37, 222);
            this.usrlbl.Name = "usrlbl";
            this.usrlbl.Size = new System.Drawing.Size(42, 17);
            this.usrlbl.TabIndex = 13;
            this.usrlbl.Text = "User:";
            // 
            // usrtxt
            // 
            this.usrtxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.usrtxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.usrtxt.Location = new System.Drawing.Point(85, 222);
            this.usrtxt.Margin = new System.Windows.Forms.Padding(0);
            this.usrtxt.Name = "usrtxt";
            this.usrtxt.Size = new System.Drawing.Size(342, 20);
            this.usrtxt.TabIndex = 14;
            // 
            // protgrp
            // 
            this.protgrp.Controls.Add(this.HTTP1rad);
            this.protgrp.Controls.Add(this.HTTP0rad);
            this.protgrp.Controls.Add(this.http9rad);
            this.protgrp.Controls.Add(this.whorad);
            this.protgrp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.protgrp.Location = new System.Drawing.Point(12, 286);
            this.protgrp.Name = "protgrp";
            this.protgrp.Size = new System.Drawing.Size(431, 104);
            this.protgrp.TabIndex = 15;
            this.protgrp.TabStop = false;
            this.protgrp.Text = "Protocol Selection";
            // 
            // advgrp
            // 
            this.advgrp.Controls.Add(this.timelbl);
            this.advgrp.Controls.Add(this.timetxt);
            this.advgrp.Controls.Add(this.portlbl);
            this.advgrp.Controls.Add(this.porttxt);
            this.advgrp.Controls.Add(this.addtxt);
            this.advgrp.Controls.Add(this.addlbl);
            this.advgrp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.advgrp.Location = new System.Drawing.Point(12, 462);
            this.advgrp.Name = "advgrp";
            this.advgrp.Size = new System.Drawing.Size(431, 127);
            this.advgrp.TabIndex = 16;
            this.advgrp.TabStop = false;
            this.advgrp.Text = "Advanced Options";
            // 
            // titlelbl
            // 
            this.titlelbl.AutoSize = true;
            this.titlelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titlelbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.titlelbl.Location = new System.Drawing.Point(132, 19);
            this.titlelbl.Name = "titlelbl";
            this.titlelbl.Size = new System.Drawing.Size(192, 24);
            this.titlelbl.TabIndex = 17;
            this.titlelbl.Text = "LOCATION CLIENT";
            // 
            // whorad
            // 
            this.whorad.AutoSize = true;
            this.whorad.Checked = true;
            this.whorad.Location = new System.Drawing.Point(28, 31);
            this.whorad.Name = "whorad";
            this.whorad.Size = new System.Drawing.Size(100, 17);
            this.whorad.TabIndex = 0;
            this.whorad.TabStop = true;
            this.whorad.Text = "Who is Protocol";
            this.whorad.UseVisualStyleBackColor = true;
            // 
            // http9rad
            // 
            this.http9rad.AutoSize = true;
            this.http9rad.Location = new System.Drawing.Point(28, 64);
            this.http9rad.Name = "http9rad";
            this.http9rad.Size = new System.Drawing.Size(72, 17);
            this.http9rad.TabIndex = 1;
            this.http9rad.TabStop = true;
            this.http9rad.Text = "HTTP 0.9";
            this.http9rad.UseVisualStyleBackColor = true;
            // 
            // HTTP0rad
            // 
            this.HTTP0rad.AutoSize = true;
            this.HTTP0rad.Location = new System.Drawing.Point(245, 31);
            this.HTTP0rad.Name = "HTTP0rad";
            this.HTTP0rad.Size = new System.Drawing.Size(72, 17);
            this.HTTP0rad.TabIndex = 2;
            this.HTTP0rad.TabStop = true;
            this.HTTP0rad.Text = "HTTP 1.0";
            this.HTTP0rad.UseVisualStyleBackColor = true;
            // 
            // HTTP1rad
            // 
            this.HTTP1rad.AutoSize = true;
            this.HTTP1rad.Location = new System.Drawing.Point(245, 64);
            this.HTTP1rad.Name = "HTTP1rad";
            this.HTTP1rad.Size = new System.Drawing.Size(72, 17);
            this.HTTP1rad.TabIndex = 3;
            this.HTTP1rad.TabStop = true;
            this.HTTP1rad.Text = "HTTP 1.1";
            this.HTTP1rad.UseVisualStyleBackColor = true;
            // 
            // advimplbl
            // 
            this.advimplbl.AutoSize = true;
            this.advimplbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.advimplbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.advimplbl.Location = new System.Drawing.Point(15, 402);
            this.advimplbl.Name = "advimplbl";
            this.advimplbl.Size = new System.Drawing.Size(102, 17);
            this.advimplbl.TabIndex = 19;
            this.advimplbl.Text = "IMPORTANT!";
            // 
            // advlbl
            // 
            this.advlbl.AutoSize = true;
            this.advlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.advlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.advlbl.Location = new System.Drawing.Point(15, 422);
            this.advlbl.Name = "advlbl";
            this.advlbl.Size = new System.Drawing.Size(294, 34);
            this.advlbl.TabIndex = 18;
            this.advlbl.Text = "Advanced Options are for experienced users.\r\nPlease use at your own risk.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.label1.Location = new System.Drawing.Point(72, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 51);
            this.label1.TabIndex = 20;
            this.label1.Text = "Welcome to the Location Client!\r\n\r\nTo use this client simply enter the details be" +
    "low!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LocationInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(455, 627);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.advimplbl);
            this.Controls.Add(this.advlbl);
            this.Controls.Add(this.titlelbl);
            this.Controls.Add(this.advgrp);
            this.Controls.Add(this.protgrp);
            this.Controls.Add(this.usrtxt);
            this.Controls.Add(this.usrlbl);
            this.Controls.Add(this.loclbl);
            this.Controls.Add(this.loctxt);
            this.Controls.Add(this.implbl);
            this.Controls.Add(this.warnlbl);
            this.Controls.Add(this.finlbl);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "LocationInterface";
            this.ShowIcon = false;
            this.Text = "LocationInterface";
            this.protgrp.ResumeLayout(false);
            this.protgrp.PerformLayout();
            this.advgrp.ResumeLayout(false);
            this.advgrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button finlbl;
        private System.Windows.Forms.Label warnlbl;
        private System.Windows.Forms.Label implbl;
        private System.Windows.Forms.Label addlbl;
        private System.Windows.Forms.TextBox addtxt;
        private System.Windows.Forms.Label portlbl;
        private System.Windows.Forms.TextBox porttxt;
        private System.Windows.Forms.TextBox timetxt;
        private System.Windows.Forms.Label timelbl;
        private System.Windows.Forms.TextBox loctxt;
        private System.Windows.Forms.Label loclbl;
        private System.Windows.Forms.Label usrlbl;
        private System.Windows.Forms.TextBox usrtxt;
        private System.Windows.Forms.GroupBox protgrp;
        private System.Windows.Forms.GroupBox advgrp;
        private System.Windows.Forms.Label titlelbl;
        private System.Windows.Forms.RadioButton whorad;
        private System.Windows.Forms.RadioButton HTTP1rad;
        private System.Windows.Forms.RadioButton HTTP0rad;
        private System.Windows.Forms.RadioButton http9rad;
        private System.Windows.Forms.Label advimplbl;
        private System.Windows.Forms.Label advlbl;
        private System.Windows.Forms.Label label1;
    }
}