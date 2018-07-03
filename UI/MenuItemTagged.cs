using System;

namespace ReportManager
{
	/// <summary>
	/// Summary description for MenuItemTagged.
	/// </summary>
	public class MenuItemTagged: System.Windows.Forms.MenuItem
	{
		public object Tag;
		public MenuItemTagged(): base()
		{
			Tag = null;
		}
		public MenuItemTagged(string s):base(s)
		{
			Tag = null;
		}
		public MenuItemTagged(object tag):base()
		{
			Tag = tag;
		}
		public MenuItemTagged(string s, object tag): base(s)
		{
			Tag = tag;
		}
	}
}
