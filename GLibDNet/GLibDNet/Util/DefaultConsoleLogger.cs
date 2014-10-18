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
	/// ライブラリ初期化中などでのみ使用されます。
	/// </newpara>
	/// <newpara>
	/// 
	/// </newpara>
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class DefaultConsoleLogger : Logger
	{
		/// <summary>
		/// ログの記録対象クラス
		/// </summary>
		private Type type;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="className">Loggerを使用するクラス名</param>
		internal DefaultConsoleLogger(Type type)
		{
			// 引数チェック
			if (type == null)
			{
				throw new ArgumentNullException("type", "nullにすることはできません。");
			}

			this.type = type;
		}

		/// <summary>
		/// デバッグレベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void debug(String message)
		{
			this.print(message);
		}

		/// <summary>
		/// インフォレベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void info(String message)
		{
			this.print(message);
		}

		/// <summary>
		/// 警告レベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void warn(String message)
		{
			this.print(message);
		}


		/// <summary>
		/// エラーレベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void error(String message)
		{
			this.print(message);
		}

		/// <summary>
		/// エラーレベルでログを出力
		/// </summary>
		/// <param name="e">例外</param>
		public void error( Exception e )
		{
			this.print( e.ToString() );
		}

		/// <summary>
		/// 致命的レベルでログを出力
		/// </summary>
		/// <param name="message">出力するメッセージ</param>
		public void fatal(String message)
		{
			this.print(message);
		}

		/// <summary>
		/// 致命的レベルでログを出力
		/// </summary>
		/// <param name="e">例外</param>
		public void fatal( Exception e )
		{
			this.print( e.ToString() );
		}

		/// <summary>
		/// メッセージをコンソールに出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		private void print(String message)
		{
			string buff;

			buff = "[" + System.Threading.Thread.CurrentThread.ManagedThreadId + "]"
						+ " [-----] "
						+ this.type
						+ " '" + message + "'";

			System.Console.WriteLine(buff);
		}
	}
}
