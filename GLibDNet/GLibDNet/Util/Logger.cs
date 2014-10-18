using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Util
{
	/// <summary>
	/// ログを記録するインターフェースです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public interface Logger
	{
		/// <summary>
		/// デバッグレベルのメッセージを出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		void debug(string message);

		/// <summary>
		/// インフォレベルのメッセージを出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		void info(string message);

		/// <summary>
		/// 警告レベルのメッセージを出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		void warn(string message);


		/// <summary>
		/// エラーレベルのメッセージを出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		void error(string message);

		/// <summary>
		/// エラーレベルのメッセージを出力します。
		/// </summary>
		/// <param name="e">例外</param>
		void error( Exception e );

		/// <summary>
		/// 致命的レベルのメッセージを出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		void fatal(string message);

		/// <summary>
		/// 致命的レベルのメッセージを出力します。
		/// </summary>
		/// <param name="e">例外</param>
		void fatal( Exception e );
	}
}
