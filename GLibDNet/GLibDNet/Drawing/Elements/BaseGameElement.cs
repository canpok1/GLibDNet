using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GLibDNet.Update;

namespace GLibDNet.Drawing
{
	/// <summary>
	/// 座標を持つ要素を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public abstract class BaseGameElement
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
		/// コンストラクタ
		/// </summary>
		public BaseGameElement()
		{
			this.point = new Position(0, 0);
			this.size = new Size(0, 0);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public BaseGameElement(Int32 x, Int32 y, Int32 width, Int32 height) : this()
		{
			this.setPoint(x, y);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public BaseGameElement(Position p, Int32 width, Int32 height) : this()
		{
			if (p == null)
			{
				throw new ArgumentNullException("p", "Nullにはできません。");
			}

			this.setPoint(p);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="s">大きさ</param>
		public BaseGameElement(Int32 x, Int32 y, Size s) : this()
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", "Nullにはできません。");
			}

			this.setPoint(x, y);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="s">大きさ</param>
		public BaseGameElement(Position p, Size s) : this()
		{
			// 引数チェック
			if (p == null)
			{
				throw new ArgumentNullException("p", "Nullにはできません。");
			}
			if (s == null)
			{
				throw new ArgumentNullException("s", "Nullにはできません。");
			}

			this.setPoint(p);
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
				throw new ArgumentNullException("p", "Nullにはできません。");
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
				throw new ArgumentOutOfRangeException("width", "負の値にすることはできません。");
			}

			this.size.Width = width;
			this.size.Height = height;
		}

		/// <summary>
		/// 大きさを設定します。
		/// </summary>
		/// <param name="s">大きさ</param>
		public void setSize(Size s)
		{
			// 引数チェック
			if (s == null)
			{
				throw new ArgumentNullException("s", "Nullにすることはできません。");
			}

			this.setSize(s.Width, s.Height);
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
		/// 指定座標が領域内かを判定します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>true:領域内 false:領域外</returns>
		public abstract Boolean isContain(Int32 x, Int32 y);

		/// <summary>
		/// 指定座標が領域内かを判定します。
		/// </summary>
		/// <param name="p">座標</param>
		/// <returns>true:領域内 false:領域外</returns>
		public Boolean isContain(Position p)
		{
			// 引数チェック
			if (p == null)
			{
				throw new ArgumentNullException("p", "Nullにはできません。");
			}

			return this.isContain(p.getIntegerX(), p.getIntegerY());
		}

		/// <summary>
		/// 領域を描画します。
		/// </summary>
		/// <param name="g">描画先</param>
		/// <returns>描画後の振る舞い</returns>
		public abstract DrawResultEnum drawMyself(Graphics g);
	}
}
