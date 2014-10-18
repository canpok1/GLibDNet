using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Config
{
	/// <summary>
	/// 設定値を操作するクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class ConfigUtility
	{
		/// <summary>
		/// このクラスはインスタンスを生成できません。
		/// </summary>
		private ConfigUtility()
		{
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		public static String getString(String name)
		{
			return ConfigManager.getInstance().getString(name);
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		public static Byte getByte(String name)
		{
			return ConfigManager.getInstance().getByte(name);
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		public static int getInteger(String name)
		{
			return ConfigManager.getInstance().getInteger(name);
		}

		/// <summary>
		/// 設定値を取得します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		public static Double getDouble(String name)
		{
			return ConfigManager.getInstance().getDouble(name);
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <param name="value">値</param>
		public static void setConfig(String name, String value)
		{
			ConfigManager.getInstance().setConfig(name, value);
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <param name="value">値</param>
		public static void setConfig(String name, int value)
		{
			ConfigManager.getInstance().setConfig(name, value);
		}

		/// <summary>
		/// 設定値に値を設定します。
		/// </summary>
		/// <param name="name">設定値の名前</param>
		/// <param name="value">値</param>
		public static void setConfig(String name, Double value)
		{
			ConfigManager.getInstance().setConfig(name, value);
		}

		/// <summary>
		/// 現在の設定値を設定ファイルに上書き保存します。
		/// </summary>
		public static void save()
		{
			ConfigManager.getInstance().save();
		}

		/// <summary>
		/// 設定ファイルから設定値を読み込みます。
		/// </summary>
		/// <param name="path">設定ファイルのパス</param>
		public static void load(String path)
		{
			ConfigManager.getInstance().load(path);
		}

		/// <summary>
		/// 前回読み込んだ設定ファイルから設定値を読み込みます。
		/// </summary>
		public static void reload()
		{
			ConfigManager.getInstance().reload();
		}
	}
}
