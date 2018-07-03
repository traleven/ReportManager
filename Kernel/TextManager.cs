using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ReportManager
{
	/// <summary>
	/// Summary description for TextManager.
	/// </summary>
	public class TextManager
	{
		public TextManager()
		{
		}

		public virtual void Save(Report r, object Arg)
		{
			System.IO.Stream stream = 
				new System.IO.FileStream(".\\Reports\\"+Arg.ToString()+".xml", //.rep
											System.IO.FileMode.Create);
			/*BinaryFormatter binary = new BinaryFormatter();
			binary.Serialize(stream, r);*/
			ReportBuilder rb = new ReportBuilder(r);
			XmlSerializer xml = new XmlSerializer(rb.GetType());
			xml.Serialize(stream,rb);
			stream.Close();
		}

		public virtual Report Load(string Rep, object Arg)
		{
			ReportBuilder b = new ReportBuilder();
			b.Build(".\\Reports\\"+Rep+".rep");
			return b.Report;
		}

		#region DateToSql
		public static string DateToSQL(string str)
		{
			char[] s = str.ToCharArray();
			for(int i=0; i<s.Length; i++)
				if(s[i]=='.') s[i]='/';
			return new String(s);
		}

		public static string DateToSQL(DateTime d)
		{
			char[] s = d.ToShortDateString().ToCharArray();
			for(int i=0; i<s.Length; i++)
				if(s[i]=='.') s[i]='/';
			return new String(s);
		}	
		#endregion

		#region FileStr
		public static string[] StringsFromFile(string path)
		{
			System.IO.StreamReader fin = new System.IO.StreamReader(path,
				System.Text.Encoding.Unicode);
			ArrayList a = new ArrayList();
			string t;
			while((t=fin.ReadLine())!=null)
				a.Add(t);
			fin.Close();
			return ((string[])a.ToArray(typeof(string)));
		}
		#endregion
	}
}
