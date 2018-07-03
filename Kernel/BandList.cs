using System;
using System.Collections;

namespace ReportManager
{
	/// <summary>
	/// Summary description for BandList.
	/// </summary>
	public class BandList: IList
	{
		protected ArrayList list;

		public BandList()
		{
			list = new ArrayList();
		}

		public BandList(ArrayList srcList)
		{
			list = srcList;
		}

		public Band GetBandAtHeight(int h)
		{
			Band ret = null;
			foreach(Band b in list)
				if(Math.Abs(b.Top-h)<5)
				{
					ret = b;
					return ret;
				}
			ret = new Band();
			ret.Top = h;
			this.Add(ret);
			return ret;
		}

		public void SortByPosition()
		{
			list.Sort(new bandComparer());
		}

		#region IList Members

		public bool IsReadOnly
		{
			get
			{
				return list.IsReadOnly;
			}
		}

		object IList.this[int index]
		{
			get
			{
				return list[index] as Band;
			}
			set
			{
				list[index]=value as Band;
			}
		}

		public Band this[int index]
		{
			get{return list[index] as Band;}
			set{list[index] = value;}
		}

		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
		}

		void IList.Insert(int index, object value)
		{
			list.Insert(index,value as Band);
		}

		public void Insert(int index, Band value)
		{
			list.Insert(index,value);
		}

		void IList.Remove(object value)
		{
			list.Remove(value);
		}

		public void Remove(Band value)
		{
			list.Remove(value);
		}

		bool IList.Contains(object value)
		{
			return list.Contains(value);
		}

		public bool Contains(Band value)
		{
			return list.Contains(value);
		}

		public void Clear()
		{
			list.Clear();
		}

		int IList.IndexOf(object value)
		{
			return list.IndexOf(value);
		}

		public int IndexOf(Band value)
		{
			return list.IndexOf(value);
		}

		int IList.Add(object value)
		{
			return list.Add(value as Band);
		}

		public int Add(Band value)
		{
			return list.Add(value);
		}

		public bool IsFixedSize
		{
			get
			{
				return list.IsFixedSize;
			}
		}

		#endregion

		#region ICollection Members

		public bool IsSynchronized
		{
			get
			{
				return list.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
				return list.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
			list.CopyTo(array,index);
		}

		public object SyncRoot
		{
			get
			{
				return list.SyncRoot;
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		protected class bandComparer: IComparer
		{
			#region IComparer Members

			public int Compare(object x, object y)
			{
				Band a = x as Band, b = y as Band;
				if((a==null)||(b==null))
				  return 0;
				return -b.Top+a.Top;
			}

			#endregion

		}

	}
}
