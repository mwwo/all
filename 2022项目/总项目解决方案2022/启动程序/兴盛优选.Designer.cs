﻿namespace 启动程序
{
	// Token: 0x0200000A RID: 10
	public partial class 兴盛优选 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000C528 File Offset: 0x0000A728
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.openFileDialog1 = new global::System.Windows.Forms.OpenFileDialog();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.重新扫描ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.复制串码ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.复制网址ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new global::System.Windows.Forms.ColumnHeader();
			this.label1 = new global::System.Windows.Forms.Label();
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.button3 = new global::System.Windows.Forms.Button();
			this.button4 = new global::System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.openFileDialog1.FileName = "openFileDialog1";
			this.textBox1.Location = new global::System.Drawing.Point(88, 412);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new global::System.Drawing.Size(145, 22);
			this.textBox1.TabIndex = 37;
			this.重新扫描ToolStripMenuItem.Name = "重新扫描ToolStripMenuItem";
			this.重新扫描ToolStripMenuItem.Size = new global::System.Drawing.Size(124, 22);
			this.重新扫描ToolStripMenuItem.Text = "重新扫描";
			this.复制串码ToolStripMenuItem.Name = "复制串码ToolStripMenuItem";
			this.复制串码ToolStripMenuItem.Size = new global::System.Drawing.Size(124, 22);
			this.复制串码ToolStripMenuItem.Text = "复制串码";
			this.复制网址ToolStripMenuItem.Name = "复制网址ToolStripMenuItem";
			this.复制网址ToolStripMenuItem.Size = new global::System.Drawing.Size(124, 22);
			this.复制网址ToolStripMenuItem.Text = "复制网址";
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.复制网址ToolStripMenuItem,
				this.复制串码ToolStripMenuItem,
				this.重新扫描ToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(125, 70);
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3,
				this.columnHeader4,
				this.columnHeader5
			});
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new global::System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(800, 398);
			this.listView1.TabIndex = 34;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.columnHeader1.Text = "序号";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "店名";
			this.columnHeader2.Width = 150;
			this.columnHeader3.Text = "联系人";
			this.columnHeader3.Width = 100;
			this.columnHeader4.Text = "电话";
			this.columnHeader4.Width = 100;
			this.columnHeader5.Text = "地址";
			this.columnHeader5.Width = 200;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(5, 415);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(77, 12);
			this.label1.TabIndex = 40;
			this.label1.Text = "输入关键字：";
			this.button1.Location = new global::System.Drawing.Point(251, 404);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(133, 34);
			this.button1.TabIndex = 39;
			this.button1.Text = "开始";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.button2.Location = new global::System.Drawing.Point(390, 404);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(133, 34);
			this.button2.TabIndex = 41;
			this.button2.Text = "停止";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new global::System.EventHandler(this.button2_Click);
			this.button3.Location = new global::System.Drawing.Point(529, 404);
			this.button3.Name = "button3";
			this.button3.Size = new global::System.Drawing.Size(133, 34);
			this.button3.TabIndex = 42;
			this.button3.Text = "导出";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new global::System.EventHandler(this.button3_Click);
			this.button4.Location = new global::System.Drawing.Point(668, 404);
			this.button4.Name = "button4";
			this.button4.Size = new global::System.Drawing.Size(120, 34);
			this.button4.TabIndex = 43;
			this.button4.Text = "清空";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new global::System.EventHandler(this.button4_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(800, 450);
			base.Controls.Add(this.button4);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			base.Name = "兴盛优选";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "兴盛优选";
			base.Load += new global::System.EventHandler(this.兴盛优选_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000102 RID: 258
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000103 RID: 259
		private global::System.Windows.Forms.OpenFileDialog openFileDialog1;

		// Token: 0x04000104 RID: 260
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x04000105 RID: 261
		private global::System.Windows.Forms.ToolStripMenuItem 重新扫描ToolStripMenuItem;

		// Token: 0x04000106 RID: 262
		private global::System.Windows.Forms.ToolStripMenuItem 复制串码ToolStripMenuItem;

		// Token: 0x04000107 RID: 263
		private global::System.Windows.Forms.ToolStripMenuItem 复制网址ToolStripMenuItem;

		// Token: 0x04000108 RID: 264
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000109 RID: 265
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x0400010A RID: 266
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x0400010B RID: 267
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x0400010C RID: 268
		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		// Token: 0x0400010D RID: 269
		private global::System.Windows.Forms.ColumnHeader columnHeader4;

		// Token: 0x0400010E RID: 270
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400010F RID: 271
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000110 RID: 272
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.Button button3;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.ColumnHeader columnHeader5;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.Button button4;
	}
}
