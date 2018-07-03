using System;
using System.Windows.Forms;
using System.Collections;

namespace ReportManager
{
	/// <summary>
	/// Summary description for MenuBuilder.
	/// </summary>
	public class MenuBuilder
	{
		public MenuBuilder()
		{
		}

		public static MenuItem[] LoadMenu(string path)
		{
			System.IO.StreamReader input = new System.IO.StreamReader(path,
				System.Text.Encoding.Unicode);
			string s;
			string[] arr;
			ArrayList ret = new ArrayList();
			while((s=input.ReadLine()) != null)
			{
				arr = s.Split('|');
				ret.Add(new MenuItemTagged(arr[0],arr[1]));
			}
			return (MenuItem[])ret.ToArray(typeof(MenuItem));
		}
	}
}
