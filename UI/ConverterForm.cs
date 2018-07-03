using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml.Serialization;


namespace ReportManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Converter : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckedListBox clbSrc;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.CheckedListBox clbDst;
		private System.Windows.Forms.Button btnOpenFolder;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Converter()
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
			this.clbSrc = new System.Windows.Forms.CheckedListBox();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.clbDst = new System.Windows.Forms.CheckedListBox();
			this.btnOpenFolder = new System.Windows.Forms.Button();
			this.btnRun = new System.Windows.Forms.Button();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// clbSrc
			// 
			this.clbSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.clbSrc.Location = new System.Drawing.Point(8, 32);
			this.clbSrc.Name = "clbSrc";
			this.clbSrc.Size = new System.Drawing.Size(216, 229);
			this.clbSrc.TabIndex = 0;
			// 
			// txtFolder
			// 
			this.txtFolder.Location = new System.Drawing.Point(8, 8);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(440, 20);
			this.txtFolder.TabIndex = 1;
			this.txtFolder.Text = ".";
			this.txtFolder.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
			// 
			// clbDst
			// 
			this.clbDst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.clbDst.Location = new System.Drawing.Point(272, 32);
			this.clbDst.Name = "clbDst";
			this.clbDst.Size = new System.Drawing.Size(216, 229);
			this.clbDst.TabIndex = 2;
			// 
			// btnOpenFolder
			// 
			this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFolder.Location = new System.Drawing.Point(456, 8);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(32, 23);
			this.btnOpenFolder.TabIndex = 3;
			this.btnOpenFolder.Text = "...";
			this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.btnRun.Location = new System.Drawing.Point(232, 32);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(32, 232);
			this.btnRun.TabIndex = 4;
			this.btnRun.Text = ">>";
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// fbd
			// 
			this.fbd.SelectedPath = ".";
			// 
			// Converter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 273);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.btnOpenFolder);
			this.Controls.Add(this.clbDst);
			this.Controls.Add(this.txtFolder);
			this.Controls.Add(this.clbSrc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Converter";
			this.Text = "Converter";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOpenFolder_Click(object sender, System.EventArgs e)
		{
			if(fbd.ShowDialog(this)==DialogResult.OK)
			{
				txtFolder.Text = fbd.SelectedPath;
			}
		}

		private void txtFolder_TextChanged(object sender, System.EventArgs e)
		{
			if(System.IO.Directory.Exists(txtFolder.Text))
			{
				clbSrc.Items.Clear();
				System.IO.FileInfo[] files = new System.IO.DirectoryInfo(txtFolder.Text).GetFiles("*.bin");
				clbSrc.Items.AddRange(files);
				clbDst.Items.Clear();
				clbDst.Items.AddRange(files);
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			txtFolder.Text = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			fbd.SelectedPath = txtFolder.Text;
		}

		private void btnRun_Click(object sender, System.EventArgs e)
		{
			foreach(System.IO.FileInfo f in clbSrc.CheckedItems)
			{
				//------------------------------------------------------------------//
				System.IO.Stream stream = null;
				try
				{
					ReportBuilder builder = new ReportBuilder();
					Report activeReport;
					builder.Build( f.FullName );
					activeReport = builder.Report;
					activeReport.Location = new Point((this.Width-activeReport.Width)/2,0);
					//***//
					stream = new System.IO.FileStream(fbd.SelectedPath+"\\"+f.Name+".xml", System.IO.FileMode.Create);
					//ReportBuilder rb = new ReportBuilder(r);
					XmlSerializer xml = new XmlSerializer(/*rb*/builder.GetType());
					xml.Serialize(stream,/*rb*/builder);
					stream.Close();
					//------------------------------------------------------------------//
					clbDst.SetItemChecked(clbDst.FindStringExact(f.Name),true);
				}
				catch(Exception err)
				{
					if(stream!=null)
						if(stream.CanWrite)
							stream.Close();
				}
			}
		}
	}
}
