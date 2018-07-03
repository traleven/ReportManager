using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace ReportManager
{
	/// <summary>
	/// Report GUI control
	/// </summary>
	[Serializable()]
	public class Report: 
		System.Windows.Forms.UserControl, System.Runtime.Serialization.ISerializable
	{
		public ArrayList Items;
		public BandList Bands;
		public const int PageHeight = 940;
		public const double PageScaleY = /*0.75*/1.0;

		#region Properties
		public string FIO
		{
			get
			{
				foreach(Control t in Items)
					if(t.Name == "fio")
						return t.Text;
				return "";
			}
		}

		public System.DateTime Date
		{
			get{return DateTime.Today;}
		}
		#endregion

		public Report():base()
		{
			Items = new ArrayList();
			Bands = new BandList();
			//
			// Required for Windows Form Designer support
			//
			//InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			foreach(Control t in this.Controls)
				Items.Add(t);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (Items != null) 
				{
					Items.Clear();
					Bands.Clear();
				}
			}
			base.Dispose( disposing );
		}

		#region ISerializable

		public Report(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context):base()
		{
			Control t;
			Band b;
			int h;
			Bands = new BandList();

			this.SuspendLayout();
			this.Tag = (string)info.GetValue("Name",typeof(string));
			this.BackColor = (Color)info.GetValue("BGColor",typeof(Color));
			this.Width = (int)info.GetValue("Width",Type.GetType("System.Int32"));
			this.Height = (int)info.GetValue("Height",Type.GetType("System.Int32"));
			this.Items = (ArrayList)info.GetValue(
				"Items",Type.GetType("System.Collections.ArrayList"));
			for(int i=0; i<this.Items.Count; i++)
			{
				
				t = (Control)info.GetValue("Items["+i.ToString()+"]",typeof(Control));
				if(t.GetType()==typeof(ReportManager.Controls.SMTextBox))
					((ReportManager.Controls.SMTextBox)t).MoveNext += 
						new Controls.MoveNextControls(shd);
				if(t.Text.CompareTo("today")==0)
				{
					t.Text = DateTime.Now.ToShortDateString();
				}
				if(t.Name=="docs")
				{
					((ComboBox)t).Items.Clear();
					((ComboBox)t).Items.AddRange(TextManager.StringsFromFile(".\\docs.cfg"));
				}
				this.Items[i] = (System.Windows.Forms.Control)t;
				/**/
				h = t.Top;
				b = this.Bands.GetBandAtHeight(h);
				t.Top = 0;
				b.Controls.Add(t);
				/*if(!this.Controls.Contains(b))
					this.Controls.Add(b);*/
				/*
				this.Controls.Add(t);*/
			}
			Bands.SortByPosition();
			for(int i = 0; i<Bands.Count; i++)
			{
				//Bands[i].Dock = DockStyle.Top;
				this.Controls.Add(Bands[i]);
			}
			this.ResumeLayout(false);
		}

		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context)
		{
			//for(int i=0; i<Items.Count; i++)
			//	if(Items[i].GetType()==typeof(Controls.SMTextBox))
			//		((Controls.SMTextBox)Items[i]).PrepareToSerialize();

			info.AddValue("Name",this.Tag);
			info.AddValue("BGColor",this.BackColor);
			info.AddValue("Width",this.Width);
			info.AddValue("Height",this.Height);
			info.AddValue("Items",this.Items);
			for(int i=0; i<Items.Count; i++)
				info.AddValue("Items["+i.ToString()+"]",this.Items[i]);
		}

		#endregion

		public bool DrawGraphics(System.Drawing.Graphics e, int page)
		{
			bool more = false;
			e.Clear(System.Drawing.Color.White);
			foreach(Controls.Drawable t in Bands)
			{
				if(((Control)t).Top*PageScaleY>=page*PageHeight)
					if(((Control)t).Top*PageScaleY<(page+1)*PageHeight)
						t.Draw(e);
					else
						more = true;
			}
			return more;
		}

		private void shd(int top, int d)
		{
			//this.SuspendLayout();
			foreach(Band t in this.Bands)
				if(t.Top>top)
					t.Top += d;
			//Bands.GetBandAtHeight(top).Height += d;
			int y = this.Top;
			this.Height += d;
			if(this.ParentForm!=null)
				(this.ParentForm).AutoScrollPosition.Offset(0,d);
			//this.Top = y-d;
			//this.Height = 10000;//
			//this.ResumeLayout(false);
		}

		public void AddControlsEvents()
		{
			ReportManager.Controls.MoveNextControls c = new ReportManager.Controls.MoveNextControls(shd);
			foreach(Band b in this.Bands)
			{
				foreach(Control t in b.Controls)
					if(t is ReportManager.Controls.SMTextBox)
						((ReportManager.Controls.SMTextBox)t).MoveNext = c;
			}
		}

		public Report Clone()
		{
			Report t = new Report();
			Control c;
			foreach(Controls.Clonable tmp in this.Bands)
			{
				c = tmp.Clone();
				if(c.GetType()==typeof(Controls.SMTextBox))
					((Controls.SMTextBox)c).MoveNext += 
						new Controls.MoveNextControls(t.shd);
				t.Bands.Add(c as Band);
				t.Controls.Add(c);
			}
			t.Location = this.Location;
			t.Size = this.Size;
			t.BackColor = this.BackColor;
			t.Tag = this.Tag;

			return t;
		}
	}
}
