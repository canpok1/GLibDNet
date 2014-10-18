using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GLibDNet.Update;

namespace GLibDNet.Drawing.Elements
{
	/// <summary>
	/// 文字を表示するクラスです。
	/// </summary>
	public class TextViewer : BaseGameElement
	{
		/// <summary>
		/// 表示する文字列
		/// </summary>
		private String text;

		/// <summary>
		/// 文字のフォント
		/// </summary>
		public Font font { get; set; }

		/// <summary>
		/// ブラシ
		/// </summary>
		public Brush brush { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="text">表示する文字列</param>
		public TextViewer( Int32 x, Int32 y, String text )
			: base(x, y, 0, 0)
		{
			this.initialize();
			this.setText(text);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="text">文字列</param>
		public TextViewer( Position p, String text )
			: base(p, 0, 0 )
		{
			this.initialize();
			this.setText(text);
		}

		/// <summary>
		/// 初期値を設定します。
		/// </summary>
		private void initialize()
		{
			this.font = new Font("MSGoshic", 20);
			this.brush = Brushes.Black;
			this.setText("");
		}

		/// <summary>
		/// 指定座標が領域内かを判定します。
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>true:領域内 false:領域外</returns>
		public override Boolean isContain(Int32 x, Int32 y)
		{
			Int32 left = this.getPoint().getIntegerX();
			Int32 right = left + this.getSize().Width;
			Int32 top = this.getPoint().getIntegerY();
			Int32 bottom = top + this.getSize().Height;

			if( x < left
				|| x > right
				|| y < top
				|| y > bottom )
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 描画します。
		/// </summary>
		/// <param name="g">描画先</param>
		/// <returns>描画後の振る舞い</returns>
		public override DrawResultEnum drawMyself(Graphics g)
		{
			g.DrawString(
				this.text,
				this.font,
				this.brush,
				this.getPoint().getX(),
				this.getPoint().getY() );

			return DrawResultEnum.ONCE;
		}

		/// <summary>
		/// 表示する文字列を設定します。
		/// </summary>
		/// <param name="text">表示文字列</param>
		public void setText(String text)
		{
			this.text = text;
			// TODO 領域サイズを設定
		}
	}
}
