﻿namespace CsharpSelenium
{
    partial class 琴房预约查找
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 90);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(854, 404);
            this.listView1.TabIndex = 149;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "符合网址";
            this.columnHeader2.Width = 1000;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 497);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入网址(一行一个)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(77, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(242, 21);
            this.textBox2.TabIndex = 171;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 170;
            this.label1.Text = "关键字：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(462, 31);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 31);
            this.button3.TabIndex = 169;
            this.button3.Text = "停止";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(650, 31);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 31);
            this.button5.TabIndex = 167;
            this.button5.Text = "清空数据";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(556, 31);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 31);
            this.button4.TabIndex = 166;
            this.button4.Text = "导出数据";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(368, 31);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(88, 31);
            this.button6.TabIndex = 163;
            this.button6.Text = "开始查询";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(744, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 31);
            this.button1.TabIndex = 172;
            this.button1.Text = "复制数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 琴房预约查找
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 497);
            this.Controls.Add(this.groupBox1);
            this.Name = "琴房预约查找";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "琴房预约查找";
            this.Load += new System.EventHandler(this.琴房预约查找_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}