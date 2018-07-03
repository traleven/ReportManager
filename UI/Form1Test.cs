using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace ReportManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1Test : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private Report r;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private ReportBuilder builder;
		private System.Windows.Forms.MenuItem newRepMenu;
		private System.Windows.Forms.MenuItem printMenu;
		private System.Windows.Forms.MenuItem prntPrevMenu;
		private System.Windows.Forms.MenuItem prntPrintMenu;
		private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbConnection oleDbConnection1;
		private System.Windows.Forms.MenuItem repMenu;
		private System.Windows.Forms.MenuItem saveRepMenu;
		private System.Windows.Forms.MenuItem loadRepMenu;
		private System.Windows.Forms.MenuItem prntPrntMenu;
		private Report activeReport;
		private Report printRep;

		private int pagenum;

		public Form1Test()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			newRepMenu.MenuItems.AddRange(MenuBuilder.LoadMenu(".//menu.ini"));
			foreach(MenuItem t in newRepMenu.MenuItems)
				t.Click += new EventHandler(createReport_Click);
			builder = new ReportBuilder();
			activeReport = null;
			/*/
			activeReport = new Report();
			this.Controls.Add(activeReport);
			new TextManager().Save(activeReport,activeReport.Tag);
			/*/
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1Test));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.newRepMenu = new System.Windows.Forms.MenuItem();
			this.repMenu = new System.Windows.Forms.MenuItem();
			this.saveRepMenu = new System.Windows.Forms.MenuItem();
			this.loadRepMenu = new System.Windows.Forms.MenuItem();
			this.printMenu = new System.Windows.Forms.MenuItem();
			this.prntPrevMenu = new System.Windows.Forms.MenuItem();
			this.prntPrintMenu = new System.Windows.Forms.MenuItem();
			this.prntPrntMenu = new System.Windows.Forms.MenuItem();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.newRepMenu,
																					  this.repMenu,
																					  this.printMenu});
			// 
			// newRepMenu
			// 
			this.newRepMenu.Index = 0;
			this.newRepMenu.Text = "Открыть бланк";
			// 
			// repMenu
			// 
			this.repMenu.Index = 1;
			this.repMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.saveRepMenu,
																					this.loadRepMenu});
			this.repMenu.Text = "Заключение";
			// 
			// saveRepMenu
			// 
			this.saveRepMenu.Index = 0;
			this.saveRepMenu.Text = "Сохранить";
			this.saveRepMenu.Click += new System.EventHandler(this.saveRepMenu_Click);
			// 
			// loadRepMenu
			// 
			this.loadRepMenu.Index = 1;
			this.loadRepMenu.Text = "Открыть...";
			this.loadRepMenu.Click += new System.EventHandler(this.loadRepMenu_Click);
			// 
			// printMenu
			// 
			this.printMenu.Index = 2;
			this.printMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.prntPrevMenu,
																					  this.prntPrintMenu,
																					  this.prntPrntMenu});
			this.printMenu.Text = "Печать";
			// 
			// prntPrevMenu
			// 
			this.prntPrevMenu.Index = 0;
			this.prntPrevMenu.Text = "Посмотреть...";
			this.prntPrevMenu.Click += new System.EventHandler(this.prntPrevMenu_Click);
			// 
			// prntPrintMenu
			// 
			this.prntPrintMenu.Index = 1;
			this.prntPrintMenu.Text = "Печатать...";
			this.prntPrintMenu.Click += new System.EventHandler(this.prntPrintMenu_Click);
			// 
			// prntPrntMenu
			// 
			this.prntPrntMenu.Index = 2;
			this.prntPrntMenu.Text = "Печатать";
			this.prntPrntMenu.Click += new System.EventHandler(this.prntPrntMenu_Click);
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// printDialog1
			// 
			this.printDialog1.Document = this.printDocument1;
			// 
			// printPreviewDialog1
			// 
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Document = this.printDocument1;
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
			this.printPreviewDialog1.Location = new System.Drawing.Point(661, 19);
			this.printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty;
			this.printPreviewDialog1.Visible = false;
			// 
			// oleDbDataAdapter1
			// 
			this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
			this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "Diagn", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("FIO", "FIO"),
																																																				 new System.Data.Common.DataColumnMapping("Dat", "Dat"),
																																																				 new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																				 new System.Data.Common.DataColumnMapping("Path", "Path"),
																																																				 new System.Data.Common.DataColumnMapping("Text", "Text")})});
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT Diagn.FIO, Diagn.Dat, Report.Name, Report.Path, Diagn.[Text] FROM (Diagn I" +
				"NNER JOIN Report ON Diagn.RepID = Report.ID)";
			this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
			// 
			// oleDbConnection1
			// 
			this.oleDbConnection1.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source="".\reps.mdb"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
			// 
			// Form1Test
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(592, 273);
			this.Menu = this.mainMenu1;
			this.Name = "Form1Test";
			this.Text = "Заключение УЗИ";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);

		}
		#endregion

		private void createReport_Click(object sender, System.EventArgs e)
		{
			builder.Build( (string)((MenuItemTagged)sender).Tag );
			if(activeReport!=null)
				this.Controls.Remove(activeReport);
			activeReport = builder.Report;
			activeReport.Location = new Point((this.Width-activeReport.Width)/2,0);
			this.Controls.Add(activeReport);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			MouseEventArgs a = new MouseEventArgs(e.Button,e.Clicks,e.X,e.Y,0);
			base.OnMouseWheel (a);
		}


		private void prntPrevMenu_Click(object sender, System.EventArgs e)
		{
			pagenum = 0;
			if(activeReport!=null)
			{
				printRep = ReportBuilder.ReloadRep(activeReport,true);
				/**
				this.Controls.Remove(activeReport);
				activeReport = printRep;
				this.Controls.Add(activeReport);
				**/
				printPreviewDialog1.ShowDialog(this);
			}
		}

		private void prntPrintMenu_Click(object sender, System.EventArgs e)
		{
			pagenum = 0;
			if(activeReport!=null)
				if(printDialog1.ShowDialog(this) == DialogResult.OK)
				{
					printRep = ReportBuilder.ReloadRep(activeReport,true);
					printDocument1.Print();
				}
		}


		private void prntPrntMenu_Click(object sender, System.EventArgs e)
		{
			pagenum = 0;
			if(activeReport!=null)
			{
				printRep = ReportBuilder.ReloadRep(activeReport,true);
				try
				{
					printDocument1.Print();
				}
				catch
				{
					MessageBox.Show(this,"Printing error",e.ToString(),MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
			}
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.HasMorePages = printRep.DrawGraphics(e.Graphics,pagenum++);;
		}

		private void saveRepMenu_Click(object sender, System.EventArgs e)
		{
			if(activeReport!=null)
				save(activeReport, oleDbConnection1);
		}

		private void save(Report r, System.Data.OleDb.OleDbConnection con)
		{
			string cmd;
			TextManager mng = new TextManager();
			string[] text = new string[r.Items.Count];
			cmd = "Insert into Diagn (FIO, Dat, Name) Values (";
			cmd += "\""+r.FIO+"\", ";
			cmd += "\""+r.Date.ToShortDateString()+"\", ";
			cmd += "\""+(string)r.Tag+"\");";
			con.Open();
			System.Data.OleDb.OleDbCommand c = 
				new System.Data.OleDb.OleDbCommand(cmd,con);
			c.ExecuteNonQuery();
			cmd = "Select max(ID) From Diagn;";
			c = new System.Data.OleDb.OleDbCommand(cmd,con);
			mng.Save(r,c.ExecuteScalar());
			con.Close();
		}

		private void loadRep(object Arg)
		{
			builder.Build(".\\Reports\\"+Arg.ToString()+".rep");
		}

		private void loadRepMenu_Click(object sender, System.EventArgs e)
		{
			LoadDialog dialog = new LoadDialog();
			dialog.LoadReport = new loadReport(loadRep);
			if(dialog.ShowDialog(this) == DialogResult.OK)
			{
				this.SuspendLayout();
				if(activeReport!=null)
					this.Controls.Remove(activeReport);
				activeReport = builder.Report;
				activeReport.Location = new Point((this.Width-activeReport.Width)/2,activeReport.Top);
				this.Controls.Add(activeReport);
				this.ResumeLayout(false);
			}
		}

		private void Form1_SizeChanged(object sender, System.EventArgs e)
		{
			if(activeReport!=null)
				activeReport.Location = new Point((this.Width-activeReport.Width)/2,activeReport.Top);
		}
	}
}
