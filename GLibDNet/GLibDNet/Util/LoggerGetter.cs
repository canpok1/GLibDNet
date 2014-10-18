using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Util
{
	/// <summary>
	/// Loggerを取得するクラスです。
	/// このクラスはシングルトンです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class LoggerGetter
	{
		/// <summary>
		/// インスタンス
		/// </summary>
		private static LoggerGetter singleton = new LoggerGetter();

		/// <summary>
		/// Loggerを生成するインスタンスです。
		/// </summary>
		private LoggerFactory factory;


		/// <summary>
		/// コンストラクタ
		/// </summary>
		private LoggerGetter()
		{
			factory = null;
		}


		/// <summary>
		/// インスタンスを取得します。
		/// </summary>
		/// <returns></returns>
		public static LoggerGetter getInstance()
		{
			return LoggerGetter.singleton;
		}


		/// <summary>
		/// Loggerを取得します。
		/// 取得するLoggerはsetFactoryで設定したインスタンスから生成します。
		/// setFactoryの設定前の場合はDefaultConsoleLoggerが生成されます。
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Logger getLogger(Type type)
		{
			Logger logger;

			if (this.factory == null)
			{
				logger = new DefaultConsoleLogger(type);
			}
			else
			{
				logger = this.factory.create(type);
			}

			return logger;
		}


		/// <summary>
		/// LoggerFactoryを設定します。
		/// 設定したLoggerFactoryからLoggerが生成されます。
		/// </summary>
		/// <param name="factory"></param>
		internal void setFactory(LoggerFactory factory)
		{
			// 引数チェック
			if (factory == null)
			{
				throw new ArgumentNullException("factory", "nullにすることはできません。");
			}

			this.factory = factory;
		}
	}
}
