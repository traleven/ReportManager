using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ReportManager
{
	/// <summary>
	/// Summary description for ReportBuilder.
	/// </summary>
	public class ReportBuilder: IXmlSerializable
	{
		private Report r;

		public ReportBuilder()
		{
		}

		public ReportBuilder(Report rep)
		{
			r = rep;
		}

		public void Build(string path)
		{
			System.IO.Stream stream = 
				new System.IO.FileStream(path, System.IO.FileMode.Open);
			try
			{
				BinaryFormatter binary = new BinaryFormatter();
				r = (Report) binary.Deserialize(stream);
			}
			catch(Exception)
			{
				stream.Close();
				stream = new System.IO.FileStream(path, System.IO.FileMode.Open);
				XmlSerializer xml = new XmlSerializer(this.GetType());
				r = (xml.Deserialize(stream) as ReportBuilder).Report;
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			}
			stream.Close();
		}

		public void Fill(string[] lines)
		{
			if((r!=null)&&(lines!=null))
			{
				for(int i=0; (i<lines.Length)&&(i<r.Items.Count); i++)
					((System.Windows.Forms.Control)r.Items[i]).Text = lines[i];
			}
		}

		public Report Report
		{
			get{return r;}
		}

		public static Report ReloadRep(Report r, bool ToPrint)
		{
			Report t = r.Clone();
			t.AddControlsEvents();

			/*System.IO.Stream stream = 
				new System.IO.FileStream(".\\tmp\\rep.tmp",System.IO.FileMode.Create);
			BinaryFormatter binary = new BinaryFormatter();
			binary.Serialize(stream, t);
			stream.Close();

			stream = new System.IO.FileStream(".\\tmp\\rep.tmp", System.IO.FileMode.Open);
			//BinaryFormatter binary = new BinaryFormatter();
			t = (Report) binary.Deserialize(stream);
			stream.Close();
			if(ToPrint)
			{
				for(int i=0; i<t.Items.Count; i++)
					if(t.Items[i].GetType()==typeof(Controls.SMTextBox))
					{
						//((Controls.SMTextBox)t.Items[i]).PrepareToSerialize();
						((Controls.SMTextBox)t.Items[i]).PrepareToPrint();
					}
			}*/
			if(ToPrint)
			{
				foreach(Band b in t.Bands)
					foreach(Object c in b.Controls)
						if(c is Controls.SMTextBox)
						{
							//((Controls.SMTextBox)t.Items[i]).PrepareToSerialize();
							((Controls.SMTextBox)c).PrepareToPrint();
						}
			}
			return t;
		}

		#region IXmlSerializable Members

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			if(r!=null)
			{
				writer.WriteAttributeString("Width",r.Width.ToString());
				writer.WriteAttributeString("Height",r.Height.ToString());
				writer.WriteAttributeString("BGColor",r.BackColor.Name);
				writer.WriteAttributeString("BandsCount",r.Bands.Count.ToString());
				for(int i = 0; i<r.Bands.Count; ++i)
				{
					writer.WriteStartElement("Band");
					r.Bands[i].WriteXml(writer);
					writer.WriteEndElement();
				}
			}
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			r = new Report();
			r.Width = Int32.Parse(reader.GetAttribute("Width"));
			r.Height = Int32.Parse(reader.GetAttribute("Height"));
			r.BackColor = System.Drawing.Color.FromName(reader.GetAttribute("BGColor"));
			int c = Int32.Parse(reader.GetAttribute("BandsCount"));
			Band b;
			reader.ReadStartElement();
			string s;
			for(int i = 0; i<c; ++i)
			{
				s = reader.Name;
				while(!reader.IsStartElement("Band"))
				{
					reader.Skip();
					s = reader.Name;
				}
				s = reader.Name;

				b = new Band();
				b.ReadXml(reader);
				r.Bands.Add(b);
				r.Controls.Add(b);
			}
			reader.ReadEndElement();
			r.AddControlsEvents();
		}

		#endregion
	}
}
