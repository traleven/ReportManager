using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ReportManager.Controls
{
	/// <summary>
	/// Serializable label
	/// </summary>
	[Serializable()]
	public class SLabel : System.Windows.Forms.Label,
		System.Runtime.Serialization.ISerializable, Controls.Drawable, Controls.Clonable,
		System.Xml.Serialization.IXmlSerializable
	{
		public SLabel():base()
		{
		}

		public SLabel(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context):base()
		{
			this.SuspendLayout();
			this.Width = (int)info.GetValue("Width",Type.GetType("System.Int32"));
			this.Height = (int)info.GetValue("Height",Type.GetType("System.Int32"));
			this.Left = (int)info.GetValue("Left",Type.GetType("System.Int32"));
			this.Top = (int)info.GetValue("Top",Type.GetType("System.Int32"));
			this.Text = (string)info.GetValue("Text",Type.GetType("System.String"));
			this.Font = (Font)info.GetValue("Font",typeof(Font));
			this.ForeColor = (Color)info.GetValue("ForeColor",typeof(Color));
			this.Name = (string)info.GetValue("Name",typeof(string));
			this.TabIndex = (int)info.GetValue("TabIndex",typeof(int));
			this.ResumeLayout(false);
		}

		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("Width",this.Width);
			info.AddValue("Height",this.Height);
			info.AddValue("Left",this.Left);
			info.AddValue("Top",this.Top);
			info.AddValue("Text",this.Text);
			info.AddValue("Font",this.Font);
			info.AddValue("ForeColor",this.ForeColor);
			info.AddValue("Name",this.Name);
			info.AddValue("TabIndex",this.TabIndex);
		}

		public void Draw(System.Drawing.Graphics e)
		{
			e.DrawString(this.Text,this.Font,new SolidBrush(this.ForeColor),
				(System.Drawing.PointF)new System.Drawing.Point(
				Parent.Location.X+Location.X+100,
				((int)((Parent.Location.Y+Location.Y)*Report.PageScaleY))%Report.PageHeight+100));
		}

		public System.Windows.Forms.Control Clone()
		{
			SLabel t = new SLabel();
			t.Size = this.Size;
			t.Location = this.Location;
			/*if(Parent!=null)
			{
				t.Left += Parent.Left;
				t.Top += Parent.Top;
			}*/
			t.Left = this.Left;
			t.Top = this.Top;
			t.Text = this.Text;
			t.Font = this.Font;
			t.ForeColor = this.ForeColor;
			t.Name = this.Name;
			t.TabIndex = this.TabIndex;

			return t;
		}
		#region IXmlSerializable Members

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			writer.WriteAttributeString("Width",Width.ToString());
			writer.WriteAttributeString("Height",Height.ToString());
			writer.WriteAttributeString("Top",Top.ToString());
			writer.WriteAttributeString("Left",Left.ToString());
			writer.WriteAttributeString("TabIndex",TabIndex.ToString());
			//***//
			writer.WriteAttributeString("ForeColor",ForeColor.Name);
			writer.WriteAttributeString("TextAlign",((int)TextAlign).ToString());
			writer.WriteAttributeString("FontName",Font.Name);
			writer.WriteAttributeString("FontSize",Font.SizeInPoints.ToString());
			writer.WriteAttributeString("FontStyle",((int)Font.Style).ToString());
			//***//
			writer.WriteAttributeString("Name",Name);
			//***//
			writer.WriteElementString("Text",Text);
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
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
			s = reader.GetAttribute("TextAlign");
			if(s!=null)
				TextAlign = (ContentAlignment)Int32.Parse(s);
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
			reader.ReadStartElement();
			Text = reader.ReadElementString("Text");
			reader.ReadEndElement();
		}

		#endregion
	}
}
