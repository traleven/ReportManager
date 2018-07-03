using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ReportManager.Controls
{
	/// <summary>
	/// Summary description for SMTextBox.
	/// </summary>
	[Serializable()]
	public class SMTextBox : System.Windows.Forms.UserControl,
		System.Runtime.Serialization.ISerializable, Drawable, Controls.Clonable,
		System.Xml.Serialization.IXmlSerializable
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBox1;

		public MoveNextControls MoveNext;
		public string[] Words;
		private ArrayList labels;
		private bool flag;
		private int prevHeight;
		private int prevWidth;

		public override string Text
		{
			get{return textBox1.Text;}
			set{textBox1.Text = value;}
		}

		public override Color ForeColor
		{
			get
			{
				return textBox1.ForeColor;
			}
			set
			{
				textBox1.ForeColor = value;
			}
		}

		public void Draw(System.Drawing.Graphics e)
		{
			e.DrawString(textBox1.Text,textBox1.Font,new SolidBrush(textBox1.ForeColor),
				new RectangleF(
				   new Point(Parent.Location.X+Location.X+100,
						((int)((Parent.Location.Y+Location.Y)*Report.PageScaleY))%Report.PageHeight+100),
						this.Size));
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SMTextBox()
		{
			prevHeight = -1;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			//this.SMTextBox_Resize(this,null);
			prevWidth = this.Width;
			MoveNext = new MoveNextControls(proxy);
			labels = new ArrayList();
			Words = new string[0];
			/*for(int i=0; i<5; i++)
				Words[i] = "word"+i.ToString();*/
			flag = true;
			AddLabel(Words);
			prevHeight = this.Height;
		}

		public SMTextBox(string[] words)
		{
			prevHeight = -1;
			InitializeComponent();
			//this.SMTextBox_Resize(this,null);
			MoveNext = new MoveNextControls(proxy);
			Words = words;
			labels = new ArrayList();
			flag = true;
			AddLabel(Words);
			prevHeight = this.Height;
		}

		public void PrepareToSerialize()
		{
			flag = false;
			clearLabel();
			this.Height = textBox1.Height;
			if((prevHeight>=0)&&(this.Height!=prevHeight))
				MoveNext(this.Top,this.Height-prevHeight);
			//MoveNext(this.Top,(int)((this.Height>24?(this.Height-39):0)*0.75));
			flag = true;
		}

		public void PrepareToPrint()
		{
			Band b = this.Parent as Band;
			flag = false;
			if(b!=null)
			{
				//b.Height = textBox1.Height;
				int h;
				if(textBox1.Text!="")
					h = /*this.Height-panel1.Height*/textBox1.Height;
				else
					h = 0;
				if((prevHeight>=0)&&(h!=prevHeight))
					MoveNext(b.Top,h-prevHeight);
				//MoveNext(this.Top,(int)((this.Height>24?(this.Height-39):0)*0.75));
				/*if(this.textBox1.Text!="") MoveNext(this.Top,-5);
				else MoveNext(this.Top,5);*/
				//MoveNext(this.Top,-(int)(((1-Report.PageScaleY)*textBox1.Height)/Report.PageScaleY));
			}
			clearLabel();
			flag = true;
		}

		#region ISerializable

		public SMTextBox(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context):base()
		{
			this.SuspendLayout();
			prevHeight = -1;
			InitializeComponent();
			MoveNext = new MoveNextControls(proxy);
			labels = new ArrayList();
			flag = true;

			this.Width = (int)info.GetValue("Width",Type.GetType("System.Int32"));
			this.Height = (int)info.GetValue("Height",Type.GetType("System.Int32"));
			this.Left = (int)info.GetValue("Left",Type.GetType("System.Int32"));
			this.Top = (int)info.GetValue("Top",Type.GetType("System.Int32"));
			textBox1.Text = (string)info.GetValue("Text",Type.GetType("System.String"));
			textBox1.Font = (Font)info.GetValue("Font",typeof(Font));
			this.ForeColor = (Color)info.GetValue("ForeColor",typeof(Color));
			this.Name = (string)info.GetValue("Name",typeof(string));
			this.TabIndex = (int)info.GetValue("TabIndex",typeof(int));
			this.Words = (string[])info.GetValue("Word",typeof(string[]));
			for(int i=0; i<Words.Length; i++)
				Words[i] = (string)info.GetValue("Word"+i.ToString(),typeof(string));
			AddLabel(Words);
			this.ResumeLayout(true);
			prevHeight = this.Height;
		}

		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("Width",this.Width);
			info.AddValue("Height",this.Height);
			info.AddValue("Left",this.Left);
			info.AddValue("Top",this.Top);
			info.AddValue("Text",textBox1.Text);
			info.AddValue("Font",textBox1.Font);
			info.AddValue("ForeColor",this.ForeColor);
			info.AddValue("Name",this.Name);
			info.AddValue("TabIndex",this.TabIndex);
			info.AddValue("Word",/*new string[0]*/this.Words);
			for(int i=0; i<Words.Length; i++)
				info.AddValue("Word"+i.ToString(),Words[i]);
			prevHeight = this.Height;
		}

		#endregion

		public void RedrawWords()
		{
			this.clearLabel();
			this.AddLabel(Words);
			this.Width--;
			this.Width++;
		}

		private void AddLabel(string str)
		{
			if(str==null) return;
			LinkLabel t = new LinkLabel();
			((Control)t).TabStop = false;
			t.Text = str;
			t.AutoSize = true;

			if(labels.Count>0)
			{
				LinkLabel last = (LinkLabel)labels[labels.Count-1];
				if(t.Width + last.Width + last.Left < this.Width)
					t.Location = new Point(last.Left+last.Width,last.Top);
				else
					t.Location = new Point(0,last.Top+last.Height);
			}
			else
				t.Location = new Point(0,0);
			t.LinkClicked += new LinkLabelLinkClickedEventHandler(addWord_Click);
			labels.Add(t);
			panel1.Controls.Add(t);
			panel1.Height = t.Top+t.Height;
		}

		private void AddLabel(string[] str)
		{
			if(str==null) return;
			this.SuspendLayout();
			for(int i=0; i<str.Length; i++)
				AddLabel(str[i]);
			//this.SMTextBox_Resize(this,null);
			this.ResumeLayout(/*true*/false);
		}

		private void clearLabel()
		{
			this.Height -= panel1.Height;
			if(this.Parent!=null)
				this.Parent.Height -= panel1.Height;
			panel1.Controls.Clear();
			panel1.Height = 0;
			if(labels!=null)
				labels.Clear();
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

		private void proxy(int t, int d)
		{
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Info;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(256, 0);
			this.panel1.TabIndex = 0;
			this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(0, 48);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(256, 24);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			this.textBox1.Resize += new System.EventHandler(this.textBox1_Resize);
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// SMTextBox
			// 
			this.BackColor = System.Drawing.SystemColors.Desktop;
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.panel1);
			this.Name = "SMTextBox";
			this.Size = new System.Drawing.Size(256, 72);
			this.Resize += new System.EventHandler(this.SMTextBox_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		private void addWord_Click(object sender, LinkLabelLinkClickedEventArgs e)
		{
			textBox1.Text += ((Control)sender).Text+" ";
		}

		private void SMTextBox_Resize(object sender, System.EventArgs e)
		{
			if(flag)
			{
				flag = false;
				if(prevWidth!=this.Width)
				{
					prevWidth = this.Width;
					clearLabel();
					AddLabel(Words);
				}
				textBox1_TextChanged(textBox1, e);
				this.Height = panel1.Height + textBox1.Height;
				flag = true;
				if((prevHeight>=0)&&(this.Height!=prevHeight))
				{
					if(this.Parent!=null)
					{
						this.Parent.Height+=this.Height-prevHeight;
						MoveNext(this.Parent.Top,this.Height-prevHeight);
					}
					else
						MoveNext(this.Top,this.Height-prevHeight);
				}
				prevHeight = this.Height;
			}
		}

		private void panel1_Resize(object sender, System.EventArgs e)
		{
			SMTextBox_Resize(sender,e);
		}

		private void textBox1_Resize(object sender, System.EventArgs e)
		{
			SMTextBox_Resize(sender,e);
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			TextBox ctrl = (TextBox)sender;
			using (Graphics g = ctrl.CreateGraphics())	
			{	
				ctrl.Height = 
					Convert.ToInt32(g.MeasureString(ctrl.Text, ctrl.Font,ctrl.Width).Height) + 
					ctrl.Font.Height;	
			}	
		}

		public System.Windows.Forms.Control Clone()
		{
			SMTextBox t = new SMTextBox();

			t.flag = false;
			t.Size = this.Size;
			t.Location = this.Location;
			/*if(Parent!=null)
			{
				t.Left += Parent.Left;
				t.Top += Parent.Top;
			}*/
			t.Left = this.Left;
			t.Top = this.Top;
			t.Text = textBox1.Text;
			t.Font = textBox1.Font;
			t.ForeColor = this.ForeColor;
			t.Name = this.Name;
			t.TabIndex = this.TabIndex;
			t.Words = this.Words;
			t.AddLabel(Words);
			t.prevHeight = t.Height;
			t.flag = true;
			//t.MoveNext = this.MoveNext;

			return t;
		}
		#region IXmlSerializable Members

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			/**writer.WriteAttributeString("Width",Width.ToString());
			writer.WriteAttributeString("Height",Height.ToString());
			writer.WriteAttributeString("Top",Top.ToString());
			writer.WriteAttributeString("Left",Left.ToString());
			writer.WriteAttributeString("TabIndex",TabIndex.ToString());
			writer.WriteElementString("Text",Text);
			/*writer.WriteAttributeString("ControlsCount",this.Controls.Count.ToString());
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
			}*/
			writer.WriteAttributeString("Width",Width.ToString());
			writer.WriteAttributeString("Height",Height.ToString());
			writer.WriteAttributeString("Top",Top.ToString());
			writer.WriteAttributeString("Left",Left.ToString());
			writer.WriteAttributeString("TabIndex",TabIndex.ToString());
			//***//
			writer.WriteAttributeString("ForeColor",ForeColor.Name);
			writer.WriteAttributeString("FontName",Font.Name);
			writer.WriteAttributeString("FontSize",Font.SizeInPoints.ToString());
			writer.WriteAttributeString("FontStyle",((int)Font.Style).ToString());
			//***//
			writer.WriteAttributeString("Name",Name);
			//***//
			writer.WriteAttributeString("ItemsCount",this.Words.Length.ToString());
			for(int i = 0; i<Words.Length; ++i)
				writer.WriteElementString("Item"+i.ToString(),Words[i]);
			writer.WriteElementString("Text",Text);

		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			/*Width = Int32.Parse(reader.GetAttribute("Width"));
			Height = Int32.Parse(reader.GetAttribute("Height"));
			Top = Int32.Parse(reader.GetAttribute("Top"));
			Left = Int32.Parse(reader.GetAttribute("Left"));
			reader.ReadStartElement();
			Text = reader.ReadElementString("Text");
			reader.ReadEndElement();*/
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
			//***//
			s = reader.GetAttribute("ForeColor");
			if(s!=null)
				ForeColor = Color.FromName(s);
			{
				string fName = null;
				float fSize = 10;
				FontStyle fStyle = 0;
				s = reader.GetAttribute("FontName");
				if(s!=null)
					fName = s;
				s = reader.GetAttribute("FontSize");
				if(s!=null)
					fSize = (float)Double.Parse(s);
				s = reader.GetAttribute("FontStyle");
				if(s!=null)
					fStyle = (FontStyle)Int32.Parse(s);
				if(fName!=null)
					Font = new Font(fName,fSize,fStyle,GraphicsUnit.Point);
			}
			//***//
			s = reader.GetAttribute("Name");
			if(s!=null)
				Name = s;
			//***//
			int c = 0;
			s = reader.GetAttribute("ItemsCount");
			if(s!=null)
				c = Int32.Parse(s);
			Words = new string[c];
			reader.ReadStartElement();
			for(int i = 0; i<c; ++i)
			{
				s = reader.ReadElementString("Item"+i.ToString());
				//reader.ReadEndElement();
				if(s!=null)
					Words[i] = s;
				else
					Words[i] = "";
			}
			RedrawWords();
			//reader.ReadStartElement();
			Text = reader.ReadElementString("Text");
			reader.ReadEndElement();
		}

		#endregion
	}
}
