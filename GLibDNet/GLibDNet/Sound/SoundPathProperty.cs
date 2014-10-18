using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Sound
{
	/// <summary>
	/// サウンドファイルのパス情報を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class SoundPathProperty : SoundProperty
	{
		/// <summary>
		/// サウンドファイルのパス
		/// </summary>
		private String filePath;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">サウンドファイルのパス</param>
		/// <param name="type">サウンドファイルの形式</param>
		public SoundPathProperty(String filePath, SoundType type) : base(type)
		{
			// 引数チェック
			if (String.IsNullOrEmpty(filePath) == true)
			{
				throw new ArgumentException("filePath", "空にはできません。");
			}

			this.filePath = filePath;
		}

		/// <summary>
		/// サウンドファイルのパスを取得します。
		/// </summary>
		/// <returns>サウンドファイルのパス</returns>
		public String getFilePath()
		{
			return this.filePath;
		}

		/// <summary>
		/// サウンドファイル情報のキーを取得します。
		/// サウンドファイルのパスをキーとします。
		/// </summary>
		/// <returns>サウンドファイルのキー</returns>
		public override string getKey()
		{
			return this.filePath;
		}
	}
}
