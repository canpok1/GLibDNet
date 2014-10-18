using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Drawing
{
	/// <summary>
	/// 描画用のユーティリティです。
	/// </summary>
	public class DrawUtil
	{
		/// <summary>
		/// フォントの種類
		/// </summary>
		public enum FontType
		{
			GOSHIC,
		};

		/// <summary>
		/// 色
		/// </summary>
		public enum ColorType
		{
			WHITE,
			BLACK,
			RED,
			BLUE,
			YELLOW,
			GREEN,
		};

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private DrawUtil()
		{
		}

		/// <summary>
		/// フォントを取得します。
		/// </summary>
		/// <param name="type">種類</param>
		/// <param name="size">太さ</param>
		/// <returns>フォント</returns>
		public static Font GetFont( FontType type, Int32 size )
		{
			switch( type )
			{
				case FontType.GOSHIC :
					return new Font( "MSGoshic", size );
			}

			throw new ArgumentException( "type", "対応するフォントがありません。" );
		}


		/// <summary>
		/// ブラシを取得します。
		/// </summary>
		/// <param name="color">ブラシの色</param>
		/// <returns>ブラシ</returns>
		public static Brush GetBrush( ColorType color )
		{
			switch( color )
			{
				case ColorType.BLACK :
					return Brushes.Black;
				case ColorType.WHITE :
					return Brushes.White;
				case ColorType.RED :
					return Brushes.Red;
				case ColorType.BLUE :
					return Brushes.Blue;
				case ColorType.YELLOW :
					return Brushes.Yellow;
				case ColorType.GREEN :
					return Brushes.Green;
			}

			throw new ArgumentException( "color", "対応するブラシがありません。" );
		}
	}
}
