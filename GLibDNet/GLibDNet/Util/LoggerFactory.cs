using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Util
{
	/// <summary>
	/// Loggerを生成するインターフェースです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public interface LoggerFactory
	{
		/// <summary>
		/// Loggerを生成します。
		/// </summary>
		/// <param name="type">Loggerを使用するクラスタイプ</param>
		/// <returns>Logger</returns>
		Logger create(Type type);
	}
}
