
using System.Drawing;
using System.Windows.Forms;

namespace CopyDel
{
    partial class TestClick
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
            this.TestBtn1 = new System.Windows.Forms.Button();
            this.TestBtn2 = new System.Windows.Forms.Button();
            this.ChkBox = new System.Windows.Forms.CheckBox();
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TestBtn1
            // 
            this.TestBtn1.Location = new System.Drawing.Point(424, 10);
            this.TestBtn1.Name = "TestBtn1";
            this.TestBtn1.Size = new System.Drawing.Size(82, 23);
            this.TestBtn1.TabIndex = 31;
            this.TestBtn1.Text = "Test";
            this.TestBtn1.UseVisualStyleBackColor = true;
            this.TestBtn1.Click += new System.EventHandler(this.TestBtn1_Click);
            // 
            // TestBtn2
            // 
            this.TestBtn2.Location = new System.Drawing.Point(11, 10);
            this.TestBtn2.Name = "TestBtn2";
            this.TestBtn2.Size = new System.Drawing.Size(82, 23);
            this.TestBtn2.TabIndex = 32;
            this.TestBtn2.Text = "Test";
            this.TestBtn2.UseVisualStyleBackColor = true;
            this.TestBtn2.Click += new System.EventHandler(this.TestBtn2_Click);
            // 
            // ChkBox
            // 
            this.ChkBox.AutoSize = true;
            this.ChkBox.Location = new System.Drawing.Point(161, 14);
            this.ChkBox.Name = "ChkBox";
            this.ChkBox.Size = new System.Drawing.Size(105, 17);
            this.ChkBox.TabIndex = 42;
            this.ChkBox.Text = "Aditional Options";
            this.ChkBox.UseVisualStyleBackColor = true;
            // 
            // RTB
            // 
            this.RTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB.Location = new System.Drawing.Point(10, 492);
            this.RTB.Name = "RTB";
            this.RTB.Size = new System.Drawing.Size(497, 83);
            this.RTB.TabIndex = 43;
            this.RTB.Text = "";
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.Location = new System.Drawing.Point(11, 54);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(495, 432);
            this.picBox.TabIndex = 44;
            this.picBox.TabStop = false;
            // 
            // TestClick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 585);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.RTB);
            this.Controls.Add(this.ChkBox);
            this.Controls.Add(this.TestBtn2);
            this.Controls.Add(this.TestBtn1);
            this.Name = "TestClick";
            this.Text = "TestClick";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button TestBtn1;
        private Button TestBtn2;
        private CheckBox ChkBox;
        private RichTextBox RTB;
        private PictureBox picBox;
    }
}