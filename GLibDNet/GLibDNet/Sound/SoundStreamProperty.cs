using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GLibDNet.Sound
{
	/// <summary>
	/// サウンドファイルのストリームを保持するクラスです。
	/// </summary>
	public class SoundStreamProperty : SoundProperty
	{
		/// <summary>
		/// サウンドファイルのストリーム
		/// </summary>
		private Stream stream;

		/// <summary>
		/// サウンドファイル情報のキー
		/// </summary>
		private String key;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="stream">サウンドファイルへのストリーム</param>
		/// <param name="key">サウンドファイル情報のキー</param>
		/// <param name="type">サウンドファイルの形式</param>
		public SoundStreamProperty(Stream stream, String key, SoundType type)
			: base(type)
		{
			// 引数チェック
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "nullにできません。");
			}
			if (String.IsNullOrEmpty(key) == true)
			{
				throw new ArgumentException("key", "空にできません。");
			}

			this.stream = stream;
			this.key = key;
		}

		/// <summary>
		/// サウンドファイルのストリームを取得します。
		/// </summary>
		/// <returns>サウンドファイルのストリーム</returns>
		public Stream getStream()
		{
			return this.stream;
		}

		/// <summary>
		/// サウンドファイル情報のキーを取得します。
		/// </summary>
		/// <returns>サウンドファイル情報</returns>
		public override string getKey()
		{
			return this.key;
		}
	}
}
