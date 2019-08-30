namespace Toolbox
{
    partial class Form1
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
            this.ok = new System.Windows.Forms.Button();
            this.name_dim0 = new System.Windows.Forms.Label();
            this.val_dim0 = new System.Windows.Forms.TextBox();
            this.val_dim1 = new System.Windows.Forms.TextBox();
            this.val_dim2 = new System.Windows.Forms.TextBox();
            this.val_dim3 = new System.Windows.Forms.TextBox();
            this.name_dim1 = new System.Windows.Forms.Label();
            this.name_dim2 = new System.Windows.Forms.Label();
            this.name_dim3 = new System.Windows.Forms.Label();
            this.unitsGroup = new System.Windows.Forms.GroupBox();
            this.unit2 = new System.Windows.Forms.RadioButton();
            this.unit1 = new System.Windows.Forms.RadioButton();
            this.unit0 = new System.Windows.Forms.RadioButton();
            this.unitsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(383, 12);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.button1_Click);
            // 
            // name_dim0
            // 
            this.name_dim0.AutoSize = true;
            this.name_dim0.Location = new System.Drawing.Point(12, 18);
            this.name_dim0.Name = "name_dim0";
            this.name_dim0.Size = new System.Drawing.Size(73, 17);
            this.name_dim0.TabIndex = 3;
            this.name_dim0.Text = "dim_stub0";
            // 
            // val_dim0
            // 
            this.val_dim0.Location = new System.Drawing.Point(91, 12);
            this.val_dim0.Name = "val_dim0";
            this.val_dim0.Size = new System.Drawing.Size(100, 22);
            this.val_dim0.TabIndex = 5;
            // 
            // val_dim1
            // 
            this.val_dim1.Location = new System.Drawing.Point(91, 40);
            this.val_dim1.Name = "val_dim1";
            this.val_dim1.Size = new System.Drawing.Size(100, 22);
            this.val_dim1.TabIndex = 8;
            // 
            // val_dim2
            // 
            this.val_dim2.Location = new System.Drawing.Point(91, 68);
            this.val_dim2.Name = "val_dim2";
            this.val_dim2.Size = new System.Drawing.Size(100, 22);
            this.val_dim2.TabIndex = 9;
            // 
            // val_dim3
            // 
            this.val_dim3.Location = new System.Drawing.Point(91, 96);
            this.val_dim3.Name = "val_dim3";
            this.val_dim3.Size = new System.Drawing.Size(100, 22);
            this.val_dim3.TabIndex = 10;
            // 
            // name_dim1
            // 
            this.name_dim1.AutoSize = true;
            this.name_dim1.Location = new System.Drawing.Point(12, 46);
            this.name_dim1.Name = "name_dim1";
            this.name_dim1.Size = new System.Drawing.Size(73, 17);
            this.name_dim1.TabIndex = 11;
            this.name_dim1.Text = "dim_stub1";
            // 
            // name_dim2
            // 
            this.name_dim2.AutoSize = true;
            this.name_dim2.Location = new System.Drawing.Point(12, 73);
            this.name_dim2.Name = "name_dim2";
            this.name_dim2.Size = new System.Drawing.Size(73, 17);
            this.name_dim2.TabIndex = 12;
            this.name_dim2.Text = "dim_stub2";
            // 
            // name_dim3
            // 
            this.name_dim3.AutoSize = true;
            this.name_dim3.Location = new System.Drawing.Point(12, 101);
            this.name_dim3.Name = "name_dim3";
            this.name_dim3.Size = new System.Drawing.Size(73, 17);
            this.name_dim3.TabIndex = 13;
            this.name_dim3.Text = "dim_stub3";
            // 
            // unitsGroup
            // 
            this.unitsGroup.Controls.Add(this.unit0);
            this.unitsGroup.Controls.Add(this.unit2);
            this.unitsGroup.Controls.Add(this.unit1);
            this.unitsGroup.Location = new System.Drawing.Point(217, 12);
            this.unitsGroup.Name = "unitsGroup";
            this.unitsGroup.Size = new System.Drawing.Size(148, 106);
            this.unitsGroup.TabIndex = 18;
            this.unitsGroup.TabStop = false;
            this.unitsGroup.Text = "Units";
            // 
            // unit2
            // 
            this.unit2.AutoSize = true;
            this.unit2.Location = new System.Drawing.Point(19, 75);
            this.unit2.Name = "unit2";
            this.unit2.Size = new System.Drawing.Size(110, 21);
            this.unit2.TabIndex = 18;
            this.unit2.TabStop = true;
            this.unit2.Text = "radioButton3";
            this.unit2.UseVisualStyleBackColor = true;
            // 
            // unit1
            // 
            this.unit1.AutoSize = true;
            this.unit1.Location = new System.Drawing.Point(19, 48);
            this.unit1.Name = "unit1";
            this.unit1.Size = new System.Drawing.Size(110, 21);
            this.unit1.TabIndex = 17;
            this.unit1.TabStop = true;
            this.unit1.Text = "radioButton2";
            this.unit1.UseVisualStyleBackColor = true;
            // 
            // unit0
            // 
            this.unit0.AutoSize = true;
            this.unit0.Checked = true;
            this.unit0.Location = new System.Drawing.Point(19, 21);
            this.unit0.Name = "unit0";
            this.unit0.Size = new System.Drawing.Size(110, 21);
            this.unit0.TabIndex = 19;
            this.unit0.TabStop = true;
            this.unit0.Text = "radioButton1";
            this.unit0.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 127);
            this.Controls.Add(this.unitsGroup);
            this.Controls.Add(this.name_dim3);
            this.Controls.Add(this.name_dim2);
            this.Controls.Add(this.name_dim1);
            this.Controls.Add(this.val_dim3);
            this.Controls.Add(this.val_dim2);
            this.Controls.Add(this.val_dim1);
            this.Controls.Add(this.val_dim0);
            this.Controls.Add(this.name_dim0);
            this.Controls.Add(this.ok);
            this.Name = "Form1";
            this.Text = "Form1";
            this.unitsGroup.ResumeLayout(false);
            this.unitsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label name_dim0;
        private System.Windows.Forms.TextBox val_dim0;
        private System.Windows.Forms.TextBox val_dim1;
        private System.Windows.Forms.TextBox val_dim2;
        private System.Windows.Forms.TextBox val_dim3;
        private System.Windows.Forms.Label name_dim1;
        private System.Windows.Forms.Label name_dim2;
        private System.Windows.Forms.Label name_dim3;
        private System.Windows.Forms.GroupBox unitsGroup;
        private System.Windows.Forms.RadioButton unit2;
        private System.Windows.Forms.RadioButton unit1;
        private System.Windows.Forms.RadioButton unit0;
    }
}

