using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GLibDNet.Update;

namespace GLibDNet.Drawing.Shapes
{
	/// <summary>
	/// 四角を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class Rectangle : Shape
	{
		/// <summary>
		/// ブラシ
		/// </summary>
		public Brush brush { get; set; }

		/// <summary>
		/// 四角を生成します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public Rectangle(Int32 x, Int32 y, Int32 width, Int32 height)
			: base(x, y, width, height)
		{
			this.initialize();
		}

		/// <summary>
		/// 四角を生成します。
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		public Rectangle(Position p, Int32 width, Int32 height)
			: base(p, width, height)
		{
			this.initialize();
		}

		/// <summary>
		/// 四角を生成します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="size">大きさ</param>
		public Rectangle(Int32 x, Int32 y, Size s)
			: base(x, y, s)
		{
			this.initialize();
		}

		/// <summary>
		/// 四角を生成します。
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="s">大きさ</param>
		public Rectangle(Position p, Size s)
			: base(p, s)
		{
			this.initialize();
		}

		/// <summary>
		/// 四角を元に新しい四角を生成します。
		/// </summary>
		/// <param name="obj">元になる四角</param>
		public Rectangle(Rectangle obj)
			: base(obj)
		{
			this.initialize();
		}

		/// <summary>
		/// 初期設定
		/// </summary>
		private void initialize()
		{
			this.brush = Brushes.Aqua;
		}

		/// <summary>
		/// 指定座標が図形内かを判定します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>true:図形内 false:図形外</returns>
		public override Boolean isContain(Int32 x, Int32 y)
		{
			Int32 left = this.getPoint().getIntegerX();
			Int32 right = left + this.getSize().Width;
			Int32 top = this.getPoint().getIntegerY();
			Int32 bottom = top + this.getSize().Height;

			if( x >= left && x < right
				&& y >= top && y < bottom )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 図形を描画します。
		/// </summary>
		/// <param name="g">描画先</param>
		/// <returns>描画後の振る舞い</returns>
		public override DrawResultEnum drawMyself(Graphics g)
		{
			g.FillRectangle(
				this.brush,
				this.getPoint().getX(),
				this.getPoint().getY(),
				this.getSize().Width,
				this.getSize().Height);

			return DrawResultEnum.ONCE;
		}

	}
}
