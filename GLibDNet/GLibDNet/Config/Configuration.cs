using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Config
{
	/// <summary>
	/// 設定値を保存するクラスです。
	/// </summary>
	class Configuration
	{
		/// <summary>
		/// 設定値
		/// </summary>
		private Dictionary<String, String> configList;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Configuration()
		{
			this.configList = new Dictionary<string, string>();
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="key">設定値の名前</param>
		/// <param name="value">設定値</param>
		public void setValue(String key, String value)
		{
			try
			{
				this.configList.Add(key, value);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値設定時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="key">設定値の名前</param>
		/// <returns>設定値</returns>
		public String getValue(String key)
		{
			try
			{
				return this.configList[key];
			}
			catch (Exception e)
			{
				throw new ArgumentException("値取得時に例外が発生しました。", e);
			}
		}
	}
}
