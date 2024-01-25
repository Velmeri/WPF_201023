using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_201023._2048
{
	public class Tile
	{
		public int Value { get; set; }

		public Tile(int value)
		{
			Value = value;
		}

		public void Merge(Tile anotherTile)
		{
			if (this.Value == anotherTile.Value) {
				this.Value *= 2;
				anotherTile.Value = 0;
			}
		}

		#region operators

		public override string ToString()
		{
			return this.Value.ToString();
		}

		public static implicit operator int(Tile obj)
		{
			return obj.Value;
		}

		#region Conditional operators
		public static bool operator ==(Tile left, int right)
		{
			if(left.Value == right)
				return true;
			return false;
		}

		public static bool operator !=(Tile left, int right)
		{
			if (left.Value != right)
				return true;
			return false;
		}

		public static bool operator >=(Tile left, int right)
		{
			if (left.Value >= right)
				return true;
			return false;
		}

		public static bool operator <=(Tile left, int right)
		{
			if (left.Value <= right)
				return true;
			return false;
		}

		public static bool operator >(Tile left, int right)
		{
			if (left.Value > right)
				return true;
			return false;
		}

		public static bool operator <(Tile left, int right)
		{
			if (left.Value < right)
				return true;
			return false;
		}

		public static bool operator ==(Tile left, Tile right)
		{
			if (left.Value == right.Value)
				return true;
			return false;
		}

		public static bool operator !=(Tile left, Tile right)
		{
			if (left.Value != right.Value)
				return true;
			return false;
		}

		public static bool operator >=(Tile left, Tile right)
		{
			if (left.Value >= right.Value)
				return true;
			return false;
		}

		public static bool operator <=(Tile left, Tile right)
		{
			if (left.Value <= right.Value)
				return true;
			return false;
		}

		public static bool operator >(Tile left, Tile right)
		{
			if (left.Value > right.Value)
				return true;
			return false;
		}

		public static bool operator <(Tile left, Tile right)
		{
			if (left.Value < right.Value)
				return true;
			return false;
		}

		#endregion

		#endregion
	}
}
