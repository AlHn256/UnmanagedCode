
using System.Drawing;
using System.Windows.Forms;

namespace UnmanagedCode
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.DelScrin = new System.Windows.Forms.Button();
            this.TestBtn = new System.Windows.Forms.Button();
            this.GetPixelBtn = new System.Windows.Forms.Button();
            this.XTxtBox = new System.Windows.Forms.TextBox();
            this.YTxtBox = new System.Windows.Forms.TextBox();
            this.LbTxt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TestBtn1
            // 
            this.TestBtn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TestBtn1.Location = new System.Drawing.Point(811, 10);
            this.TestBtn1.Name = "TestBtn1";
            this.TestBtn1.Size = new System.Drawing.Size(82, 23);
            this.TestBtn1.TabIndex = 31;
            this.TestBtn1.Text = "Mouse event";
            this.TestBtn1.UseVisualStyleBackColor = true;
            this.TestBtn1.Click += new System.EventHandler(this.TestBtn1_Click);
            // 
            // TestBtn2
            // 
            this.TestBtn2.Location = new System.Drawing.Point(106, 10);
            this.TestBtn2.Name = "TestBtn2";
            this.TestBtn2.Size = new System.Drawing.Size(56, 23);
            this.TestBtn2.TabIndex = 32;
            this.TestBtn2.Text = "Scan";
            this.TestBtn2.UseVisualStyleBackColor = true;
            this.TestBtn2.Click += new System.EventHandler(this.TestBtn2_Click);
            // 
            // ChkBox
            // 
            this.ChkBox.AutoSize = true;
            this.ChkBox.Location = new System.Drawing.Point(12, 14);
            this.ChkBox.Name = "ChkBox";
            this.ChkBox.Size = new System.Drawing.Size(88, 17);
            this.ChkBox.TabIndex = 42;
            this.ChkBox.Text = "Load Params";
            this.ChkBox.UseVisualStyleBackColor = true;
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.Location = new System.Drawing.Point(11, 39);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(883, 1010);
            this.picBox.TabIndex = 44;
            this.picBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(697, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Get";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // DelScrin
            // 
            this.DelScrin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DelScrin.Location = new System.Drawing.Point(742, 10);
            this.DelScrin.Name = "DelScrin";
            this.DelScrin.Size = new System.Drawing.Size(63, 23);
            this.DelScrin.TabIndex = 46;
            this.DelScrin.Text = "Del Scrin";
            this.DelScrin.UseVisualStyleBackColor = true;
            this.DelScrin.Click += new System.EventHandler(this.DelScrin_Click);
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(168, 10);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(56, 23);
            this.TestBtn.TabIndex = 47;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // GetPixelBtn
            // 
            this.GetPixelBtn.Location = new System.Drawing.Point(629, 10);
            this.GetPixelBtn.Name = "GetPixelBtn";
            this.GetPixelBtn.Size = new System.Drawing.Size(62, 23);
            this.GetPixelBtn.TabIndex = 48;
            this.GetPixelBtn.Text = "Get Pixel";
            this.GetPixelBtn.UseVisualStyleBackColor = true;
            this.GetPixelBtn.Click += new System.EventHandler(this.GetPixelBtn_Click);
            // 
            // XTxtBox
            // 
            this.XTxtBox.Location = new System.Drawing.Point(535, 11);
            this.XTxtBox.Name = "XTxtBox";
            this.XTxtBox.Size = new System.Drawing.Size(41, 20);
            this.XTxtBox.TabIndex = 49;
            this.XTxtBox.Text = "0";
            // 
            // YTxtBox
            // 
            this.YTxtBox.Location = new System.Drawing.Point(582, 11);
            this.YTxtBox.Name = "YTxtBox";
            this.YTxtBox.Size = new System.Drawing.Size(41, 20);
            this.YTxtBox.TabIndex = 50;
            this.YTxtBox.Text = "0";
            // 
            // LbTxt
            // 
            this.LbTxt.AutoSize = true;
            this.LbTxt.Location = new System.Drawing.Point(416, 15);
            this.LbTxt.Name = "LbTxt";
            this.LbTxt.Size = new System.Drawing.Size(0, 13);
            this.LbTxt.TabIndex = 51;
            // 
            // TestClick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 1061);
            this.Controls.Add(this.LbTxt);
            this.Controls.Add(this.YTxtBox);
            this.Controls.Add(this.XTxtBox);
            this.Controls.Add(this.GetPixelBtn);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.DelScrin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picBox);
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
        private PictureBox picBox;
        private Button button1;
        private Button DelScrin;
        private Button TestBtn;
        private Button GetPixelBtn;
        private TextBox XTxtBox;
        private TextBox YTxtBox;
        private Label LbTxt;
    }
}