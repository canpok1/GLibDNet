using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GLibDNet.Util
{
	/// <summary>
	/// <newpara>
	/// ログをコンソールに出力するクラスです。
	/// </newpara>
	/// <newpara>
	/// 
	/// </newpara>
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class ConsoleLogger : Logger
	{
		/// <summary>
		/// ログの出力レベルです。
		/// </summary>
		public enum LoggingLevel
		{
			DEBUG,
			INFO,
			WARN,
			ERROR,
			FATAL,
		}

		/// <summary>
		/// ログの記録対象クラス
		/// </summary>
		private Type type;

		/// <summary>
		/// ログの出力レベル
		/// これ以上のレベルのメッセージを出力します。
		/// </summary>
		private LoggingLevel level;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="className">Loggerを使用するクラス名</param>
		internal ConsoleLogger(Type type, LoggingLevel level)
		{
			// 引数チェック
			if (type == null)
			{
				throw new ArgumentNullException("type", "nullにすることはできません。");
			}

			this.type = type;
			this.level = level;
		}

		/// <summary>
		/// デバッグレベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void debug(String message)
		{
			this.print(LoggingLevel.DEBUG, message);
		}

		/// <summary>
		/// インフォレベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void info(String message)
		{
			this.print(LoggingLevel.INFO, message);
		}

		/// <summary>
		/// 警告レベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void warn(String message)
		{
			this.print(LoggingLevel.WARN, message);
		}


		/// <summary>
		/// エラーレベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void error(String message)
		{
			this.print(LoggingLevel.ERROR, message);
		}

		/// <summary>
		/// エラーレベルでログを出力
		/// </summary>
		/// <param name="e">例外</param>
		public void error( Exception e )
		{
			if( e == null )
			{
				this.print( LoggingLevel.ERROR, null );
			}
			else
			{
				this.print( LoggingLevel.ERROR, e.ToString() );
			}
		}

		/// <summary>
		/// 致命的レベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void fatal(String message)
		{
			this.print(LoggingLevel.FATAL, message);
		}

		/// <summary>
		/// 致命的レベルでログを出力
		/// </summary>
		/// <param name="e">例外</param>
		public void fatal( Exception e )
		{
			if( e == null )
			{
				this.print( LoggingLevel.FATAL, null );
			}
			else
			{
				this.print( LoggingLevel.FATAL, e.ToString() );
			}
		}

		/// <summary>
		/// ログレベルが設定値以上の場合はメッセージをコンソールに出力します。
		/// </summary>
		/// <param name="level">ログレベル</param>
		/// <param name="message">メッセージ</param>
		private void print(LoggingLevel l, String message)
		{
			// エラーレベルをチェック
			if (l < this.level)
			{
				return;
			}

			string buff;

			buff = "[" + System.Threading.Thread.CurrentThread.ManagedThreadId + "]"
						+ " [" + l + "] "
						+ this.type;
			if (message != null)
			{
				buff += " '" + message + "'";
			}

			System.Console.WriteLine(buff);
		}
	}
}
