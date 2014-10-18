using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Update;
using System.Drawing;

namespace GLibDNet.Drawing.Shapes
{
	/// <summary>
	/// 円を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class Circle : Shape
	{
		/// <summary>
		/// ブラシ
		/// </summary>
		public Brush brush { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">横幅</param>
		/// <param name="height">縦幅</param>
		public Circle( Int32 x, Int32 y, Int32 width, Int32 height )
			: base( x, y, width, height )
		{
			this.initialize();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="point">位置</param>
		/// <param name="width">横幅</param>
		/// <param name="height">縦幅</param>
		public Circle( Position point, Int32 width, Int32 height )
			: base( point, width, height )
		{
			this.initialize();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="size">大きさ</param>
		public Circle( Int32 x, Int32 y, Size size )
			: base( x, y, size )
		{
			this.initialize();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="point">位置</param>
		/// <param name="size">大きさ</param>
		public Circle( Position point, Size size )
			: base( point, size )
		{
			this.initialize();
		}

		/// <summary>
		/// 初期化
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
		public override Boolean isContain(int x, int y)
		{
			// TODO 実装
			throw new NotImplementedException();
		}

		/// <summary>
		/// 図形を描画します。
		/// </summary>
		/// <param name="g">描画先</param>
		/// <returns>描画後の振る舞い</returns>
		public override DrawResultEnum drawMyself(System.Drawing.Graphics g)
		{
			g.FillEllipse(
							this.brush,
							this.getPoint().getX(),
							this.getPoint().getY(),
							this.getSize().Width,
							this.getSize().Height );

			return DrawResultEnum.ONCE;
		}
	}
}
