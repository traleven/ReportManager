using System;

namespace ReportManager.Controls
{
	public delegate void MoveNextControls(int height, int delta);

	public interface Drawable
	{
		void Draw(System.Drawing.Graphics e);
	}

	public interface Clonable
	{
		System.Windows.Forms.Control Clone();
	}

}
