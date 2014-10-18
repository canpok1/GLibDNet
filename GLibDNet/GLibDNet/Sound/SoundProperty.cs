using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Sound
{
	/// <summary>
	/// サウンドファイルの情報を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public abstract class SoundProperty
	{
		/// <summary>
		/// サウンドファイルの形式
		/// </summary>
		private SoundType type;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="type">サウンドファイルの形式</param>
		public SoundProperty(SoundType type)
		{
			this.type = type;
		}

		/// <summary>
		/// サウンドファイル情報のキーを取得します。
		/// </summary>
		/// <returns>キー</returns>
		public abstract String getKey();

		/// <summary>
		/// サウンドファイルの形式を取得します。
		/// </summary>
		/// <returns>サウンドファイルの形式</returns>
		public SoundType getSoundType()
		{
			return this.type;
		}
	}
}
