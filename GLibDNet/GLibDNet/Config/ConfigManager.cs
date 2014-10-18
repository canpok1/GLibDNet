using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Config
{
	/// <summary>
	/// 設定値を操作するクラスです。
	/// このクラスはシングルトンです。
	/// </summary>
	class ConfigManager
	{
		/// <summary>
		/// インスタンス
		/// </summary>
		private static ConfigManager singleton = new ConfigManager();

		/// <summary>
		/// 設定ファイルのパス
		/// </summary>
		private String path;

		/// <summary>
		/// 設定値
		/// </summary>
		private Configuration config;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private ConfigManager()
		{
			this.path = null;
			this.config = null;
		}

		/// <summary>
		/// インスタンスを取得します。
		/// </summary>
		/// <returns>インスタンス</returns>
		public static ConfigManager getInstance()
		{
			return ConfigManager.singleton;
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <returns>設定値</returns>
		public string getString(String name)
		{
			try
			{
				return this.config.getValue(name);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値取得時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <returns>設定値</returns>
		public Byte getByte(String name)
		{
			try
			{
				string value = this.config.getValue(name);
				return Byte.Parse(value);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値取得時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <returns>設定値</returns>
		public int getInteger(String name)
		{
			try
			{
				string value = this.config.getValue(name);
				return int.Parse(value);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値取得時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <returns>設定値</returns>
		public double getDouble(String name)
		{
			try
			{
				string value = this.config.getValue(name);
				return double.Parse(value);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値取得時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <param name="value">設定する値</param>
		public void setConfig(String name, String value)
		{
			try
			{
				this.config.setValue(name, value);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値設定時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <param name="value">設定する値</param>
		public void setConfig(String name, int value)
		{
			try
			{
				string str = value.ToString();
				this.config.setValue(name, str);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値設定時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <param name="value">設定する値</param>
		public void setConfig(String name, double value)
		{
			try
			{
				string str = value.ToString();
				this.config.setValue(name, str);
			}
			catch (Exception e)
			{
				throw new ArgumentException("値設定時に例外が発生しました。", e);
			}
		}

		/// <summary>
		/// 現在の設定値を読み込み時のファイルに上書き保存します。
		/// </summary>
		public void save()
		{
			// TODO 保存処理を実装
		}

		/// <summary>
		/// 設定ファイルから設定値を読み込みます。
		/// </summary>
		/// <param name="path">設定ファイルのパス</param>
		public void load(String path)
		{
			this.path = path;
			this.config = new Configuration();

			// TODO 設定ファイルから読み込む

			// TODO 仮の設定
			this.config.setValue("UPDATE_RATE", "30");
			this.config.setValue("REFRESH_RATE", "30");
			this.config.setValue("W_WIDTH", "800");
			this.config.setValue("W_HEIGHT", "600");
		}

		/// <summary>
		/// 前回読み込んだ設定ファイルから設定値を読み込みます。
		/// </summary>
		public void reload()
		{
			this.load(this.path);
		}

	}
}
