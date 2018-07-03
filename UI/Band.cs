using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ReportManager.Controls;

namespace ReportManager
{
	/// <summary>
	/// Horizontal band for user controls
	/// </summary>
	public class Band : System.Windows.Forms.Panel, Drawable, Clonable, 
		System.Xml.Serialization.IXmlSerializable
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Band()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Band
			// 
			this.Size = new System.Drawing.Size(200, 24);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: Add custom paint code here

			// Calling the base class OnPaint
			base.OnPaint(pe);
			//pe.Graphics.DrawRectangle(Pens.Red,0,0,Width-1,Height-1);
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged (e);
			if(this.Parent!=null)
			{
				this.Width = Parent.Width;
				this.Anchor |= AnchorStyles.Left | AnchorStyles.Right;
				//this.Dock = DockStyle.Top;
			}
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded (e);
			int h = e.Control.Top+e.Control.Height;
			if(this.Height<h)
				this.Height = h;
		}

		public override string ToString()
		{
			string ret = "";
			SortedList lst = new SortedList();
			foreach(Control c in Controls)
			{
				lst.Add(c.Left,c.Text);
			}
			for(int i = 0; i<lst.Count; ++i)
				ret = ret + lst.GetByIndex(i);
			return ret;
		}

		#region Drawable Members

		public void Draw(Graphics e)
		{
			foreach(Drawable d in this.Controls)
			{
				d.Draw(e);
			}
		}

		#endregion

		#region Clonable Members

		public Control Clone()
		{
			Band ret = new Band();
			ret.Width = Width;
			ret.Height = Height;
			ret.Top = Top;
			ret.Left = Left;
			ret.TabIndex = TabIndex;
			foreach(Clonable c in this.Controls)
				ret.Controls.Add(c.Clone());
			return ret;
		}

		#endregion

		#region IXmlSerializable Members

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			writer.WriteAttributeString("Width",Width.ToString());
			writer.WriteAttributeString("Height",Height.ToString());
			writer.WriteAttributeString("Top",Top.ToString());
			writer.WriteAttributeString("Left",Left.ToString());
			writer.WriteAttributeString("TabIndex",TabIndex.ToString());
			writer.WriteAttributeString("ControlsCount",this.Controls.Count.ToString());
			for(int i = 0; i<Controls.Count; ++i)
			{
				System.Xml.Serialization.IXmlSerializable x = 
					Controls[i] as System.Xml.Serialization.IXmlSerializable;
				writer.WriteStartElement(Controls[i].GetType().ToString());
				if(x!=null)
				{
					x.WriteXml(writer);
				}
				writer.WriteEndElement();
			}
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			System.Xml.Serialization.IXmlSerializable cntrl;
			string s;
			s = reader.GetAttribute("Width");
			if(s!=null)
				Width = Int32.Parse(s);
			s = reader.GetAttribute("Height");
			if(s!=null)
				Height = Int32.Parse(s);
			s = reader.GetAttribute("Top");
			if(s!=null)
				Top = Int32.Parse(s);
			s = reader.GetAttribute("Left");
			if(s!=null)
				Left = Int32.Parse(s);
			s = reader.GetAttribute("TabIndex");
			if(s!=null)
				TabIndex = Int32.Parse(s);
			s = reader.GetAttribute("ControlsCount");
			int c = 0;
			if(s!=null)
				c = Int32.Parse(s);

			reader.ReadStartElement();
			for(int i = 0; i<c; ++i)
			{
				s = reader.LocalName;
				if(reader.IsStartElement("ReportManager.Controls.SLabel"))
					cntrl = new ReportManager.Controls.SLabel();
				else if(reader.IsStartElement("ReportManager.Controls.STextBox"))
					cntrl = new ReportManager.Controls.STextBox();
				else if(reader.IsStartElement("ReportManager.Controls.SComboBox"))
					cntrl = new ReportManager.Controls.SComboBox();
				else if(reader.IsStartElement("ReportManager.Controls.SMTextBox"))
					cntrl = new ReportManager.Controls.SMTextBox();
				else cntrl = null;
				cntrl.ReadXml(reader);
				s = reader.LocalName;
				this.Controls.Add(cntrl as Control);
			}
			try
			{
				reader.ReadEndElement();
			}
			catch(System.Xml.XmlException e)
			{
				s = e.StackTrace;
			}
			s = reader.LocalName;

		}

		#endregion
	}
}
