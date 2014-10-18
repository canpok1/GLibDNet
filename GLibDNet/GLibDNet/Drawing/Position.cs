using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Drawing
{
	/// <summary>
	/// 座標を表すクラスです。
	/// 座標は実数値で保持しています。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class Position
	{
		/// <summary>
		/// 座標
		/// </summary>
		private PointF point;

		/// <summary>
		/// 座標を作成します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		public Position(Single x, Single y)
		{
			this.point = new PointF(x, y);
		}

		/// <summary>
		/// 座標を作成します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		public Position(Int32 x, Int32 y)
		{
			this.point = new PointF(x, y);
		}

		/// <summary>
		/// 同じ位置の座標を作成します。
		/// </summary>
		/// <param name="obj"></param>
		public Position(Position obj)
		{
			// 引数チェック
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "Nullにできません。");
			}

			this.point = new PointF(obj.getX(), obj.getY());
		}

		/// <summary>
		/// X座標を取得します。
		/// </summary>
		/// <returns>X座標</returns>
		public Single getX()
		{
			return this.point.X;
		}

		/// <summary>
		/// Y座標を取得します。
		/// </summary>
		/// <returns>Y座標</returns>
		public Single getY()
		{
			return this.point.Y;
		}

		/// <summary>
		/// X座標を取得します。
		/// 保持している座標を四捨五入した値となります。
		/// </summary>
		/// <returns>X座標</returns>
		public Int32 getIntegerX()
		{
			Int32 x = (Int32)Math.Round(this.point.X);
			return x;
		}

		/// <summary>
		/// Y座標を取得します。
		/// 保持している座標を四捨五入した値となります。
		/// </summary>
		/// <returns>Y座標</returns>
		public Int32 getIntegerY()
		{
			Int32 y = (Int32)Math.Round(this.point.Y);
			return y;
		}

		/// <summary>
		/// X座標を設定します。
		/// </summary>
		/// <param name="x">X座標</param>
		public void setX(Single x)
		{
			this.point.X = x;
		}

		/// <summary>
		/// X座標を設定します。
		/// </summary>
		/// <param name="x">X座標</param>
		public void setX(Int32 x)
		{
			this.point.X = (Single)x;
		}

		/// <summary>
		/// Y座標を設定します。
		/// </summary>
		/// <param name="y">Y座標</param>
		public void setY(Single y)
		{
			this.point.Y = y;
		}

		/// <summary>
		/// Y座標を設定します。
		/// </summary>
		/// <param name="y">Y座標</param>
		public void setY(Int32 y)
		{
			this.point.Y = (Single)y;
		}
	}
}
