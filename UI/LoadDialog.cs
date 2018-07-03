using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ReportManager
{
	public delegate void loadReport(object Arg);
	/// <summary>
	/// Summary description for LoadDialog.
	/// </summary>
	public class LoadDialog : System.Windows.Forms.Form
	{
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbConnection conn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Data.DataSet dataSet1;
		private System.Data.OleDb.OleDbDataAdapter adapter;
		private System.Windows.Forms.Button load;
		private System.Windows.Forms.Button cancel;
		public loadReport LoadReport;
		private System.Windows.Forms.DateTimePicker dateFilter;
		private System.Windows.Forms.TextBox nameFilter;
		private System.Windows.Forms.CheckBox nameCheck;
		private System.Windows.Forms.CheckBox repCheck;
		private System.Windows.Forms.TextBox repFilter;
		private System.Windows.Forms.ComboBox dateCheck;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LoadDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.adapter = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.conn = new System.Data.OleDb.OleDbConnection();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.dataSet1 = new System.Data.DataSet();
			this.load = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.dateFilter = new System.Windows.Forms.DateTimePicker();
			this.nameFilter = new System.Windows.Forms.TextBox();
			this.nameCheck = new System.Windows.Forms.CheckBox();
			this.repCheck = new System.Windows.Forms.CheckBox();
			this.repFilter = new System.Windows.Forms.TextBox();
			this.dateCheck = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// adapter
			// 
			this.adapter.SelectCommand = this.oleDbSelectCommand1;
			this.adapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							  new System.Data.Common.DataTableMapping("Table", "Diagn", new System.Data.Common.DataColumnMapping[] {
																																																	   new System.Data.Common.DataColumnMapping("Dat", "Dat"),
																																																	   new System.Data.Common.DataColumnMapping("FIO", "FIO"),
																																																	   new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																	   new System.Data.Common.DataColumnMapping("Name", "Name")})});
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT ID, FIO, Dat, Name FROM Diagn";
			this.oleDbSelectCommand1.Connection = this.conn;
			// 
			// conn
			// 
			this.conn.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source="".\reps.mdb"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.dataGrid1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(592, 216);
			this.panel1.TabIndex = 0;
			// 
			// dataGrid1
			// 
			this.dataGrid1.ContextMenu = this.contextMenu1;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.DataSource = this.dataSet1;
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.RowHeadersVisible = false;
			this.dataGrid1.Size = new System.Drawing.Size(592, 216);
			this.dataGrid1.TabIndex = 0;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "DataSet";
			this.dataSet1.Locale = new System.Globalization.CultureInfo("ru-RU");
			// 
			// load
			// 
			this.load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.load.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.load.Location = new System.Drawing.Point(104, 272);
			this.load.Name = "load";
			this.load.TabIndex = 1;
			this.load.Text = "Открыть";
			this.load.Click += new System.EventHandler(this.load_Click);
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(400, 272);
			this.cancel.Name = "cancel";
			this.cancel.TabIndex = 2;
			this.cancel.Text = "Отмена";
			// 
			// dateFilter
			// 
			this.dateFilter.Location = new System.Drawing.Point(40, 224);
			this.dateFilter.Name = "dateFilter";
			this.dateFilter.Size = new System.Drawing.Size(128, 20);
			this.dateFilter.TabIndex = 3;
			// 
			// nameFilter
			// 
			this.nameFilter.Location = new System.Drawing.Point(200, 224);
			this.nameFilter.Name = "nameFilter";
			this.nameFilter.Size = new System.Drawing.Size(160, 20);
			this.nameFilter.TabIndex = 4;
			this.nameFilter.Text = "";
			// 
			// nameCheck
			// 
			this.nameCheck.Location = new System.Drawing.Point(184, 224);
			this.nameCheck.Name = "nameCheck";
			this.nameCheck.Size = new System.Drawing.Size(16, 24);
			this.nameCheck.TabIndex = 6;
			this.nameCheck.ThreeState = true;
			this.nameCheck.CheckedChanged += new System.EventHandler(this.check_CheckedChanged);
			// 
			// repCheck
			// 
			this.repCheck.Location = new System.Drawing.Point(376, 224);
			this.repCheck.Name = "repCheck";
			this.repCheck.Size = new System.Drawing.Size(16, 24);
			this.repCheck.TabIndex = 7;
			this.repCheck.ThreeState = true;
			this.repCheck.CheckedChanged += new System.EventHandler(this.check_CheckedChanged);
			// 
			// repFilter
			// 
			this.repFilter.Location = new System.Drawing.Point(392, 224);
			this.repFilter.Name = "repFilter";
			this.repFilter.Size = new System.Drawing.Size(192, 20);
			this.repFilter.TabIndex = 8;
			this.repFilter.Text = "";
			// 
			// dateCheck
			// 
			this.dateCheck.Items.AddRange(new object[] {
														   "",
														   ">=",
														   "<=",
														   "="});
			this.dateCheck.Location = new System.Drawing.Point(0, 224);
			this.dateCheck.Name = "dateCheck";
			this.dateCheck.Size = new System.Drawing.Size(40, 21);
			this.dateCheck.TabIndex = 9;
			this.dateCheck.TextChanged += new System.EventHandler(this.dateCheck_TextChanged);
			this.dateCheck.SelectedIndexChanged += new System.EventHandler(this.check_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 248);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Поиск по дате";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(200, 248);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Поиск по имени";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(392, 248);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(160, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Поиск по типу исследования";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Удалить";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// LoadDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 301);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateCheck);
			this.Controls.Add(this.repFilter);
			this.Controls.Add(this.repCheck);
			this.Controls.Add(this.nameCheck);
			this.Controls.Add(this.nameFilter);
			this.Controls.Add(this.dateFilter);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.load);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "LoadDialog";
			this.Text = "Открыть...";
			this.Load += new System.EventHandler(this.LoadDialog_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadDialog_Load(object sender, System.EventArgs e)
		{
			conn.Open();
			adapter.Fill(dataSet1);
			dataGrid1.DataSource = dataSet1.Tables["Diagn"];
			conn.Close();
		}

		private void load_Click(object sender, System.EventArgs e)
		{
			LoadReport(dataGrid1[dataGrid1.CurrentRowIndex, 0]);
		}

		private void check_CheckedChanged(object sender, System.EventArgs e)
		{
			adapter.SelectCommand.CommandText = "Select ID,FIO,Dat,Name From Diagn ";
			if((dateCheck.Text!="") || nameCheck.Checked || repCheck.Checked)
				adapter.SelectCommand.CommandText += "Where ";
			if(dateCheck.SelectedIndex>0)
			{
				adapter.SelectCommand.CommandText +=
					"Dat"+dateCheck.Text +
					TextManager.DateToSQL(dateFilter.Value.ToShortDateString())+" ";
				if(nameCheck.Checked || repCheck.Checked)
					adapter.SelectCommand.CommandText += "and ";
			}
			if(nameCheck.Checked)
			{
				adapter.SelectCommand.CommandText +=
					"FIO Like \"%"+nameFilter.Text+"%\" ";
				if(repCheck.Checked)
					adapter.SelectCommand.CommandText += "and ";
			}
			if(repCheck.Checked)
			{
				adapter.SelectCommand.CommandText +=
					"Name Like \"%"+repFilter.Text+"%\"";
			}
			adapter.SelectCommand.CommandText += " ";

			dataGrid1.DataSource = null;
			dataSet1.Clear();
			this.LoadDialog_Load(this,null);
		}

		private void dateCheck_TextChanged(object sender, System.EventArgs e)
		{
			if(dateCheck.Items.IndexOf(dateCheck.Text)<0)
				dateCheck.SelectedIndex = 0;
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
