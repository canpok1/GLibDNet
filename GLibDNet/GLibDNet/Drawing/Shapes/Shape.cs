using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GLibDNet.Update;

namespace GLibDNet.Drawing.Shapes
{
	/// <summary>
	/// 図形を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public abstract class Shape
	{
		/// <summary>
		/// 左上座標
		/// </summary>
		private Position point;

		/// <summary>
		/// 大きさ
		/// </summary>
		private Size size;

		/// <summary>
		/// 図形を生成します。
		/// </summary>
		public Shape()
		{
			this.point = new Position(0, 0);
			this.size = new Size(0, 0);
		}

		/// <summary>
		/// 図形を生成します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public Shape(Int32 x, Int32 y, Int32 width, Int32 height) : this()
		{
			this.setPoint(x, y);
			this.setSize(width, height);
		}

		/// <summary>
		/// 図形を生成します。
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public Shape(Position p, Int32 width, Int32 height) : this()
		{
			this.setPoint(p);
			this.setSize(width, height);
		}

		/// <summary>
		/// 図形を生成します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="s">大きさ</param>
		public Shape(Int32 x, Int32 y, Size s) : this()
		{
			this.setPoint(x, y);
			this.setSize(s);
		}

		/// <summary>
		/// 図形を生成します。
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="s">大きさ</param>
		public Shape(Position p, Size s) : this()
		{
			this.setPoint(p);
			this.setSize(s);
		}

		/// <summary>
		/// 図形を元に新しい図形を生成します。
		/// </summary>
		/// <param name="obj">元になる図形</param>
		public Shape(Shape obj) : this()
		{
			// 引数チェック
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "Nullにはできません。");
			}

			this.setPoint(obj.getPoint());
			this.setSize(obj.getSize());
		}

		/// <summary>
		/// 座標を設定します。
		/// </summary>
		/// <param name="p">座標</param>
		public void setPoint(Position p)
		{
			// 引数チェック
			if (p == null)
			{
				throw new ArgumentNullException("p", "Nullにできません。");
			}
			this.point.setX(p.getX());
			this.point.setY(p.getY());
		}

		/// <summary>
		/// 座標を設定します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		public void setPoint(Int32 x, Int32 y)
		{
			this.point.setX(x);
			this.point.setY(y);
		}

		/// <summary>
		/// 座標を設定します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		public void setPoint(Single x, Single y)
		{
			this.point.setX(x);
			this.point.setY(y);
		}

		/// <summary>
		/// 座標を取得します。
		/// </summary>
		/// <returns>座標</returns>
		public Position getPoint()
		{
			return new Position(this.point);
		}

		/// <summary>
		/// 大きさを設定します。
		/// </summary>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public void setSize(Int32 width, Int32 height)
		{
			// 引数チェック
			if (width < 0)
			{
				throw new ArgumentOutOfRangeException("width", "負の値にはできません。");
			}
			if (height < 0)
			{
				throw new ArgumentOutOfRangeException("height", "負の値にはできません。");
			}

			this.size.Width = width;
			this.size.Height = height;
		}

		/// <summary>
		/// 大きさを設定します。
		/// </summary>
		/// <param name="size">大きさ</param>
		public void setSize(Size s)
		{
			// 引数チェック
			if (size == null)
			{
				throw new ArgumentNullException("size", "Nullにできません。");
			}

			this.size.Width = size.Width;
			this.size.Height = size.Height;
		}

		/// <summary>
		/// 大きさを取得します。
		/// </summary>
		/// <returns>大きさ</returns>
		public Size getSize()
		{
			return new Size(this.size.Width, this.size.Height);
		}

		/// <summary>
		/// 指定座標が図形内であるかを判定します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>true:座標が図形内 false:座標が図形外</returns>
		public abstract Boolean isContain(Int32 x, Int32 y);

		/// <summary>
		/// 指定座標が図形内であるかを判定します。
		/// </summary>
		/// <param name="point">座標</param>
		/// <returns>true:座標が図形内 false:座標が図形外</returns>
		public Boolean isContain(Position p)
		{
			return this.isContain(point.getIntegerX(), point.getIntegerY());
		}

		/// <summary>
		/// 図形を表示します。
		/// </summary>
		/// <param name="g">描画先</param>
		/// <returns>描画後の振る舞い</returns>
		public abstract DrawResultEnum drawMyself(Graphics g);

	}
}
