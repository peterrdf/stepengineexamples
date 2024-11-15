namespace STEPExample
{
    partial class STEPExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(STEPExample));
            this.buttonPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.buttonFind3DModel = new System.Windows.Forms.Button();
            this.buttonFind3DParts = new System.Windows.Forms.Button();
            this.buttonFindAssemblies = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(522, 10);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(75, 23);
            this.buttonPath.TabIndex = 0;
            this.buttonPath.Text = "...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "STEP file ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxContent
            // 
            this.textBoxContent.Location = new System.Drawing.Point(73, 13);
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.Size = new System.Drawing.Size(441, 20);
            this.textBoxContent.TabIndex = 2;
            // 
            // buttonFind3DModel
            // 
            this.buttonFind3DModel.Location = new System.Drawing.Point(12, 39);
            this.buttonFind3DModel.Name = "buttonFind3DModel";
            this.buttonFind3DModel.Size = new System.Drawing.Size(191, 20);
            this.buttonFind3DModel.TabIndex = 3;
            this.buttonFind3DModel.Text = "Find 3D Model";
            this.buttonFind3DModel.UseVisualStyleBackColor = true;
            this.buttonFind3DModel.Click += new System.EventHandler(this.buttonFind3DModel_Click);
            // 
            // buttonFind3DParts
            // 
            this.buttonFind3DParts.Location = new System.Drawing.Point(209, 39);
            this.buttonFind3DParts.Name = "buttonFind3DParts";
            this.buttonFind3DParts.Size = new System.Drawing.Size(191, 20);
            this.buttonFind3DParts.TabIndex = 4;
            this.buttonFind3DParts.Text = "Find 3D Parts";
            this.buttonFind3DParts.UseVisualStyleBackColor = true;
            this.buttonFind3DParts.Click += new System.EventHandler(this.buttonFind3DParts_Click);
            // 
            // buttonFindAssemblies
            // 
            this.buttonFindAssemblies.Location = new System.Drawing.Point(406, 39);
            this.buttonFindAssemblies.Name = "buttonFindAssemblies";
            this.buttonFindAssemblies.Size = new System.Drawing.Size(191, 20);
            this.buttonFindAssemblies.TabIndex = 5;
            this.buttonFindAssemblies.Text = "Find Assemblies";
            this.buttonFindAssemblies.UseVisualStyleBackColor = true;
            this.buttonFindAssemblies.Click += new System.EventHandler(this.buttonFindAssemblies_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 67);
            this.Controls.Add(this.buttonFindAssemblies);
            this.Controls.Add(this.buttonFind3DParts);
            this.Controls.Add(this.buttonFind3DModel);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "STEPExample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.Button buttonFind3DModel;
        private System.Windows.Forms.Button buttonFind3DParts;
        private System.Windows.Forms.Button buttonFindAssemblies;
    }
}

